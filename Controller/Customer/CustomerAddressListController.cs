using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Events;
using FrontEnd.Source;
using SkipManagement.Model;
using Backend.Events;
using Backend.Database;

namespace SkipManagement.Controller
{
    public class CustomerAddressListController : AbstractFormListController<CustomerAddress>
    {
        public RecordSource<Address> Addresses { get; private set; } = new(DatabaseManager.Find<Address>()!);
        internal CustomerAddressListController() 
        {
            OpenWindowOnNew = false;
            AfterRecordNavigation += OnNewRecord;
        }

        private void OnNewRecord(object? sender, AllowRecordMovementArgs e)
        {
            if (e.NewRecord) 
            {
                if (CurrentRecord!=null)
                    CurrentRecord.Customer = (Customer?)ParentRecord;

                CurrentRecord?.Clean();
            }
        }

        public override void OnOptionFilterClicked(FilterEventArgs e)
        {
        }

        public override async void OnSubFormFilter()
        {
            ReloadSearchQry();
            SearchQry.AddParameter("customerID", ParentRecord?.GetPrimaryKey()?.GetValue());
            RecordSource<CustomerAddress> results = await CreateFromAsyncList(SearchQry.Statement(), SearchQry.Params());
            RecordSource.ReplaceRange(results);
            GoFirst();
        }

        public override Task<IEnumerable<CustomerAddress>> SearchRecordAsync()
        {
            throw new NotImplementedException();
        }

        protected override void Open(CustomerAddress model)
        {
        }

        public override AbstractClause InstantiateSearchQry() =>
        new CustomerAddress().Select().All().From().Where().EqualsTo("CustomerID","@customerID");
    }
}