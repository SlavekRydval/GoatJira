namespace GoatJira.Services
{
    using Atlassian.Jira;
    using GoatJira.Model.Jira.JiraIssueType;
    using GoatJira.Model.Jira.JiraPriority;
    using GoatJira.ViewModel;
    using System.Collections.Generic;

    class JiraConnection
    {
        //properties
        public List<JiraPriorityModel> Priorities { get; protected set; } = new List<JiraPriorityModel>();
        public Dictionary<string, JiraIssueTypeModel> Types { get; protected set; } = new Dictionary<string, JiraIssueTypeModel>();
        public bool isConnected { get; protected set; } = false;

        //variables
        protected Jira jira = null;

        protected void ReadListsOfValues()
        {
            Types.Clear();
            foreach (var issueType in jira.IssueTypes.GetIssueTypesAsync().Result)
                Types.Add(issueType.Id, new JiraIssueTypeModelService(issueType).Read());

            Priorities.Clear();
            foreach (var prio in jira.Priorities.GetPrioritiesAsync().Result)
                Priorities.Add(new JiraPriorityModelService(prio).Read());
        }

        public void Login(LoginInformationViewModel LoginInformation,  bool ForceNewLogin = false)
        {
            if (!isConnected || ForceNewLogin)
            {
                jira = Jira.CreateRestClient(LoginInformation.LoginInformation.URI, LoginInformation.LoginInformation.Username, LoginInformation.LoginInformation.Password);
                jira.RestClient.RestSharpClient.Proxy = System.Net.WebRequest.GetSystemWebProxy();
                jira.RestClient.RestSharpClient.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

                ReadListsOfValues();
                isConnected = true;
            }
        }

        public IPagedQueryResult<Issue> GetJiraIssues(string Jql, int MaxItems = 1000)
        {
            return jira.Issues.GetIssuesFromJqlAsync(Jql, MaxItems).Result;
        }

    }
}
