namespace GoatJira.Model.Jira.JiraIssueType
{
    using Atlassian.Jira;
    using System;

    class JiraIssueTypeModelService : IJiraIssueTypeModelService
    {
        private readonly IssueType issueType;

        public JiraIssueTypeModelService(IssueType IssueType)
            => issueType = IssueType;

        public JiraIssueTypeModel Read() =>
            new JiraIssueTypeModel()
            {
                Name = issueType.Name,
                Id = issueType.Id,
                IsSubTask = issueType.IsSubTask,
                Description = issueType.Description,
                IconUrl = issueType.IconUrl
            };

        public void Save(JiraIssueTypeModel JiraIssueType)
        {
            throw new NotImplementedException();
        }
    }
}
