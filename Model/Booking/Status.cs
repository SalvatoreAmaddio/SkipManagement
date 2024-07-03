using Backend.Model;
using FrontEnd.Model;
using System.Data.Common;

namespace SkipManagement.Model
{
    [Table(nameof(Status))]
    public class Status : AbstractModel<Status>
    {
        #region backing fields
        private long _statusID;
        private string _statusName = string.Empty;
        #endregion

        #region Properties
        [PK]
        public long StatusID { get => _statusID; set => UpdateProperty(ref value, ref _statusID); }
        [Field]
        public string StatusName { get => _statusName; set => UpdateProperty(ref value, ref _statusName); }
        #endregion

        #region Constructor
        public Status() { }

        public Status(DbDataReader db) 
        {
            _statusID = db.GetInt64(0);
            _statusName = db.GetString(1);
        }
        #endregion

        public override string? ToString() => $"{StatusName}";
    }
}
