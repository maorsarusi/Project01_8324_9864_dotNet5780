using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Add_Guest_Request.xaml
    /// </summary>
    public partial class Add_Guest_Request : Window
    {

        BE.GuestRequest guestRequest;
        BL.IBL bl;
        public Add_Guest_Request()
        {
            InitializeComponent();
            guestRequest = new BE.GuestRequest();
          
            bl = BL.FactoryBl.Instance;

            this.areaComboBox.ItemsSource = Enum.GetValues(typeof(BE.Areas));
            this.childrensAttractionsComboBox.ItemsSource = Enum.GetValues(typeof(BE.Options));
            this.gardenComboBox.ItemsSource = Enum.GetValues(typeof(BE.Options));
            this.jacuzziComboBox.ItemsSource = Enum.GetValues(typeof(BE.Options));
            this.poolComboBox.ItemsSource = Enum.GetValues(typeof(BE.Options));
            this.typeComboBox.ItemsSource = Enum.GetValues(typeof(BE.ResortType));

            this.DataContext = guestRequest;

        }

        //בדיקת תקינות קלט למייל
        private static bool ValidateMail(string emailAddress)
        {
            var regex = @"\A(?:[a-z0-9!#$%&'+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'+/=?^_`{|}~-]+)@(?:[a-z0-9](?:[a-z0-9-][a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            bool isValid = Regex.IsMatch(emailAddress, regex, RegexOptions.IgnoreCase);
            return isValid;
        }
        
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //בדיקת תקינות קלט לכל הרכיבים
                if (privateNameTextBox.Text == null || familyNameTextBox.Text == null || areaComboBox.SelectedValue == null || typeComboBox.SelectedValue == null)
                    throw new Exception("יש למלא את כל הפרטים");
                else if (numAdults.Value == 0)
                    throw new Exception("חייב להיות לפחות אדם אחד מעל גיל 18");

                else if (ValidateMail(mailAddressTextBox.Text))
                    throw new Exception("כתובת האימייל אינה תקינה");
                guestRequest.Area = (BE.Areas)areaComboBox.SelectedItem;
                guestRequest.Type = (BE.ResortType)typeComboBox.SelectedItem;

                int count = bl.NumberOfUnitsThatMatchingTheRequirement(guestRequest);
                if (count==0)
                {
                    MessageBoxResult result = MessageBox.Show("?אין יחידות מתאימות במערכת לדרישה זו,האם ברצונך לשנותה", "הודעת מערכת", MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            break;
                        case MessageBoxResult.No:
                            bl.AddGuestRequest(guestRequest);
                            this.Close();
                            break;                      
                    }
                }
                else
                {
                    bl.AddGuestRequest(guestRequest);
                    MessageBox.Show(" :מספר היחידות המתאים לדרישה זו הוא " + count);
                         MessageBox.Show("הבקשה נשלחה בהצלחה");
                    this.Close();
                }            
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
