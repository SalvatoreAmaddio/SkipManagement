using FrontEnd.Controller;
using SkipManagement.Model;

namespace SkipManagement.Controller
{
    public class CustomerController : AbstractFormController<Customer>
    {
        public CustomerAddressListController CustomerAddressListController { get; } = new();
        internal CustomerController() 
        {
            AddSubControllers(CustomerAddressListController);
        }

        public CustomerController(Customer? customer) 
        { 
            GoAt(customer);
        }
    }
}
