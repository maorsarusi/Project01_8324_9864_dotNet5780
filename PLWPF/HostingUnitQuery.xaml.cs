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
    /// Interaction logic for HostingUnitQuery.xaml
    /// </summary>
    public partial class HostingUnitQuery : Window
    {
        BL.IBL bl;
        BE.HostingUnit unit;
        public HostingUnitQuery()
        {
            InitializeComponent();
            bl =  BL.FactoryBl.Instance;
            hukeyComboBox.ItemsSource= bl.GetAllUnits().ToList();
            hukeyComboBox.DisplayMemberPath = "HostingUnitKey";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource orderViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("orderViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // orderViewSource.Source = [generic data source]
        }

        private void selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            unit = (BE.HostingUnit)hukeyComboBox.SelectedItem;
            orderDataGrid.DataContext = bl.OrdersForUnit(unit);
        }
    }
}
