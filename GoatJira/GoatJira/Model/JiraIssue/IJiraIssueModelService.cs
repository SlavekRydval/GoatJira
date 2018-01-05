namespace GoatJira.Model.JiraIssue
{
    interface IJiraIssueModelService
    {
        JiraIssueModel Read();
        void Save(JiraIssueModel LoginInformation);
    }
}
