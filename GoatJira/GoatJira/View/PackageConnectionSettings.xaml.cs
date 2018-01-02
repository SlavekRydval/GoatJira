using System;
using System.Windows;

namespace GoatJira.View
{
    public partial class PackageConnectionSettings : Window
    {
        public PackageConnectionSettings()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((rbJql.IsChecked == true) && String.IsNullOrWhiteSpace(tbUserDefinedJql.Text))
            {
                MessageBox.Show("Please enter the JQL", "JQL is missing", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                this.DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
