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
    /// Interaction logic for All_Orders.xaml
    /// </summary>
    public partial class All_Orders : Window
    {
        BL.IBL bl;
        ObservableCollection<BE.Order> orders;
        public All_Orders()
        {
            InitializeComponent();
            bl = BL.FactoryBl.Instance;
            orders = new ObservableCollection<BE.Order>(bl.GetAllOrders());
            orderDataGrid.DataContext = orders;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource orderViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("orderViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // orderViewSource.Source = [generic data source]
        }
    }
}
