using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Model;
using System.Data.Common;

namespace SkipManagement.Model
{
    [Table(nameof(Booking))]
    public class Booking : AbstractModel<Booking>
    {
        #region backing fields
        private long _bookingId;
        private Customer? _customer;
        private Address? _address;
        private DateTime? _startDate;
        private TimeSpan? _timeof;
        private Skip? _skip;
        private Job? _job;
        private string _daysFor = string.Empty;
        private Driver? _driver;
        private PaymentType? _paymentType;
        private DateTime? _deadline;
        private Status? _status;
        private int _countDown;
        #endregion

        #region Properties
        [PK]
        public long BookingID { get => _bookingId; set => UpdateProperty(ref value, ref _bookingId); }

        [FK]
        public Customer? Customer { get => _customer; set => UpdateProperty(ref value, ref _customer); }

        [FK]
        public Address? Address { get => _address; set => UpdateProperty(ref value, ref _address); }

        [Field]
        public DateTime? StartDate { get => _startDate; set => UpdateProperty(ref value, ref _startDate); }

        [Field]
        public TimeSpan? TimeOf { get => _timeof; set => UpdateProperty(ref value, ref _timeof); }

        [FK]
        public Skip? Skip { get => _skip; set => UpdateProperty(ref value, ref _skip); }

        [FK]
        public Job? Job { get => _job; set => UpdateProperty(ref value, ref _job); }

        [Field]
        public string DaysFor { get => _daysFor; set => UpdateProperty(ref value, ref _daysFor); }

        [FK]
        public Driver? Driver { get => _driver; set => UpdateProperty(ref value, ref _driver); }

        [FK]
        public PaymentType? PaymentType { get => _paymentType; set => UpdateProperty(ref value, ref _paymentType); }

        [Field]
        public DateTime? Deadline { get => _deadline; set => UpdateProperty(ref value, ref _deadline); }

        [FK]
        public Status? Status { get => _status; set => UpdateProperty(ref value, ref _status); }

        public int Countdown { get => _countDown; set => UpdateProperty(ref value, ref _countDown); }

        public string? CountdownString 
        { 
            get
            {
                if (Countdown > 0)
                    return $"{Countdown}day(s) left";
                else
                    return $"{Countdown}day(s) delay!";
            }
        }
        #endregion

        #region Constructor
        public Booking() 
        {
            SelectQry = this.Select().All().Fields("CustomerName", "StreetNum", "StreetName", "FurtherInfo", "PostCode.PostCodeID", "Code", "City.CityID", "CityName",
            "SkipName", "JobName", "FirstName", "LastName", "StatusName", "ABS(julianday(Deadline) - julianday(StartDate)) AS CountDown")
            .From()
            .InnerJoin(nameof(Customer),"CustomerID")
            .InnerJoin(nameof(Skip), "SkipID")
            .InnerJoin(nameof(Job), "JobID")
            .InnerJoin(nameof(Driver), "DriverID")
            .InnerJoin(nameof(Status), "StatusID")
            .InnerJoin(nameof(Address), "AddressID")
            .InnerJoin(nameof(Address), nameof(PostCode) ,"PostCodeID")
            .InnerJoin(nameof(PostCode), nameof(City), "CityID")
            .Statement();
        }

        public Booking(DbDataReader db) : this()
        {
            _bookingId = db.GetInt64(0);
            _customer = new(db.GetInt64(1), db.GetString(12));
            _address = new(db.GetInt64(2), db.GetString(13), db.GetString(14), db.GetString(15), new(db.GetInt64(16), db.GetString(17), new(db.GetInt64(18), db.GetString(19))));
            _startDate = db.TryFetchDate(3);
            _timeof = db.TryFetchTime(4);
            _skip = new(db.GetInt64(5), db.GetString(20));
            _job = new(db.GetInt64(6), db.GetString(21));
            _daysFor = db.GetValue(7)?.ToString();
            _driver = new(db.GetInt64(8), db.GetString(22), db.GetString(23));
            _paymentType = new(db.GetInt64(9));
            _deadline = db.TryFetchDate(10);
            _status = new(db.GetInt64(11), db.GetString(24));
            _countDown = (int)db.GetDouble(25);
        }
        #endregion

        public override string ToString() => $"{BookingID} - {Customer} - {Address} - {Skip} - {Job} - {Status}";
    }
}
