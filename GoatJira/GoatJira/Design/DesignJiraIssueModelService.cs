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
            UpdatedAt = DateTime.Now,
            DefinitionOfDone = "The team agrees on, and displays prominently somewhere in the team room, a list of criteria which must be met before a product increment \"often a user story\" is considered \"done\". Failure to meet these criteria at the end of a sprint normally implies that the work should not be counted toward that sprint's velocity.\n\nhttps://www.agilealliance.org/glossary/definition-of-done",
            AcceptanceCriteria = "The goals of Acceptance Criteria are:\n"+
                                 "* to clarify what the team should build before they start work\n" +
                                 "* to ensure everyone has a common understanding of the problem\n" +
                                 "* to help the team members know when the Story is complete\n" +
                                 "* to help verify the Story via automated tests.\n\n" +
                                 "https://agilepainrelief.com/notesfromatooluser/2017/05/definition-of-done-vs-user-stories-vs-acceptance-criteria.html#.WmTxqTciHmE"
        };

        public void Save(JiraIssueModel LoginInformation)
        {
            throw new NotImplementedException();
        }
    }
}
