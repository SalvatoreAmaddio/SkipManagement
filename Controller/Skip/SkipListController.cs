using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Events;
using SkipManagement.Model;

namespace SkipManagement.Controller
{
    public class SkipListController : AbstractFormListController<Skip>
    {
        public SkipListController() 
        { 
            OpenWindowOnNew = false;
            AfterUpdate += OnAfterUpdate;
        }

        private async void OnAfterUpdate(object? sender, AfterUpdateArgs e)
        {
            if (e.Is(nameof(Search)))
            {
                await OnSearchPropertyRequeryAsync(sender);
            }
        }

        public override void OnOptionFilterClicked(FilterEventArgs e)
        {
        }

        public override async Task<IEnumerable<Skip>> SearchRecordAsync()
        {
            SearchQry.AddParameter("name", Search.ToLower() + "%");
            return await CreateFromAsyncList(SearchQry.Statement(), SearchQry.Params());
        }

        protected override void Open(Skip model)
        {
        }

        public override AbstractClause InstantiateSearchQry() =>
        new Skip().Select().All().From().Where().Like("LOWER(SkipName)", "@name");

    }
}