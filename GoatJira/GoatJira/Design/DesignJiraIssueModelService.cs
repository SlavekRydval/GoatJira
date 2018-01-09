using GoatJira.Model.Jira.JiraIssue;
using System;

namespace GoatJira.Design
{
    class DesignJiraIssueModelService : IJiraIssueModelService
    {
        public JiraIssueModel Read() => new JiraIssueModel() {
            Assignee = "Brouk Sáček",
            Components = "CRM, DWH",
            CreatedAt = DateTime.Now,
            CustomFields = null,
            Description = "asldf jakdaskf riu8t ajdbv urh\najfdgb drrrrrrrrrrrrrrrrrrr\nkghbeiru fdMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMvnbdksjhgb f",
            DueDate = DateTime.Now,
            Key = "key key key",
            Labels = "ABC, EFG, DFA",
            Priority = "No priority",
            Project = "GoatJira",
            ProjectType = "Type of project",
            Reporter = "Ferda Mravenec",
            Resolution = "res res",
            ResolutionDate = DateTime.Now,
            Status = "Almost done",
            Summary = "Short summary of this issue",
            Type = "Technical Story",
            UpdatedAt = DateTime.Now
        };

        public void Save(JiraIssueModel LoginInformation)
        {
            throw new NotImplementedException();
        }
    }
}
