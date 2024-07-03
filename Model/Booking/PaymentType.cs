using Backend.Model;
using FrontEnd.Model;
using System.Data.Common;

namespace SkipManagement.Model
{
    [Table(nameof(PaymentType))]
    public class PaymentType : AbstractModel<PaymentType>
    {
        #region backing fields
        private long _paymentTypeId;
        private string _paymentName = string.Empty;
        #endregion

        #region Properties
        [PK]
        public long PaymentTypeID { get => _paymentTypeId; set => UpdateProperty(ref value, ref _paymentTypeId); }
        [Field]
        public string PaymentName { get => _paymentName; set => UpdateProperty(ref value, ref _paymentName); }
        #endregion

        #region Constructors
        public PaymentType() { }
        public PaymentType(long id) => _paymentTypeId = id;
        public PaymentType(DbDataReader db) 
        {
            _paymentTypeId = db.GetInt64(0);
            _paymentName = db.GetString(1);
        }
        #endregion

        public override string ToString() => $"{PaymentName}";
    }
}