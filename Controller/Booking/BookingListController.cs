using Backend.Database;
using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Events;
using FrontEnd.FilterSource;
using FrontEnd.Source;
using SkipManagement.Model;
using SkipManagement.View;

namespace SkipManagement.Controller
{
    public class BookingListController : AbstractFormListController<Booking>
    {
        public SourceOption DriverOptions { get; private set; }
        public SourceOption JobOptions { get; private set; }
        public SourceOption SkipOptions { get; private set; }
        public SourceOption CustomerOptions { get; private set; }
        public SourceOption TimeOptions { get; private set; }
        public SourceOption StartDateOptions { get; private set; }
        public SourceOption DeadlineOptions { get; private set; }
        public SourceOption StatusOptions { get; private set; }
        internal BookingListController()
        {
            StatusOptions = new SourceOption(new RecordSource<Status>(DatabaseManager.Find<Status>()!), "StatusName");
            DriverOptions = new SourceOption(new RecordSource<Driver>(DatabaseManager.Find<Driver>()!), "FullName");
            JobOptions = new SourceOption(new RecordSource<Job>(DatabaseManager.Find<Job>()!), "JobName");
            CustomerOptions = new SourceOption(new RecordSource<Customer>(DatabaseManager.Find<Customer>()!), "CustomerName");
            SkipOptions = new SourceOption(new RecordSource<Skip>(DatabaseManager.Find<Skip>()!), "SkipName");
            TimeOptions = new PrimitiveSourceOption(this, "TimeOf");
            StartDateOptions = new PrimitiveSourceOption(this, "StartDate");
            DeadlineOptions = new PrimitiveSourceOption(this, "Deadline");
            AfterUpdate += OnAfterUpdate;
        }

        private async void OnAfterUpdate(object? sender, AfterUpdateArgs e)
        {
            if (e.Is(nameof(Search))) 
                await OnSearchPropertyRequeryAsync(sender);
        }

        public override AbstractClause InstantiateSearchQry() =>
        new Booking().Select().All().Fields("CustomerAddress.HasLicence", "CustomerAddress.HasBaySuspension", "Customer.CustomerID", "CustomerName", "Address.AddressID", "StreetNum", "StreetName", "FurtherInfo", "PostCode.PostCodeID", "Code", "City.CityID", "CityName",
            "SkipName", "JobName", "Job.TimeFor", "FirstName", "LastName", "StatusName", $"ABS(julianday(Deadline) - julianday(StartDate)) AS Countdown")
            .From()
            .InnerJoin(nameof(CustomerAddress), "CustomerAddressID")
            .InnerJoin(nameof(CustomerAddress), nameof(Customer), "CustomerID")
            .InnerJoin(nameof(Skip), "SkipID")
            .InnerJoin(nameof(Job), "JobID")
            .InnerJoin(nameof(Driver), "DriverID")
            .InnerJoin(nameof(Status), "StatusID")
            .InnerJoin(nameof(CustomerAddress), nameof(Address), "AddressID")
            .InnerJoin(nameof(Address), nameof(PostCode), "PostCodeID")
            .InnerJoin(nameof(PostCode), nameof(City), "CityID")
            .Where()
            .OpenBracket()
            .Like("LOWER(StreetNum)", "@txt").OR().Like("LOWER(StreetName)", "@txt").OR().Like("LOWER(FurtherInfo)", "@txt").OR().Like("LOWER(Code)", "@txt").OR().Like("LOWER(CityName)", "@txt")
            .CloseBracket();

        public override void OnOptionFilterClicked(FilterEventArgs e)
        {
            ReloadSearchQry();
            StatusOptions.Conditions<WhereClause>(SearchQry);
            DriverOptions.Conditions<WhereClause>(SearchQry);
            JobOptions.Conditions<WhereClause>(SearchQry);
            CustomerOptions.Conditions<WhereClause>(SearchQry);
            SkipOptions.Conditions<WhereClause>(SearchQry);
            TimeOptions.Conditions<WhereClause>(SearchQry);
            StartDateOptions.Conditions<WhereClause>(SearchQry);
            DeadlineOptions.Conditions<WhereClause>(SearchQry);
            OnAfterUpdate(e, new(null, null, nameof(Search)));
        }

        public override async Task<IEnumerable<Booking>> SearchRecordAsync()
        {
            SearchQry.AddParameter("txt", "%" + Search.ToLower() + "%");
            return await CreateFromAsyncList(SearchQry.Statement(), SearchQry.Params());
        }

        protected override void Open(Booking model) => new BookingWindow(model).ShowDialog();
    }
}