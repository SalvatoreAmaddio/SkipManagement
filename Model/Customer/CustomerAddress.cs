using Backend.Model;
using FrontEnd.Model;
using System.Data.Common;

namespace SkipManagement.Model
{
    [Table(nameof(CustomerAddress))]
    public class CustomerAddress : AbstractModel<CustomerAddress>
    {
        #region backing properties
        private long _customerAddressID;
        private Customer? _customer;
        private Address? _address;
        private bool _hasLicence;
        private bool _hasBaySuspension;
        #endregion

        #region Properties
        [PK]
        public long CustomerAddressID { get => _customerAddressID; set => UpdateProperty(ref value, ref _customerAddressID); }
        [FK]
        public Customer? Customer { get => _customer; set => UpdateProperty(ref value, ref _customer); }
        [FK]
        public Address? Address { get => _address; set => UpdateProperty(ref value, ref _address); }
        [Field]
        public bool HasLicence { get => _hasLicence; set => UpdateProperty(ref value, ref _hasLicence); }
        [Field]
        public bool HasBaySuspension { get => _hasBaySuspension; set => UpdateProperty(ref value, ref _hasBaySuspension); }
        #endregion

        #region Constructors
        public CustomerAddress() { }
        public CustomerAddress(long customerAddressID) => _customerAddressID = customerAddressID;
        public CustomerAddress(long customerAddressID, bool hasLicence, bool hasSuspension, Customer customer, Address address) : this(customerAddressID)
        {
            _customer = customer;
            _address = address;
            _hasLicence = hasLicence;
            _hasBaySuspension = hasSuspension;
        }

        public CustomerAddress(DbDataReader db)
        {
            _customerAddressID = db.GetInt64(0);
            _customer = new Customer(db.GetInt64(1));
            _address = new Address(db.GetInt64(2));
            _hasLicence = db.GetBoolean(3);
            _hasBaySuspension = db.GetBoolean(4);
        }
        #endregion

        public override string ToString() => $"{Customer} - {Address} - Has Licence: {HasLicence} - Has Bay Suspension: {HasBaySuspension}";

    }
}
