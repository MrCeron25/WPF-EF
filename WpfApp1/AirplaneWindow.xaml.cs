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

namespace WpfApp1
{
    public partial class AirplaneWindow : Window
    {
        public AirplaneWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CheckButton()
        {
            int number = -1;
            bool IsNumber = int.TryParse(NumberOfSeats.Text, out number);
            if (!string.IsNullOrEmpty(AirplaneModelName.Text) &&
                char.IsUpper(AirplaneModelName.Text[0]) &&
                IsNumber &&
                number > 0)
            {
                Button.IsEnabled = true;
            }
            else
            {
                Button.IsEnabled = false;
            }
        }

        private void AirplaneModelName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckButton();
        }

        private void NumberOfSeats_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckButton();
        }
    }
}
