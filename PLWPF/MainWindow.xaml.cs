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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //BL.IBL bl;

        public MainWindow()
        {
            InitializeComponent();
            //bl = BL.FactoryBl.Instance;
            //bl.AddOrderList();
            //bl.AddHostList();
            //bl.AddHostingUnitList();
            //bl.AddGuestRequestList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Add_Guest_Request add_Guest_Request = new Add_Guest_Request();
            add_Guest_Request.ShowDialog();          
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ManegerPassword manegerPassword = new ManegerPassword();
            manegerPassword.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Hosting_Unit hosting_Unit = new Hosting_Unit();
            hosting_Unit.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("?האם אתה בטוח", "יציאה מהמערכת", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show("מקווים שנהנתם,מאור וישי");
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    break;
            }           
        }
    }
}
