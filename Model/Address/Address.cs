using Backend.Model;
using FrontEnd.Model;
using System.Data.Common;

namespace SkipManagement.Model
{
    [Table(nameof(Address))]
    public class Address : AbstractModel<Address>
    {
        #region backing fields
        private long _addressid;
        private string _streetNum = string.Empty;
        private string _streetName = string.Empty;
        private string _furtherInfo = string.Empty;
        private PostCode? _postCode;
        #endregion

        #region Properties
        [PK]
        public long AddressID { get => _addressid; set => UpdateProperty(ref value, ref _addressid); }
        [Field]
        public string StreetNum { get => _streetNum; set => UpdateProperty(ref value, ref _streetNum); }
        [Field]
        public string StreetName { get => _streetName; set => UpdateProperty(ref value, ref _streetName); }
        [Field]
        public string FurtherInfo { get => _furtherInfo; set => UpdateProperty(ref value, ref _furtherInfo); }
        [FK]
        public PostCode? PostCode { get => _postCode; set => UpdateProperty(ref value, ref _postCode); }
        #endregion

        #region Constructors
        public Address() { }
        public Address(long id) => _addressid = id;
        public Address(DbDataReader db)
        {
            _addressid = db.GetInt64(0);
            _streetNum = db.GetString(1);
            _streetName = db.GetString(2);
            _furtherInfo= db.GetString(3);
            _postCode = new(db.GetInt64(4));
        }
        #endregion

        public override string ToString() => $"{StreetNum}, {StreetName} - {FurtherInfo}. {PostCode}";
    }
}
