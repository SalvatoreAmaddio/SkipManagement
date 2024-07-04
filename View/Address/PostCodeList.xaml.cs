using FrontEnd.ExtensionMethods;
using SkipManagement.Controller;
using System.Windows;

namespace SkipManagement.View
{
    public partial class PostCodeList : Window
    {
        public PostCodeList()
        {
            InitializeComponent();
            this.SetController(new PostCodeListController());
        }
    }
}
