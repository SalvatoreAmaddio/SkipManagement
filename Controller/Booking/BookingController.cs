using Backend.Database;
using FrontEnd.Events;
using FrontEnd.Controller;
using FrontEnd.Source;
using SkipManagement.Model;
using Backend.Events;

namespace SkipManagement.Controller
{
    public class BookingController : AbstractFormController<Booking>
    {
        public RecordSource<Customer> Customers { get; private set; } = new(DatabaseManager.Find<Customer>()!);
        public RecordSource<Address> Addresses { get; private set; } = new(DatabaseManager.Find<Address>()!);
        public RecordSource<Skip> Skips { get; private set; } = new(DatabaseManager.Find<Skip>()!);
        public RecordSource<Job> Jobs { get; private set; } = new(DatabaseManager.Find<Job>()!);
        public RecordSource<Driver> Drivers { get; private set; } = new(DatabaseManager.Find<Driver>()!);
        public RecordSource<Status> Status { get; private set; } = new(DatabaseManager.Find<Status>()!);
        public RecordSource<PaymentType> PaymentTypes { get; private set; } = new(DatabaseManager.Find<PaymentType>()!);

        #region Constructors
        internal BookingController() 
        {
            AfterUpdate += OnAfterUpdate;
        }

        public BookingController(Booking booking) : this() 
        {
            GoAt(booking);
            if (CurrentRecord != null)
               CurrentRecord.AfterUpdate += OnCurrentRecordAfterUpdate;
            AfterRecordNavigation += OnAfterRecordNavigation;
            BeforeRecordNavigation += OnBeforeRecordNavigation;
        }
        #endregion

        #region Event Subscriptions
        private void OnBeforeRecordNavigation(object? sender, AllowRecordMovementArgs e)
        {
            if (CurrentRecord != null)
                CurrentRecord.AfterUpdate -= OnCurrentRecordAfterUpdate;
        }

        private void OnAfterRecordNavigation(object? sender, AllowRecordMovementArgs e)
        {
            if (CurrentRecord != null)
                CurrentRecord.AfterUpdate += OnCurrentRecordAfterUpdate;
        }

        private async void OnCurrentRecordAfterUpdate(object? sender, AfterUpdateArgs e)
        {
            if (e.Is(nameof(Customer)))
            {
                await RequeryAddress();
                CurrentRecord?.SetAddress(null);
                return;
            }
        }

        private async void OnAfterUpdate(object? sender, AfterUpdateArgs e)
        {
            if (e.Is(nameof(CurrentRecord)))
                await RequeryAddress();
        }
        #endregion

        private async Task RequeryAddress()
        {
            RecordSource<Address> records = await Task.Run(() => Booking.FilterAddress(CurrentRecord?.Customer?.CustomerID));
            Addresses.ReplaceRange(records);
        }
    }
}