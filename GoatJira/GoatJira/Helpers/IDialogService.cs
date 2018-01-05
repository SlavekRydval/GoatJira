namespace GoatJira.Helpers
{
    using GoatJira.ViewModel;

    interface IDialogService
    {

        void ShowMessage(string Message);
        void ShowError(string Message);
        void ShowWarning(string Message);

        void ShowAboutDialog(object DataContext);

        /// <summary>
        /// Shows dialog for changing the package connection settings data
        /// </summary>
        /// <param name="DataContext"></param>
        /// <returns>true if data was changed</returns>
        bool ShowPackageConnectionSettingsDialog(object DataContext);

        bool ShowSetLoginInformationDialog(LoginInformationViewModel DataContext);

        void ShowJiraIssue(JiraIssueViewModel DataContext);

    }
}
