using System.Windows;

namespace SkipManagement.View
{
    public partial class AddressWindow : Window
    {
        public AddressWindow()
        {
            InitializeComponent();
        }

        private void OpenPostCode(object sender, RoutedEventArgs e)
        {
            PostCodeList postCodeList = new();
            postCodeList.ShowDialog();
        }

        private void OpenCities(object sender, RoutedEventArgs e)
        {
            CityList cityList = new();
            cityList.ShowDialog();
        }
    }
}