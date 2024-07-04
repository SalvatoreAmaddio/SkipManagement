using SkipManagement.Controller;
using System.Windows;

namespace SkipManagement.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            new MainWindowController(this);
        }
    }
}