using Backend.Database;
using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Events;
using FrontEnd.Source;
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
        public RecordSource<PostCode> PostCodes { get; private set; } = new(DatabaseManager.Find<PostCode>()!);
        public AddressListController() 
        {
            OpenWindowOnNew = false;
            
        }
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

        public override AbstractClause InstantiateSearchQry() =>
        new Address()
            .Select().All()
            .From();

    }
}
