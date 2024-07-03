using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Events;
using SkipManagement.Model;

namespace SkipManagement.Controller
{
    public class CustomerAddressListController : AbstractFormListController<CustomerAddress>
    {
        public override AbstractClause InstantiateSearchQry() =>
        new CustomerAddress().Select().From();
        public override void OnOptionFilterClicked(FilterEventArgs e)
        {
        }

        public override Task<IEnumerable<CustomerAddress>> SearchRecordAsync()
        {
            throw new NotImplementedException();
        }

        protected override void Open(CustomerAddress model)
        {
        }
    }
}
