﻿using System;
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
    /// Interaction logic for All_Hostinng_Units.xaml
    /// </summary>
    public partial class All_Hostinng_Units : Window
    {
        BL.IBL bl;
        ObservableCollection<BE.HostingUnit> hostingUnits;
        public All_Hostinng_Units()
        {
            InitializeComponent();
            bl = BL.FactoryBl.Instance;
            hostingUnits = new ObservableCollection<BE.HostingUnit>(bl.GetAllUnits());
            hostingUnitDataGrid.DataContext = hostingUnits;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource hostingUnitViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("hostingUnitViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // hostingUnitViewSource.Source = [generic data source]
        }
    }
}
