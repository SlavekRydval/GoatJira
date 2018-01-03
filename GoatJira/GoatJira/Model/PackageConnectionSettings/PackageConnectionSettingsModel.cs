namespace GoatJira.Model.PackageConnectionSettings
{
    using GalaSoft.MvvmLight;

    /// <summary>
    /// PackageConnectionSettingsType indicates, how did user specified the query to the JIRA issues.
    /// Jql = pure JQL query
    /// EpicsAndStories = defined epics and all their stories
    /// UserSearch = user defined search that is saved on JIRA server
    /// </summary>
    enum PackageConnectionSettingsType { Jql, EpicsAndStories, UserSearch }

    class PackageConnectionSettingsModel: ObservableObject
    {
        private string _Jql;
        private string _EpicsAndStoriesJql;
        private string _UserSavedSearch;
        private PackageConnectionSettingsType _Type;

        /// <summary>
        /// Definition of JQL for specification of the query. Use pure JQL syntax for the definiton.
        /// </summary>
        public string Jql
        {
            get => _Jql;
            set => Set(nameof(Jql), ref _Jql, value);
        }

        /// <summary>
        /// The result of the query will be this JQL: type = epic and ('part of JQL defined in EpicsAndStoriesJql')
        /// </summary>
        public string EpicsAndStoriesJql
        {
            get => _EpicsAndStoriesJql;
            set => Set(nameof(EpicsAndStoriesJql), ref _EpicsAndStoriesJql, value);
        }

        /// <summary>
        /// Now unsupported... 
        /// </summary>
        public string UserSavedSearch
        {
            get => _UserSavedSearch;
            set => Set(nameof(UserSavedSearch), ref _UserSavedSearch, value);
        }
        
        /// <summary>
        /// Type of query that will be used within synchronization
        /// </summary>
        public PackageConnectionSettingsType Type
        {
            get => _Type;
            set => Set(nameof(Type), ref _Type, value);
        }
    }
}
