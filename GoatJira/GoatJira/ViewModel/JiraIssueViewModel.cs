using GalaSoft.MvvmLight;
using GoatJira.Helpers;
using GoatJira.Model.JiraIssue;

namespace GoatJira.ViewModel
{
    class JiraIssueViewModel : ViewModelBase
    {
        private readonly IJiraIssueModelService jiraIssueModelService;
        private readonly IDialogService dialogService;

        public JiraIssueModel JiraIssue { get; private set; }

        public JiraIssueViewModel(): this 
            (
                IsInDesignModeStatic ? (IJiraIssueModelService)new Design.DesignJiraIssueModelService() : new JiraIssueModelService(), 
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
        }




    }
}
