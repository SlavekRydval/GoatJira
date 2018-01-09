namespace GoatJira.Model.Jira.JiraPriority
{
    using Atlassian.Jira;
    using System;

    class JiraPriorityModelService : IJiraPriorityModelService
    {
        private readonly IssuePriority issuePriority;

        public JiraPriorityModelService(IssuePriority IssuePriority) =>
            issuePriority = IssuePriority;

        public JiraPriorityModel Read() =>
            new JiraPriorityModel()
            {
                Name = issuePriority.Name,
                Id = issuePriority.Id,
                IconURL = issuePriority.IconUrl,
                Description = issuePriority.Description
            };


        public void Save(JiraPriorityModel JiraPriority)
        {
            throw new NotImplementedException();
        }
    }
}
