using Backend.Model;
using FrontEnd.Model;
using System.Data.Common;

namespace SkipManagement.Model
{
    [Table(nameof(Driver))]
    public class Driver : AbstractModel<Driver>
    {
        #region backing fields
        private long _driverid;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        #endregion

        #region Properties
        [PK]
        public long DriverID { get => _driverid; set => UpdateProperty(ref value, ref _driverid); }
        [Field]
        public string FirstName { get => _firstName; set => UpdateProperty(ref value, ref _firstName); }
        [Field]
        public string LastName { get => _lastName; set => UpdateProperty(ref value, ref _lastName); }

        public string FullName { get => $"{FirstName} {LastName}"; }
        #endregion

        #region Constructors
        public Driver() { }
        public Driver(long id) => _driverid = id;
        public Driver(long id, string firstName, string lastName) : this(id)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        public Driver(DbDataReader db) 
        {
            _driverid = db.GetInt64(0);
            _firstName = db.GetString(1);
            _lastName = db.GetString(2);
        }
        #endregion

        public override string ToString() => $"{FirstName} {LastName}";
    }
}