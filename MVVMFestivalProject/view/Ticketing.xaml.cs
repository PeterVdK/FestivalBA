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
using System.Windows.Shapes;

namespace MVVMFestivalProject.view
{
    /// <summary>
    /// Interaction logic for Ticketing.xaml
    /// </summary>
    public partial class Ticketing : UserControl
    {
        public Ticketing()
        {
            InitializeComponent();
        }

        private void btnNieuw_Click(object sender, RoutedEventArgs e)
        {
            btnToevoegen.IsEnabled = true;
            btnBewerken.IsEnabled = false;
            btnExporteren.IsEnabled = false;
            txbOver.Visibility = Visibility.Hidden;
        }

        private void lsvReservaties_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnExporteren.IsEnabled = true;
            btnToevoegen.IsEnabled = false;
            btnBewerken.IsEnabled = true;
        }

        private void cboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboType.SelectedIndex > -1)
            {
                txbOver.Visibility = Visibility.Visible;
            }
        }
    }
}
