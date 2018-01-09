namespace GoatJira.Model.Jira.JiraIssueType
{
    interface IJiraIssueTypeModelService
    {
        JiraIssueTypeModel Read();
        void Save(JiraIssueTypeModel JiraIssueType);
    }
}
