namespace GoatJira.Model.Jira.JiraPriority
{
    using Newtonsoft.Json;
    using GalaSoft.MvvmLight;

    /// <summary>
    /// JIRA Priority Model Class
    /// </summary>
    class JiraPriorityModel: ObservableObject
    {
        private string name;
        private string id;
        private string iconURL;
        private string description;

        /// <summary>
        /// Name of the priority
        /// </summary>
        public string Name
        {
            get => name;
            set => Set(nameof(Name), ref name, value);
        }
        /// <summary>
        /// Id of the priority
        /// </summary>
        public string Id
        {
            get => id;
            set => Set(nameof(Id), ref id, value);
        }
        /// <summary>
        /// URL of the icon related to the priority
        /// </summary>
        public string IconURL
        {
            get => iconURL;
            set => Set(nameof(IconURL), ref iconURL, value);
        }
        /// <summary>
        /// Description of the priority
        /// </summary>
        public string Description
        {
            get => description;
            set => Set(nameof(Description), ref description, value);
        }

        /// <summary>
        /// Creates empty JiraPriority class
        /// </summary>
        [JsonConstructor]
        public JiraPriorityModel()
        {
        }

    }
}
