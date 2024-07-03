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
        #endregion

        #region Constructor
        public Booking() { }
        public Booking(DbDataReader db)
        {
            _bookingId = db.GetInt64(0);
            _customer = new(db.GetInt64(1));
            _address = new(db.GetInt64(2));
            _startDate = db.TryFetchDate(3);
            _timeof = db.TryFetchTime(4);
            _skip = new(db.GetInt64(5));
            _job = new(db.GetInt64(6));
            _daysFor = db.GetString(7);
            _driver = new(db.GetInt64(8));
            _paymentType = new(db.GetInt64(9));
            _deadline = db.TryFetchDate(10);
            _status = new(db.GetInt64(11));
        }
        #endregion

        public override string ToString() => $"{BookingID} - {Customer} - {Address} - {Skip} - {Job} - {Status}";
    }
}
