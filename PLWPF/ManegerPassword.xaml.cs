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
    /// Interaction logic for ManegerPassword.xaml
    /// </summary>
    public partial class ManegerPassword : Window
    {
        public ManegerPassword()
        {
            InitializeComponent();          
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password == "1234")
            {
                Manager manager = new Manager();
                this.Close();
                manager.ShowDialog();             
            }
            else
                MessageBox.Show("הסיסמא אינה נכונה");
        }
    }
}
