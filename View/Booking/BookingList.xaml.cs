using FrontEnd.ExtensionMethods;
using SkipManagement.Controller;
using System.Windows.Controls;

namespace SkipManagement.View
{
    public partial class BookingList : Page
    {
        public BookingList()
        {
            InitializeComponent();
            this.SetController(new BookingListController());
        }
    }
}
