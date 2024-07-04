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
        private object _timeFor = new();
        #endregion

        #region Properties
        [PK]
        public long JobID { get => _jobId; set => UpdateProperty(ref value, ref _jobId); }
        [Field]
        public string JobName { get => _jobName; set => UpdateProperty(ref value, ref _jobName); }
        [Field]
        public object TimeFor { get => _timeFor; set => UpdateProperty(ref value, ref _timeFor); }
        #endregion

        #region Constructors
        public Job() { }
        public Job(long id) => _jobId = id;
        public Job(long id, string name) : this(id) => _jobName = name;
        public Job(long id, string name, object timeFor) : this(id, name) => _timeFor = timeFor;
        public Job(DbDataReader db)
        {
            _jobId = db.GetInt64(0);
            _jobName = db.GetString(1);
            _timeFor = db.GetValue(2);

            if (_timeFor is decimal _decimal && _decimal == 0)
                _timeFor = "N/A";
        }
        #endregion

        public override string ToString() => $"{JobName}";
    }
}