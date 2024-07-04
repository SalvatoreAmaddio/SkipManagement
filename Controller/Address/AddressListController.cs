using Backend.Database;
using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Events;
using FrontEnd.FilterSource;
using FrontEnd.Source;
using SkipManagement.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkipManagement.Controller
{
    public class AddressListController : AbstractFormListController<Address>
    {
        public SourceOption PostCodesOptions { get; private set; }
        public SourceOption CitiesOptions { get; private set; }
        public SourceOption StreetNumOptions { get; private set; }
        public RecordSource<PostCode> PostCodes { get; private set; } = new(DatabaseManager.Find<PostCode>()!);
        public RecordSource<City> Cities { get; private set; } = new(DatabaseManager.Find<City>()!);

        public AddressListController()
        {
            OpenWindowOnNew = false;
            PostCodesOptions = new(PostCodes, "Code");
            CitiesOptions = new(Cities, "CityName");
            StreetNumOptions = new PrimitiveSourceOption(this, "StreetNum");
            AfterUpdate += OnAfterUpdate;
        }

        public override void OnOptionFilterClicked(FilterEventArgs e)
        {
            ReloadSearchQry();
            PostCodesOptions.Conditions<WhereClause>(SearchQry);
            CitiesOptions.Conditions<WhereClause>(SearchQry);
            StreetNumOptions.Conditions<WhereClause>(SearchQry);
            OnAfterUpdate(e, new(null, null, nameof(Search)));
        }

        private async void OnAfterUpdate(object? sender, AfterUpdateArgs e)
        {
            if (!e.Is(nameof(Search))) return;
            await OnSearchPropertyRequeryAsync(sender);
        }

        public override async Task<IEnumerable<Address>> SearchRecordAsync()
        {
            SearchQry.AddParameter("streetName", "%" + Search.ToLower() + "%");
            return await CreateFromAsyncList(SearchQry.Statement(), SearchQry.Params());
        }

        protected override void Open(Address model)
        {
        }

        public override AbstractClause InstantiateSearchQry() =>
        new Address()
            .Select().All().Fields("PostCode.Code").Fields("City.CityID").Fields("CityName")
            .From().InnerJoin(nameof(PostCode), "PostCodeID").InnerJoin(nameof(PostCode), nameof(City), "CityID")
            .Where().OpenBracket().Like("LOWER(StreetName)", "@streetName").CloseBracket();
    }
}