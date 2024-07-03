using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Events;
using SkipManagement.Model;

namespace SkipManagement.Controller
{
    public class DriverListController : AbstractFormListController<Driver>
    {
        public DriverListController() 
        {
            OpenWindowOnNew = false;
            AfterUpdate += OnAfterUpdate;
        }

        private async void OnAfterUpdate(object? sender, AfterUpdateArgs e)
        {
            if (!e.Is(nameof(Search))) return;
            await OnSearchPropertyRequeryAsync(sender);
        }

        public override void OnOptionFilterClicked(FilterEventArgs e)
        {
        }

        public override async Task<IEnumerable<Driver>> SearchRecordAsync()
        {
            SearchQry.AddParameter("name", Search.ToLower() + "%");
            SearchQry.AddParameter("name", Search.ToLower() + "%");
            return await CreateFromAsyncList(SearchQry.Statement(), SearchQry.Params());
        }

        protected override void Open(Driver model)
        {
        }

        public override AbstractClause InstantiateSearchQry() =>
        new Driver()
            .Select().All()
            .From()
            .Where()
                .OpenBracket()
                    .Like("LOWER(FirstName)", "@name")
                    .OR()
                    .Like("LOWER(LastName)", "@name")
                .CloseBracket();
    }
}