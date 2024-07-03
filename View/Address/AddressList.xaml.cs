using FrontEnd.ExtensionMethods;
using SkipManagement.Controller;
using System.Windows.Controls;

namespace SkipManagement.View
{
    public partial class AddressList : Page
    {
        public AddressList()
        {
            InitializeComponent();
            this.SetController(new AddressListController());
        }
    }
}