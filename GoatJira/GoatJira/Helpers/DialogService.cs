namespace GoatJira.Helpers
{
    using GoatJira.ViewModel;
    using System.Windows;

    class DialogService : IDialogService
    {
        public void ShowError(string Message) =>
            MessageBox.Show(Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        public void ShowMessage(string Message) =>
            MessageBox.Show(Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);

        public void ShowWarning(string Message) =>
            MessageBox.Show(Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

        public void ShowAboutDialog(object DataContext) =>
            (new View.About { DataContext = DataContext }).ShowDialog();

        public bool ShowPackageConnectionSettingsDialog(object DataContext) =>
            (new View.PackageConnectionSettings { DataContext = DataContext }).ShowDialog() == true ? true : false;

        public bool ShowSetLoginInformationDialog(LoginInformationViewModel DataContext) 
        {
            bool result; 
            var LoginInformationDlg = new View.LoginInformation() { DataContext = DataContext};
            if (DataContext.LoginInformation.SavePassword)
                LoginInformationDlg.txtPassword.Password = DataContext.LoginInformation.Password;
            result = LoginInformationDlg.ShowDialog() == true ? true : false;
            if (result && DataContext.LoginInformation.SavePassword)
                DataContext.LoginInformation.Password = LoginInformationDlg.txtPassword.Password;
            return result; 
        }

        public void ShowJiraIssue(JiraIssueViewModel JiraIssueDataContext) =>
            (new View.JiraIssue { DataContext = JiraIssueDataContext }).ShowDialog();

    }
}
