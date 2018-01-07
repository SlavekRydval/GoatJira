using Newtonsoft.Json;
using Atlassian.Jira;

namespace GoatJira.Model.Jira.JiraPriority
{
    /// <summary>
    /// Priority as it is defined at JIRA server
    /// </summary>
    class JiraPriority
    {
        /// <summary>
        /// Name of the priority
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Id of the priority
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// URL of the icon related to the priority
        /// </summary>
        public string IconURL { get; private set; }
        /// <summary>
        /// Description of the priority
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Creates empty JiraPriority class
        /// </summary>
        [JsonConstructor]
        private JiraPriority()
        {
        }

        /// <summary>
        /// Creates JiraPriority class and fill properties with values given in value parameter
        /// </summary>
        /// <param name="value">Priority as defined in JIRA library</param>
        public JiraPriority(IssuePriority value)
        {
            Name = value.Name;
            Id = value.Id;
            IconURL = value.IconUrl;
            Description = value.Description;
        }
    }
}
