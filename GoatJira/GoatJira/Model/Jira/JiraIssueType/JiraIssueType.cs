namespace GoatJira.Model.Jira.JiraIssueType
{
    using Atlassian.Jira;
    using Newtonsoft.Json;

    /// <summary>
    /// Type that can be assigned to an issue
    /// </summary>
    public class JiraIssueType
    {
        /// <summary>
        /// Name of the type
        /// </summary>
        public string Name { get; protected set; }
        /// <summary>
        /// Id of the type
        /// </summary>
        public string Id { get; protected set; }
        /// <summary>
        /// Indicates wheather it issue type is a subtask
        /// </summary>
        public bool IsSubTask { get; protected set; }
        /// <summary>
        /// Description of an issue type
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// IconUrl of an issue type
        /// </summary>
        public string IconUrl { get; private set; }

        /// <summary>
        /// Creates an empty instance of this class
        /// </summary>
        [JsonConstructor]
        private JiraIssueType()
        {
        }

        /// <summary>
        /// Creates an instance of JiraIssueType and assignes values from IssueType to the properties
        /// </summary>
        /// <param name="value"></param>
        public JiraIssueType(IssueType value)
        {
            Name = value.Name;
            Id = value.Id;
            IsSubTask = value.IsSubTask;
            Description = value.Description;
            IconUrl = value.IconUrl;
        }
    }
}
