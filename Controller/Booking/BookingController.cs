using Backend.Database;
using FrontEnd.Events;
using FrontEnd.Controller;
using FrontEnd.Source;
using SkipManagement.Model;
using Backend.Events;
using System.Windows;
using System.Windows.Input;

namespace SkipManagement.Controller
{
    public class BookingController : AbstractFormController<Booking>
    {
        private Visibility _nextJobVisibility = Visibility.Hidden;
        public Visibility NextJobVisibility { get => _nextJobVisibility; set => UpdateProperty(ref value, ref _nextJobVisibility); }
        public RecordSource<Customer> Customers { get; private set; } = new(DatabaseManager.Find<Customer>()!);
        public RecordSource<Address> Addresses { get; private set; } = new(DatabaseManager.Find<Address>()!);
        public RecordSource<Skip> Skips { get; private set; } = new(DatabaseManager.Find<Skip>()!);
        public RecordSource<Job> Jobs { get; private set; } = new(DatabaseManager.Find<Job>()!);
        public RecordSource<Driver> Drivers { get; private set; } = new(DatabaseManager.Find<Driver>()!);
        public RecordSource<Status> Status { get; private set; } = new(DatabaseManager.Find<Status>()!);
        public RecordSource<PaymentType> PaymentTypes { get; private set; } = new(DatabaseManager.Find<PaymentType>()!);
        public ICommand NextJobCMD { get; }

        #region Constructors
        internal BookingController()
        {
            AfterUpdate += OnAfterUpdate;
            NextJobCMD = new CMD(NextJob);
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

        private async void NextJob()
        {
            if (!PerformUpdate()) return;
            if (CurrentRecord == null) return;
            Customer? customer = CurrentRecord.Customer;
            Address? address = CurrentRecord.Address;
            GoNew();
            CurrentRecord.SetCustomer(customer);
            await RequeryAddress();
            CurrentRecord.RaisePropertyChanged(nameof(Customer));

            CurrentRecord.SetAddress(address);
            await CurrentRecord.SetCustomerAddressAsync();
            CurrentRecord.RaisePropertyChanged(nameof(Address));

            CurrentRecord.Clean();
            NextJobVisibility = Visibility.Hidden;
        }

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

            if (e.Is(nameof(Status)) && CurrentRecord != null && CurrentRecord.Status != null)
            {
                if (CurrentRecord.Status.StatusName.ToLower().Equals("done"))
                    NextJobVisibility = Visibility.Visible;
            }
            else NextJobVisibility = Visibility.Hidden;
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