using FrontEnd.ExtensionMethods;
using SkipManagement.Controller;
using System.Windows;

namespace SkipManagement.View
{
    public partial class CityList : Window
    {
        public CityList()
        {
            InitializeComponent();
            this.SetController(new CityListController());
        }
    }
}