namespace GoatJira.ViewModel
{
    using GalaSoft.MvvmLight;
    using GoatJira.Helpers;
    using GoatJira.Model.Jira.JiraIssue;

    class JiraIssueViewModel : ViewModelBase
    {
        private readonly IJiraIssueModelService jiraIssueModelService;
        private readonly IDialogService dialogService;

        public JiraIssueModel JiraIssue { get; private set; }

        public JiraIssueViewModel(): this 
            (
                IsInDesignModeStatic ? (IJiraIssueModelService)new Design.DesignJiraIssueModelService() : new JiraIssueModelService(null), 
                new DialogService()
            )
        {
#if DEBUG
            if (IsInDesignMode)
                JiraIssue = jiraIssueModelService.Read();
#endif
        }

        public JiraIssueViewModel(IJiraIssueModelService JiraIssueModelService, IDialogService DialogService)
        {
            jiraIssueModelService = JiraIssueModelService;
            dialogService = DialogService;
            JiraIssue = jiraIssueModelService.Read();
        }




    }
}
