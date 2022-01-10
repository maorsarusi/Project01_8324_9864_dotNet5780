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
    /// Interaction logic for OrderListQuery.xaml
    /// </summary>
    public partial class OrderListQuery : Window
    {
        BL.IBL bl;
        ObservableCollection<BE.Order> order, order1;
      
        public OrderListQuery(int x,int y)
        {
            InitializeComponent();
            bl = BL.FactoryBl.Instance;
            CreationTextBox.Text = x.ToString();
            senEmailTextBox.Text = y.ToString();
            order = new ObservableCollection<BE.Order>(bl.GetOrdersByMinimumDistanceFromCreationTime(x).ToList());
            order1 = new ObservableCollection<BE.Order>(bl.GetOrdersByMinimumDistanceFromSendingMailTime(y).ToList());
            orderDataGrid.DataContext = order;
            orderDataGrid1.DataContext = order1;
        }


        private void GoBackClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource orderViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("orderViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // orderViewSource.Source = [generic data source]

        }
    }
}
