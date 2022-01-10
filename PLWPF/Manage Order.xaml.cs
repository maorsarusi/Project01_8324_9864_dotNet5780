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
using System.Net.Mail;
using System.ComponentModel;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Manage_Order.xaml
    /// </summary>
    public partial class Manage_Order : Window
    {
        BE.Order order;
        BL.IBL bl;
        BackgroundWorker worker;
        public Manage_Order(int key)
        {
            InitializeComponent();
            bl = BL.FactoryBl.Instance;
            worker = new BackgroundWorker();
            worker.DoWork += Worker_Do_Working;
            order = bl.GetOrder(key);
            if (order.StatusOrder == BE.OrderStatus.SendEmail)
                SendEmailButton.IsEnabled = false;
            if (order.StatusOrder == BE.OrderStatus.Responsiveness)
                CloseOrderButton.IsEnabled = false;
            DetailsGrid.DataContext = order;
        }

        private void Worker_Do_Working(object sender, DoWorkEventArgs e)
        {
            try
            {
                bl.SendMail(order);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource orderViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("orderViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // orderViewSource.Source = [generic data source]
        }

        private void SendEmailClick(object sender, RoutedEventArgs e)
        {
            try
            {
                worker.RunWorkerAsync();
                bl.UpdateOrderStatus(order, BE.OrderStatus.SendEmail);
                MessageBox.Show("אימייל נשלח ללקוח המתאים");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CloseOrderClick(object sender, RoutedEventArgs e)
        {
            bl.UpdateOrderStatus(order, BE.OrderStatus.Responsiveness);
            MessageBox.Show("ההזמנה נסגרה בהצלחה ");
            this.Close();
        }
    }
}
