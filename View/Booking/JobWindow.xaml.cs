using FrontEnd.ExtensionMethods;
using SkipManagement.Controller;
using System.Windows;

namespace SkipManagement.View
{
    public partial class JobWindow : Window
    {
        public JobWindow()
        {
            InitializeComponent();
            this.SetController(new JobListController());
        }
    }
}