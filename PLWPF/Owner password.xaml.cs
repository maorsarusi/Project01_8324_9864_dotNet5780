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
    /// Interaction logic for Owner_password.xaml
    /// </summary>
    public partial class Owner_password : Window
    {
        BL.IBL bl;
        public Owner_password()
        {
            InitializeComponent();
         
            bl = BL.FactoryBl.Instance;
            HostingUnitNameComboBox.ItemsSource = bl.GetAllUnits().ToList();
            HostingUnitNameComboBox.DisplayMemberPath = " HostingUnitName";
            HostingUnitNameComboBox.SelectedValuePath = "HostingUnitKey";
        }

        private void PersonslAreaButton(object sender, RoutedEventArgs e)
        {
           
            string password = PasswordBoxUnitKey.Password;
            try
            {               
                if (password != HostingUnitNameComboBox.SelectedValue.ToString())
                    MessageBox.Show("הסיסמא אינה מתאימה");
                else
                {
                    Owner_s_personal_area owner_S_Personal_Area = new Owner_s_personal_area(int.Parse(HostingUnitNameComboBox.SelectedValue.ToString()));
                    this.Close();
                    owner_S_Personal_Area.ShowDialog();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message); 
            }         
        }
    }
}
