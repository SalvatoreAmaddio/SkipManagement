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

            this.DisposeOnExit();
        }
    }
}
