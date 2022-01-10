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
    /// Interaction logic for GuestListQuery.xaml
    /// </summary>
    public partial class GuestListQuery : Window
    {
        int count = 0;
        List<List<BE.GuestRequest>> ListToListOfGR;
        BL.IBL bl;
        ObservableCollection<BE.GuestRequest> guestRequestsObservable;
        List<BE.GuestRequest> guestRequestsList;


        IEnumerable<IGrouping<BE.Areas, BE.GuestRequest>> list;
        public GuestListQuery()
        {
            InitializeComponent();
            bl = BL.FactoryBl.Instance;

            list = bl.GuestRequestsGroupingByArea();
            ListToListOfGR = new List<List<BE.GuestRequest>>();
            foreach (var item in list)
            {
                ListToListOfGR.Add(item.ToList());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource guestRequestViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("guestRequestViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // guestRequestViewSource.Source = [generic data source]
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (count < ListToListOfGR.Count())
            {
                guestRequestsList = ListToListOfGR[count];
                guestRequestsObservable = new ObservableCollection<BE.GuestRequest>(guestRequestsList);
                guestRequestDataGrid.DataContext = guestRequestsObservable;
                count++;
            }
            if (count == ListToListOfGR.Count())
            {
                count = 0;
            }

        }

        private void GoBackclick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}