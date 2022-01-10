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
    /// Interaction logic for Add_Hosting_Unit.xaml
    /// </summary>
    public partial class Parse : Window
    {
        //List<BE.HostingUnit> units;
        BE.HostingUnit hostingUnit;
        BL.IBL bl;
        BE.Host host;

        public Parse()
        {
            InitializeComponent();
            hostingUnit = new BE.HostingUnit();
           
            DataContext = hostingUnit;
            this.bl = BL.FactoryBl.Instance;
            List<BE.Host> hosts = bl.GetAllHosts().ToList();
            List<string> Names = new List<string>() ;
            for (int i = 0; i < hosts.Count(); i++)
            {
                Names.Add(hosts[i].PrivateName + " " +hosts[i].FamilyName);
            }
            this.HostsNameComboBox.ItemsSource=Names;      
            this.areaComboBox.ItemsSource = Enum.GetValues(typeof(BE.Areas));
            this.typeComboBox.ItemsSource = Enum.GetValues(typeof(BE.ResortType));         
        }
        

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (HostsNameComboBox.SelectedItem == null || typeComboBox.SelectedValue == null || hostingUnitNameTextBox.Text == null || areaComboBox.SelectedValue == null)
                    throw new Exception("יש למלא את כל הפרטים תחילה");
                else if (capacitySize.Value == 0)
                    throw new Exception("יש למלא תפוסת יחידה תחילה היא לא יכולה להיות 0");

                //מציאת המארח ע"פ מיקומו ברשימה והכנסת שמו לרכיב המתאים
                List<BE.Host> hosts = bl.GetAllHosts().ToList();
                int index = 0;
                    string str=(string)HostsNameComboBox.SelectedValue;
                for (int i = 0; i <hosts.Count() ; i++)
                {
                    if (hosts[i].WholeName==str)
                    {
                        index = i;
                        break;
                    }
                }
                int key = hosts[index].HostKey;
                host = bl.GetHostByKey(key);
                hostingUnit.Owner = host;
                hostingUnit.Area= (BE.Areas)areaComboBox.SelectedItem;
                hostingUnit.Type = (BE.ResortType)typeComboBox.SelectedItem;
                hostingUnit.Capacity = (int)capacitySize.Value;
                bl.AddUnit(hostingUnit);
                MessageBox.Show(" היחידה נרשמה במאגר בהצלחה");
                MessageBox.Show(":סיסמת היחידה היא" + hostingUnit.HostingUnitKey);
                this.Close();
            }
            catch (BE.DuplicateKeyException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
