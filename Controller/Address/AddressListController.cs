using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Events;
using SkipManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkipManagement.Controller
{
    public class AddressListController : AbstractFormListController<Address>
    {
        public override AbstractClause InstantiateSearchQry() =>
        new Address().Select().All().From();

        public override void OnOptionFilterClicked(FilterEventArgs e)
        {
        }

        public override Task<IEnumerable<Address>> SearchRecordAsync()
        {
            throw new NotImplementedException();
        }

        protected override void Open(Address model)
        {
        }
    }
}
