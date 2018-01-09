namespace GoatJira.Model.Jira.JiraIssueType
{
    using GalaSoft.MvvmLight;

    /// <summary>
    /// JIRA Issue Model Class
    /// </summary>
    public class JiraIssueTypeModel: ObservableObject
    {
        private string _name;
        private string _id;
        private bool _isSubTask;
        private string _description;
        private string _iconUrl;

        /// <summary>
        /// Name of the type
        /// </summary>
        public string Name { get => _name; set => Set(nameof(Name), ref _name, value); }
        /// <summary>
        /// Id of the type
        /// </summary>
        public string Id { get => _id; set => Set(nameof(Id), ref _id, value); }
        /// <summary>
        /// Indicates wheather it issue type is a subtask
        /// </summary>
        public bool IsSubTask { get => _isSubTask; set => Set(nameof(IsSubTask), ref _isSubTask, value); }
        /// <summary>
        /// Description of an issue type
        /// </summary>
        public string Description { get => _description; set => Set(nameof(Description), ref _description, value); }
        /// <summary>
        /// IconUrl of an issue type
        /// </summary>
        public string IconUrl { get => _iconUrl; set => Set(nameof(IconUrl), ref _iconUrl, value); }

        /// <summary>
        /// Creates an empty instance of JiraIssueTypeModel class
        /// </summary>
        public JiraIssueTypeModel()
        {
        }

    }
}
