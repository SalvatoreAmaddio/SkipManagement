using Backend.Model;
using FrontEnd.Model;
using System.Data.Common;

namespace SkipManagement.Model
{
    [Table(nameof(Skip))]
    public class Skip : AbstractModel<Skip>
    {
        #region backing fields
        private long _skipid;
        private string _skipName = string.Empty;
        private double _price;
        #endregion

        #region Properties
        [PK]
        public long SkipID { get => _skipid; set => UpdateProperty(ref value, ref _skipid); }
        [Field]
        public string SkipName { get => _skipName; set => UpdateProperty(ref value, ref _skipName); }
        [Field]
        public double Price { get => _price; set => UpdateProperty(ref value, ref _price); }
        #endregion

        #region Constructor
        public Skip() { }
        public Skip(long id) => _skipid = id;
        public Skip(DbDataReader db) 
        {
            _skipid = db.GetInt64(0);
            _skipName = db.GetString(1);
            _price = db.GetDouble(2);
        }
        #endregion

        public override string ToString() => $"{SkipName}";
    }
}