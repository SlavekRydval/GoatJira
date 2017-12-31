using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            //when reusing, don't forget add reference to System.Deployement.
            int Revision = (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed) ?
                System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Revision :
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Revision;

            int Major = (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed) ?
                System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Major :
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major;

            int Minor = (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed) ?
                System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Minor :
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor;

             
            lblMainCaption.Content = String.Format(lblMainCaption.Content.ToString(), Major, Minor, Revision);
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
