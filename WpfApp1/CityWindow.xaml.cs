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
    public partial class CityWindow : Window
    {

        private void UpdateCountries()
        {
            Countries.ItemsSource = Data.GetCountriesNames();
        }

        public CityWindow()
        {
            InitializeComponent();
            UpdateCountries();
        }

        private void CheckButton()
        {
            if (!string.IsNullOrEmpty(CityName.Text) &&
                char.IsUpper(CityName.Text[0]) &&
                Countries.SelectedItem != null)
            {
                Button.IsEnabled = true;
            }
            else
            {
                Button.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CityName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckButton();
        }

        private void Countries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
        }
    }
}
