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
    /// Interaction logic for EntryNumberForQuery.xaml
    /// </summary>
    public partial class EntryNumberForQuery : Window
    {
        //חלון מקדים לשאילתא לצורך הכנסת מספר ימים
        int index, index1;
        public EntryNumberForQuery()
        {
            InitializeComponent();          
        }

        private void EntryQueryClick(object sender, RoutedEventArgs e)
        {
            index =(int)creation.Value;
            index1 = (int)sendMail.Value;
            OrderListQuery orderListQuery = new OrderListQuery(index, index1);
            orderListQuery.ShowDialog();
            this.Close();
        }

    }
}
