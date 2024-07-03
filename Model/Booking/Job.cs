using Backend.Model;
using FrontEnd.Model;
using System.Data.Common;

namespace SkipManagement.Model
{
    [Table(nameof(Job))]
    public class Job : AbstractModel<Job>
    {
        #region backing field
        private long _jobId;
        private string _jobName = string.Empty;
        private string _timeFor = string.Empty;
        #endregion

        #region Properties
        [PK]
        public long JobID { get => _jobId; set => UpdateProperty(ref value, ref _jobId); }
        [Field]
        public string JobName { get => _jobName; set => UpdateProperty(ref value, ref _jobName); }
        [Field]
        public string TimeFor { get => _timeFor; set => UpdateProperty(ref value, ref _timeFor); }
        #endregion

        #region Constructors
        public Job() { }
        public Job(long id) => _jobId = id;
        public Job(DbDataReader db) 
        {
            _jobId = db.GetInt64(0);
            _jobName = db.GetString(1);
            _timeFor = db.GetString(2);
        }
        #endregion

        public override string ToString() => $"{JobName}";
    }
}
