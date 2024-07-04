using FrontEnd.ExtensionMethods;
using SkipManagement.Controller;
using SkipManagement.Model;
using System.Windows;

namespace SkipManagement.View
{
    public partial class CustomerForm : Window
    {
        public CustomerForm()
        {
            InitializeComponent();
        }

        public CustomerForm(Customer customer) : this()
        {
            this.SetController(new CustomerController(customer));
        }
    }
}