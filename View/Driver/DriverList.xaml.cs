using FrontEnd.ExtensionMethods;
using SkipManagement.Controller;
using System.Windows.Controls;

namespace SkipManagement.View
{
    public partial class DriverList : Page
    {
        public DriverList()
        {
            InitializeComponent();
            this.SetController(new DriverListController());
        }
    }
}