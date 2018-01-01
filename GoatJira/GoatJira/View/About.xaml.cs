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
            this.DataContext = DataContext;
            InitializeComponent();
        }

        ///TODO: Is this valid MVVM pattern? Probably no, just examine the topic later on.
        private void Button_Click(object sender, RoutedEventArgs e) => Close();
    }
}
