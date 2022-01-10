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
    /// Interaction logic for Hosting_Unit.xaml
    /// </summary>
    public partial class Hosting_Unit : Window
    {
        public Hosting_Unit()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Parse add_Hosting_Unit = new Parse();
            add_Hosting_Unit.ShowDialog();
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void passwordClick(object sender, RoutedEventArgs e)
        {
            Owner_password owner_Password = new Owner_password();
            owner_Password.ShowDialog();
        }

        //מיועד לתווית בעמוד יחידת אירוח
        private void Label_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AreaLabel.FontSize = 20;         
        }

        private void AreaLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            AreaLabel.FontSize = 12;
        }
    }
}