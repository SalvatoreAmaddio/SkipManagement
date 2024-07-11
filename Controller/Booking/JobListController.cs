using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Events;
using FrontEnd.FilterSource;
using SkipManagement.Model;

namespace SkipManagement.Controller
{
    public class JobListController : AbstractFormListController<Job>
    {
        public SourceOption TimeForOptions { get; private set; }
        public JobListController() 
        {
            OpenWindowOnNew = false;
            TimeForOptions = new PrimitiveSourceOption(this, "TimeFor");
            AfterUpdate += OnAfterUpdate;
        }

        private async void OnAfterUpdate(object? sender, AfterUpdateArgs e)
        {
            if (!e.Is(nameof(Search))) return;
            await OnSearchPropertyRequeryAsync(sender);
        }

        public override void OnOptionFilterClicked(FilterEventArgs e) 
        {
            ReloadSearchQry();
            TimeForOptions.Conditions<WhereClause>(SearchQry);
            OnAfterUpdate(e, new(null, null, nameof(Search)));
        }

        public override async Task<IEnumerable<Job>> SearchRecordAsync()
        {
            SearchQry.AddParameter("name", Search.ToLower() + "%");
            return await CreateFromAsyncList(SearchQry.Statement(), SearchQry.Params());
        }

        protected override void Open(Job model) { }

        public override AbstractClause InstantiateSearchQry() =>
        new Job().Select().All().From().Where().OpenBracket().Like("LOWER(JobName)", "@name").CloseBracket();
    }
}