using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for LineUp.xaml
    /// </summary>
    public partial class LineUp : UserControl
    {
        public LineUp()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate_1(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "JPG Files (.jpg)|*.jpg|PNG Files (.png)|*.png";
            if (ofd.ShowDialog() == true)
            {
                String sBestandsnaamAfbeelding = ofd.FileName;
                txtFoto.Text = sBestandsnaamAfbeelding;
            }
        }
    }
}
