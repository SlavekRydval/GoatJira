namespace GoatJira.Model.Jira.JiraIssue
{
    interface IJiraIssueModelService
    {
        JiraIssueModel Read();
        void Save(JiraIssueModel LoginInformation);
    }
}
