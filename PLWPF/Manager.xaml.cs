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
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        public Manager()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource guestRequestViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("guestRequestViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // guestRequestViewSource.Source = [generic data source]
        }

        private void GoBackClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GuestListQuery guestListQuery = new GuestListQuery();
            guestListQuery.ShowDialog();
        }

        private void OrderListButton(object sender, RoutedEventArgs e)
        {
            EntryNumberForQuery entryNumberForQuery = new EntryNumberForQuery();
            entryNumberForQuery.ShowDialog();
        }

        private void HUClick(object sender, RoutedEventArgs e)
        {
            HostingUnitQuery hostingUnitQuery = new HostingUnitQuery();
            hostingUnitQuery.ShowDialog();
        }

        private void Button_All_Guest_Requests(object sender, RoutedEventArgs e)
        {
            All_Guest_Requests all_Guest_Requests = new All_Guest_Requests();
            all_Guest_Requests.Show();
        }

        private void Button_All_Hostinng_Units(object sender, RoutedEventArgs e)
        {
            All_Hostinng_Units all_Hostinng_Units = new All_Hostinng_Units();
            all_Hostinng_Units.Show();
        }

        private void Button_All_Orders(object sender, RoutedEventArgs e)
        {
            All_Orders all_Orders = new All_Orders();
            all_Orders.Show();
        }
    }
}
