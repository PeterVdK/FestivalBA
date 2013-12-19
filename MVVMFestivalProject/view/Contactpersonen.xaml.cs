using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVMFestivalProject.view
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Contactpersonen : UserControl
    {
        public Contactpersonen()
        {
            InitializeComponent();
        }

        private void cboContactpersonen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboContactpersonen.SelectedIndex > -1)
            {
                btnOpslaan.IsEnabled = false;
                btnBewerken.IsEnabled = true;
                btnVerwijderen.IsEnabled = true;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            btnOpslaan.IsEnabled = true;
            btnBewerken.IsEnabled = false;
            btnVerwijderen.IsEnabled = false;
        }
    }
}
