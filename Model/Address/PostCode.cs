using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Model;
using System.Data.Common;

namespace SkipManagement.Model
{
    [Table(nameof(PostCode))]
    public class PostCode : AbstractModel<PostCode>
    {
        #region backing fields
        private long _postCodeId;
        private string _code = string.Empty;
        private City? _city;
        #endregion

        #region Properties
        [PK]
        public long PostCodeID { get => _postCodeId; set => UpdateProperty(ref value, ref _postCodeId); }
        [Field]
        public string Code { get => _code; set => UpdateProperty(ref value, ref _code); }
        [FK]
        public City? City { get => _city; set => UpdateProperty(ref value, ref _city); }
        #endregion

        #region Constructors
        public PostCode()
        {
            SelectQry = this.Select().All().Fields("CityName").From().InnerJoin(new City()).Statement();
        }

        public PostCode(long id) : this() => _postCodeId = id;
        public PostCode(long id, string code) : this(id) => _code = code;
        public PostCode(long id, string code, City city) : this(id, code) => _city = city;

        public PostCode(DbDataReader db) : this()
        {
            _postCodeId = db.GetInt64(0);
            _code = db.GetString(1);
            _city = new(db.GetInt64(2), db.GetString(3));
        }
        #endregion

        public override string ToString() => $"{Code}";
    }
}
