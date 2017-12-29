namespace GoatJira.Model
{
    using GalaSoft.MvvmLight;

    class PackageModel: ObservableObject
    {
        private string _GUID;
        private PackageConnectionSettingModel _ConnectionSettings = new PackageConnectionSettingModel();

        /// <summary>
        /// GUID of the package that is synchronized with JIRA
        /// </summary>
        public string GUID
        {
            get => _GUID;
            set => Set(nameof(GUID), ref _GUID, value);
        }

        /// <summary>
        /// Settings of the connection (query)
        /// </summary>
        public PackageConnectionSettingModel ConnectionSettings
        {
            get => _ConnectionSettings;
        }
    }
}
