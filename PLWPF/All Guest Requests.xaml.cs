using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for All_Guest_Requests.xaml
    /// </summary>
    public partial class All_Guest_Requests : Window
    {
        BL.IBL bl;
        ObservableCollection<BE.GuestRequest> guestRequests;
        public All_Guest_Requests()
        {
            InitializeComponent();
            bl = BL.FactoryBl.Instance;
            guestRequests = new ObservableCollection<BE.GuestRequest>(bl.GetAllGuestRequests());
            guestRequestDataGrid.DataContext = guestRequests;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource guestRequestViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("guestRequestViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // guestRequestViewSource.Source = [generic data source]
        }
    }
}
