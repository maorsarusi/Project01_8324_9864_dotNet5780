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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for NumericUpDownControl.xaml
    /// </summary>
    public partial class NumericUpDownControl : UserControl
    { 
        private int? num = null;
        public int? Value
        {
            get { return num; }
            set
            {
                if (value > MaxValue)
                    num = MaxValue;
                else if (value < MinValue)
                    num = MinValue;
                else
                    num = value;
                txtNum.Text = num == null ? "" : num.ToString();
            }
        }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public NumericUpDownControl()
        {
            InitializeComponent(); MaxValue = 100;
        }
        private void cmdUp_Click(object sender, RoutedEventArgs e)
        { Value++; }
        private void cmdDown_Click(object sender, RoutedEventArgs e)
        { Value--; }
        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNum == null || txtNum.Text == "" || txtNum.Text == "-")
            {
                Value = null;
                return;
            }
            int val;
            if (!int.TryParse(txtNum.Text, out val))
                txtNum.Text = Value.ToString();
            else Value = val;
        }
    }
}
