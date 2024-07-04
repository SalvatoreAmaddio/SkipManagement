using Backend.Database;
using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Dialogs;
using FrontEnd.Events;
using FrontEnd.Model;
using FrontEnd.Source;
using SkipManagement.Controller;
using System.Data.Common;

namespace SkipManagement.Model
{
    [Table(nameof(Booking))]
    public class Booking : AbstractModel<Booking>
    {
        #region backing fields
        private long _bookingId;
        private CustomerAddress? _customerAddress;
        private Customer? _customer;
        private Address? _address;
        private DateTime? _startDate = DateTime.Today;
        private TimeSpan? _timeof;
        private Skip? _skip;
        private Job? _job;
        private object _timeFor = new();
        private Driver? _driver;
        private PaymentType? _paymentType;
        private DateTime? _deadline;
        private Status? _status;
        private object _countDown = new();
        #endregion

        #region Properties
        [PK]
        public long BookingID { get => _bookingId; set => UpdateProperty(ref value, ref _bookingId); }

        [FK]
        public CustomerAddress? CustomerAddress { get => _customerAddress; set => UpdateProperty(ref value, ref _customerAddress); }

        [Field]
        public DateTime? StartDate { get => _startDate; set => UpdateProperty(ref value, ref _startDate); }

        [Field]
        public TimeSpan? TimeOf { get => _timeof; set => UpdateProperty(ref value, ref _timeof); }

        [FK]
        public Skip? Skip { get => _skip; set => UpdateProperty(ref value, ref _skip); }

        [FK]
        public Job? Job { get => _job; set => UpdateProperty(ref value, ref _job); }

        [Field]
        public object TimeFor { get => _timeFor; set => UpdateProperty(ref value, ref _timeFor); }

        [FK]
        public Driver? Driver { get => _driver; set => UpdateProperty(ref value, ref _driver); }

        [FK]
        public PaymentType? PaymentType { get => _paymentType; set => UpdateProperty(ref value, ref _paymentType); }

        [Field]
        public DateTime? Deadline { get => _deadline; set => UpdateProperty(ref value, ref _deadline); }

        [FK]
        public Status? Status { get => _status; set => UpdateProperty(ref value, ref _status); }

        public object Countdown { get => _countDown; set => UpdateProperty(ref value, ref _countDown); }

        public Customer? Customer { get => _customer; set => UpdateProperty(ref value, ref _customer); }
        public Address? Address { get => _address; set => UpdateProperty(ref value, ref _address); }

        public bool? HasLicence { get => _customerAddress?.HasLicence; }
        public bool? IsBaySuspension { get => _customerAddress?.HasBaySuspension; }

        public string? CountdownString
        { 
            get
            {
                if (Countdown is int _int && _int > 0)
                    return $"{Countdown}day(s) left";
                if (Countdown is string)
                    return $"N/A";
                else
                    return $"{Countdown}day(s) delay!";
            }
        }
        #endregion

        #region Constructor
        public Booking() 
        {
            SelectQry = this.Select().All().Fields("CustomerAddress.HasLicence", "CustomerAddress.HasBaySuspension", "Customer.CustomerID", "CustomerName", "Address.AddressID", "StreetNum", "StreetName", "FurtherInfo", "PostCode.PostCodeID", "Code", "City.CityID", "CityName",
            "SkipName", "JobName", "Job.TimeFor", "FirstName", "LastName", "StatusName", "ABS(julianday(Deadline) - julianday(StartDate)) AS CountDown")
            .From()
            .InnerJoin(nameof(CustomerAddress), "CustomerAddressID")
            .InnerJoin(nameof(CustomerAddress), nameof(Customer), "CustomerID")
            .InnerJoin(nameof(Skip), "SkipID")
            .InnerJoin(nameof(Job), "JobID")
            .InnerJoin(nameof(Driver), "DriverID")
            .InnerJoin(nameof(Status), "StatusID")
            .InnerJoin(nameof(CustomerAddress), nameof(Address), "AddressID")
            .InnerJoin(nameof(Address), nameof(PostCode) ,"PostCodeID")
            .InnerJoin(nameof(PostCode), nameof(City), "CityID")
            .Statement();

            AfterUpdate += OnAfterUpdate;
            BeforeUpdate += OnBeforeUpdate;
        }

        public Booking(DbDataReader db) : this()
        {
            _bookingId = db.GetInt64(0);
            _customerAddress = new(db.GetInt64(1), db.GetBoolean(11), db.GetBoolean(12), new Customer(db.GetInt64(13), db.GetString(14)), new Address(db.GetInt64(15), db.GetString(16), db.GetString(17), db.GetString(18), new PostCode(db.GetInt64(19), db.GetString(20), new City(db.GetInt64(21), db.GetString(22)))));
            _startDate = db.TryFetchDate(2);
            _timeof = db.TryFetchTime(3);
            _skip = new(db.GetInt64(4), db.GetString(23));
            _job = new(db.GetInt64(5), db.GetString(24), db.GetValue(25));
            _timeFor = db.GetValue(6);
            _driver = new(db.GetInt64(7), db.GetString(26), db.GetString(27));
            _paymentType = new(db.GetInt64(8));
            _deadline = db.TryFetchDate(9);
            _status = new(db.GetInt64(10), db.GetString(28));
            try 
            {
                _countDown = (int)db.GetDouble(29);
            }
            catch
            {
                _countDown = "N/A";
            }
            _customer = _customerAddress.Customer;
            _address = _customerAddress.Address;
        }
        #endregion

        private void OnBeforeUpdate(object? sender, BeforeUpdateArgs e)
        {
            if (e.Is(nameof(StartDate)))
            {
                DateTime? startDate = e.GetNewValueAs<DateTime?>();
                if (startDate.HasValue && Deadline.HasValue)
                    DateCheck(startDate > Deadline, e);
            }

            if (e.Is(nameof(Deadline)))
            {
                DateTime? deadline = e.GetNewValueAs<DateTime?>();
                if (StartDate.HasValue && deadline.HasValue)
                    DateCheck(StartDate > deadline, e);
            }
        }

        private static void DateCheck(bool value, BeforeUpdateArgs e)
        {
            if (value)
            {
                Failure.Allert("StartDate cannot be bigger than Deadline", "Logic Error");
                e.Cancel = true;
            }
        }
        private async void OnAfterUpdate(object? sender, FrontEnd.Events.AfterUpdateArgs e)
        {
            if (e.Is(nameof(Address)))
            {
                await FetchCustomerAddress();
                return;
            }

            if (e.Is(nameof(Job)))
            {
                if (Job != null)
                    TimeFor = Job.TimeFor;
                return;
            }

            if (e.Is(nameof(StartDate)) || e.Is(nameof(Deadline))) 
            {
                if (StartDate.HasValue && Deadline.HasValue)
                {
                    Countdown = (Deadline - StartDate).Value.Days;
                }
                RaisePropertyChanged(nameof(CountdownString));
            }

            if (e.Is(nameof(TimeFor))) 
            {
                if (TimeFor is decimal _decimal && StartDate.HasValue)
                {
                    Deadline = StartDate.Value.AddDays((double)_decimal);
                }
                else if (TimeFor is int _int && StartDate.HasValue)
                {
                    Deadline = StartDate.Value.AddDays(_int);
                }
                else
                {
                    Deadline = null;
                    Countdown = "N/A";
                }

                if (StartDate.HasValue && Deadline.HasValue) 
                {
                    Countdown = (Deadline - StartDate).Value.Days;
                }
                RaisePropertyChanged(nameof(CountdownString));
            }
        }

        public async Task FetchCustomerAddress()
        {
            List<QueryParameter> param = [];
            param.Add(new("customerID", Customer?.CustomerID));
            param.Add(new("addressID", Address?.AddressID));
            string sql = new CustomerAddress().Select().All().From().Where().EqualsTo("CustomerID", "@customerID").AND().EqualsTo("AddressID", "@addressID").Limit().Statement();
            using (CustomerAddressListController controller = new())
            {
                RecordSource<CustomerAddress> records = await Task.Run(() => controller.CreateFromAsyncList(sql, param));
                CustomerAddress = records.FirstOrDefault();
            }

            RaisePropertyChanged(nameof(HasLicence));
            RaisePropertyChanged(nameof(IsBaySuspension));
        }

        public static async Task<RecordSource<Address>> FilterAddress(long? customerID)
        {
            List<QueryParameter> param = [];
            param.Add(new("id", customerID));
            var sql = new Address().Select().All().Fields("PostCode.Code").Fields("City.CityID").Fields("CityName").From().InnerJoin(nameof(PostCode), "PostCodeID").InnerJoin(nameof(PostCode), nameof(City), "CityID").InnerJoin(nameof(CustomerAddress), "AddressID").Where().EqualsTo("CustomerAddress.CustomerID", "@id").Statement();
            using (AddressListController c = new()) 
            {
                return await c.CreateFromAsyncList(sql, param);
            }
        }

        public void SetAddress(Address? address) => _address = address;
        public override string ToString() => $"{BookingID} - {Customer} - {Address} - {Skip} - {Job} - {Status}";
    }
}