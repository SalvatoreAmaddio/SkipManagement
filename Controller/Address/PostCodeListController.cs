using Backend.Database;
using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Events;
using FrontEnd.FilterSource;
using FrontEnd.Source;
using SkipManagement.Model;
using SkipManagement.View;
using System.Windows.Input;

namespace SkipManagement.Controller
{
    public class PostCodeListController : AbstractFormListController<PostCode>
    {
        public SourceOption CitiesOptions { get; private set; }
        public ICommand OpenCityCMD { get; }
        public RecordSource<City> Cities { get; private set; } = new(DatabaseManager.Find<City>()!);
        internal PostCodeListController()
        {
            OpenWindowOnNew = false;
            AfterUpdate += OnAfterUpdate;
            CitiesOptions = new(Cities, "CityName");
            OpenCityCMD = new CMD(OpenCity);
        }

        private async void OnAfterUpdate(object? sender, AfterUpdateArgs e)
        {
            if (e.Is(nameof(Search))) 
                await OnSearchPropertyRequeryAsync(sender);  
        }

        private void OpenCity() 
        {
            CityList cityList = new();
            cityList.ShowDialog();
        }

        public override void OnOptionFilterClicked(FilterEventArgs e)
        {
            ReloadSearchQry();
            CitiesOptions.Conditions<WhereClause>(SearchQry);
            OnAfterUpdate(e, new(null, null, nameof(Search)));
        }

        public override async Task<IEnumerable<PostCode>> SearchRecordAsync()
        {
            SearchQry.AddParameter("code", Search.ToLower() + "%");
            return await CreateFromAsyncList(SearchQry.Statement(), SearchQry.Params());
        }

        protected override void Open(PostCode model)
        {
        }

        public override AbstractClause InstantiateSearchQry() =>
        new PostCode().Select().All().Fields("CityName").From().InnerJoin(new City()).Where().OpenBracket().Like("LOWER(Code)", "@code").CloseBracket();

    }
}