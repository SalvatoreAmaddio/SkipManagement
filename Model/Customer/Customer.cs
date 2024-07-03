﻿using Backend.Model;
using FrontEnd.Model;
using System.Data.Common;

namespace SkipManagement.Model
{
    [Table(nameof(Customer))]
    public class Customer : AbstractModel<Customer>
    {
        #region backing fields
        private long _customerid;
        private string _customerName = string.Empty;
        private string _phoneNumber = string.Empty;
        private string _email = string.Empty;
        #endregion

        #region Properties
        [PK]
        public long CustomerID { get => _customerid; set => UpdateProperty(ref value, ref _customerid); }

        [Field]
        public string CustomerName { get => _customerName; set => UpdateProperty(ref value, ref _customerName); }

        [Field]
        public string PhoneNumber { get => _phoneNumber; set => UpdateProperty(ref value, ref _phoneNumber); }

        [Field]
        public string Email { get => _email; set => UpdateProperty(ref value, ref _email); }
        #endregion

        #region Constructor
        public Customer() { }
        public Customer(long id) => _customerid = id;
        public Customer(DbDataReader db)
        {
            _customerid = db.GetInt64(0);
            _customerName = db.GetString(1);
            _phoneNumber = db.GetString(2);
            _email = db.GetString(3);
        }
        #endregion

        public override string ToString() => $"{CustomerName}";
    }
}