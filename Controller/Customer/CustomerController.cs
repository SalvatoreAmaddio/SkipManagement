using FrontEnd.Controller;
using SkipManagement.Model;
using SkipManagement.View;
using System.Windows.Input;

namespace SkipManagement.Controller
{
    public class CustomerController : AbstractFormController<Customer>
    {
        public ICommand OpenAddressWindowCMD { get; }
        public CustomerAddressListController CustomerAddressListController { get; } = new();
        internal CustomerController() 
        {
            AddSubControllers(CustomerAddressListController);
            OpenAddressWindowCMD = new CMD(OpenAddressWindow);
        }

        public CustomerController(Customer? customer) : this()
        { 
            GoAt(customer);
        }

        private void OpenAddressWindow()
        {
            AddressWindow addressWindow = new();
            addressWindow.ShowDialog();
        }
    }
}
