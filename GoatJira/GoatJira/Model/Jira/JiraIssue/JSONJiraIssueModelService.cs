namespace GoatJira.Model.Jira.JiraIssue
{
    using Newtonsoft.Json;
    using System;

    class JSONJiraIssueModelService : IJiraIssueModelService
    {
        string JSONJiraIssue; 

        public JSONJiraIssueModelService(string JSONJiraIssue) =>
            this.JSONJiraIssue = JSONJiraIssue;

        public JiraIssueModel Read() =>
            JsonConvert.DeserializeObject<JiraIssueModel>(JSONJiraIssue);

        public void Save(JiraIssueModel LoginInformation)
        {
            throw new NotImplementedException();
        }
    }
}
