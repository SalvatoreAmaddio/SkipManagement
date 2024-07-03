using Backend.Database;
using Backend.Utils;
using FrontEnd.ExtensionMethods;
using SkipManagement.Model;
using System.Windows;

namespace SkipManagement
{
    public partial class App : Application
    {
        public App()
        {
            Sys.LoadAllEmbeddedDll();

            DatabaseManager.Add(new SQLiteDatabase<Skip>());

            DatabaseManager.Add(new SQLiteDatabase<Customer>());
            DatabaseManager.Add(new SQLiteDatabase<CustomerAddress>());

            DatabaseManager.Add(new SQLiteDatabase<Address>());
            DatabaseManager.Add(new SQLiteDatabase<City>());
            DatabaseManager.Add(new SQLiteDatabase<PostCode>());

            DatabaseManager.Add(new SQLiteDatabase<Driver>());

            DatabaseManager.Add(new SQLiteDatabase<Booking>());
            DatabaseManager.Add(new SQLiteDatabase<PaymentType>());
            DatabaseManager.Add(new SQLiteDatabase<Status>());
            DatabaseManager.Add(new SQLiteDatabase<Job>());

            this.DisposeOnExit();
        }
    }
}