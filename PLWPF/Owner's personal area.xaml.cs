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
    /// Interaction logic for Owner_s_personal_area.xaml
    /// </summary>
    public partial class Owner_s_personal_area : Window
    {
        BL.IBL bl;
        BE.HostingUnit unit;
        BE.Order order;

        ObservableCollection<BE.GuestRequest> guestRequests;
        ObservableCollection<BE.Order> orders;
        List<BE.Order> guests;

        private ObservableCollection<BE.Order> _myCollection =
        new ObservableCollection<BE.Order>();
        public Owner_s_personal_area(int key)
        {
            InitializeComponent();
            bl = BL.FactoryBl.Instance;
            unit = bl.GetHostingUnit(key);

            string str = unit.Owner.PrivateName + " " + unit.Owner.FamilyName;
            HostsNameTextBox.Text = str;
            typeTextBox.Text = unit.Type.ToString();
            AreaTextBox.Text = unit.Area.ToString();
            capacitySize.Value = unit.Capacity;
            HostingUnitDetailsGrid.DataContext = unit;

            guestRequests = new ObservableCollection<BE.GuestRequest>(bl.GetAllGuestRequests());
            orders = new ObservableCollection<BE.Order>(bl.OrdersForUnit(unit));
            AllGuestRequestsComboBox.ItemsSource = guestRequests;
            OrdersOfUnitComboBox.ItemsSource = orders;
            orderDataGrid.DataContext = orders;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource orderViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("orderViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // orderViewSource.Source = [generic data source]
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("?האם אתה בטוח", " מחיקת יחידה", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        bl.DelUnit(unit);
                        MessageBox.Show("היחידה נמחקה בהצלחה");
                        this.Close();
                        break;
                    case MessageBoxResult.No:
                        MessageBox.Show("היחידה לא נמחקה");
                        break;

                }

            }
            
            catch (BE.MissingKeyException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void QueryClick(object sender, RoutedEventArgs e)
        {
            Mathing_Guest_Requests query = new Mathing_Guest_Requests(unit);
            query.Show();
        }

        private void UpdateClickButton(object sender, RoutedEventArgs e)
        {
            try
            {
                
                unit.Capacity = (int)capacitySize.Value;
                if (unit.Capacity == 0)
                    MessageBox.Show("תכולה לא יכולה להיות פחותה מאחד");
                else
                {
                    DataContext = unit;
                    bl.UpdateUnit(unit);
                    MessageBox.Show("היחידה עודכנה בהצלחה");
                    this.Close();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void AddOrderClick(object sender, RoutedEventArgs e)
        {
            try
            {
                BE.Order order = new BE.Order();
                order.HostingUnitKey = unit.HostingUnitKey;
                BE.GuestRequest guestRequest = (BE.GuestRequest)AllGuestRequestsComboBox.SelectedItem;
                if (AllGuestRequestsComboBox.SelectedValue == null)
                {
                    MessageBox.Show("לא נבחר דבר");
                }
                else
                {
                    guests = bl.GetAllOrders().ToList();
                    order.GuestRequestKey = guestRequest.GuestRequestKey;
                    int index = -1;
                    for (int i = 0; i < guests.Count(); i++)
                    {
                        if (guests[i].GuestRequestKey == order.GuestRequestKey)
                        {
                            index = i;
                            break;
                        }
                    }
                    if (index != -1)
                    {
                        MessageBox.Show("הזמנה זו כבר קיימת במערכת");
                    }
                    else
                    {
                        order.OrderDate = DateTime.Now;

                        bl.AddOrder(order);
                        orderDataGrid.ItemsSource = bl.OrdersForUnit(unit);
                        OrdersOfUnitComboBox.ItemsSource = orders;
                        MessageBox.Show("נוצרה הזמנה חדשה");
                        MessageBoxResult result = MessageBox.Show("האם ברצונך לטפל בהזמנה כעת", "הודעת מערכת", MessageBoxButton.YesNo);
                        switch (result)
                        {

                            case MessageBoxResult.Yes:

                                int key = order.OrderKey;
                                Manage_Order manage_order = new Manage_Order(key);
                                this.Close();
                                manage_order.Show();
                                break;
                            case MessageBoxResult.No:
                                break;

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void EnterOrderClick(object sender, RoutedEventArgs e)
        {
            if (OrdersOfUnitComboBox.SelectedItem == null)
                MessageBox.Show("לא נבחר דבר");
            else
            {
                order = (BE.Order)OrdersOfUnitComboBox.SelectedItem;
                if (order.StatusOrder == BE.OrderStatus.Responsiveness)
                {
                    MessageBox.Show("לא ניתן לשנות סטטוס עסקה סגורה");
                }
                else
                {
                    int key = order.OrderKey;
                    Manage_Order manage_order = new Manage_Order(key);
                    this.Close();
                    manage_order.Show();
                }
            }


        }
    }
}
