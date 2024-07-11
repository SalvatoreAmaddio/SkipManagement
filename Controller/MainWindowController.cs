using Backend.Utils;
using FrontEnd.Controller;
using FrontEnd.Forms;
using FrontEnd.Utils;
using SkipManagement.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SkipManagement.Controller
{
    public class MainWindowController
    {
        private readonly MainWindow _mainWin;
        private Curtain Curtain => _mainWin.Curtain;
        private TabControl MainTab => _mainWin.MainTab;

        #region Commands
        public ICommand OpenCurtainCMD { get; }
        public ICommand OpenPostCodeCMD { get; }
        public ICommand OpenCityCMD { get; }
        public ICommand OpenStatusCMD { get; }
        public ICommand OpenJobCMD { get; }
        public ICommand OpenPaymentTypeCMD { get; }
        #endregion

        public MainWindowController(MainWindow mainWin)
        {
            _mainWin = mainWin;
            _mainWin.DataContext = this;

            Helper.ManageTabClosing(MainTab);
            Curtain.SoftwareInfo = new SoftwareInfo("Salvatore Amaddio", "www.salvatoreamaddio.co.uk", "Mister J", "2024");

            OpenCurtainCMD = new CMD(OpenCurtain);
            OpenCityCMD = new CMD(OpenCity);
            OpenPostCodeCMD = new CMD(OpenPostCode);

            OpenStatusCMD = new CMD(OpenStatus);
            OpenJobCMD = new CMD(OpenJob);
            OpenPaymentTypeCMD = new CMD(OpenPaymentType);
        }

        private void OpenPaymentType()
        {
            new PaymentTypeWindow().ShowDialog();
        }

        private void OpenStatus()
        {
            new StatusWindow().ShowDialog();
        }
        private void OpenJob()
        {
            new JobWindow().ShowDialog();
        }

        private void OpenPostCode()
        {
            PostCodeList postCodeList = new();
            postCodeList.ShowDialog();
        }

        private void OpenCity()
        {
            CityList cityList = new();
            cityList.ShowDialog();
        }

        private void OpenCurtain() => Curtain.Open();
    }
}
