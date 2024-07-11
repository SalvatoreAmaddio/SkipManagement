using FrontEnd.ExtensionMethods;
using SkipManagement.Controller;
using System.Windows;

namespace SkipManagement.View
{
    public partial class PaymentTypeWindow : Window
    {
        public PaymentTypeWindow()
        {
            InitializeComponent();
            this.SetController(new PaymentTypeListController());
        }
    }
}