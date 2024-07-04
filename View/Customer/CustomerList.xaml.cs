using FrontEnd.ExtensionMethods;
using SkipManagement.Controller;
using System.Windows.Controls;

namespace SkipManagement.View
{
    public partial class CustomerList : Page
    {
        public CustomerList()
        {
            InitializeComponent();
            this.SetController(new CustomerListController());
        }
    }
}