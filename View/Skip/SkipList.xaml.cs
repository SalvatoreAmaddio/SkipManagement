using FrontEnd.ExtensionMethods;
using SkipManagement.Controller;
using System.Windows.Controls;

namespace SkipManagement.View
{
    public partial class SkipList : Page
    {
        public SkipList()
        {
            InitializeComponent();
            this.SetController(new SkipListController());
        }
    }
}