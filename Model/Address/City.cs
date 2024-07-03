using Backend.Model;
using FrontEnd.Model;
using System.Data.Common;

namespace SkipManagement.Model
{
    [Table(nameof(City))]
    public class City : AbstractModel<City>
    {
        #region backing fields
        private long _cityId;
        private string _cityName = string.Empty;
        #endregion

        #region Properties
        [PK]
        public long CityID { get => _cityId; set => UpdateProperty(ref value, ref _cityId); }
        [Field]
        public string CityName { get => _cityName; set => UpdateProperty(ref value, ref _cityName); }
        #endregion

        #region Constructor
        public City() { }

        public City(DbDataReader db) 
        {
            _cityId = db.GetInt64(0);
            _cityName = db.GetString(1);
        }

        public City(long cityID) => _cityId = cityID;
        #endregion

        public override string ToString() => $"{CityName}";
    }
}
