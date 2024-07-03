using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Events;
using SkipManagement.Model;

namespace SkipManagement.Controller
{
    public class DriverListController : AbstractFormListController<Driver>
    {
        public override AbstractClause InstantiateSearchQry() =>
        new Driver().Select().All().From();
        public override void OnOptionFilterClicked(FilterEventArgs e)
        {
        }

        public override Task<IEnumerable<Driver>> SearchRecordAsync()
        {
            throw new NotImplementedException();
        }

        protected override void Open(Driver model)
        {
        }
    }
}