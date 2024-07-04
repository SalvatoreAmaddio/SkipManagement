using FrontEnd.ExtensionMethods;
using SkipManagement.Controller;
using System.Windows;

namespace SkipManagement.View
{
    public partial class BookingWindow : Window
    {
        public BookingWindow()
        {
            InitializeComponent();
            this.SetController(new BookingController());
        }
    }
}