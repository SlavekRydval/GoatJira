using System.Windows;

namespace GoatJira.View
{
    public partial class LoginInformation : Window
    {
        public LoginInformation()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
