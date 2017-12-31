using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;

namespace GoatJira.View
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About: Window
    {
        public About()
        {
            InitializeComponent();
        }

        public About(object DataContext)
        {
//            this.Resources.
            this.DataContext = DataContext;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(((Hyperlink)sender).NavigateUri.OriginalString);
        }
    }
}
