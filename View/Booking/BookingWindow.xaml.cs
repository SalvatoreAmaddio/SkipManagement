using FrontEnd.ExtensionMethods;
using SkipManagement.Controller;
using SkipManagement.Model;
using System.Windows;

namespace SkipManagement.View
{
    public partial class BookingWindow : Window
    {
        public BookingWindow()
        {
            InitializeComponent();
        }

        public BookingWindow(Booking booking) : this()
        {
            this.SetController(new BookingController(booking));
        }
    }
}