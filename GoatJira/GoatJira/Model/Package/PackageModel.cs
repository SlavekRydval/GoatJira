
namespace GoatJira.Model.Package
{
    using GalaSoft.MvvmLight;
    using GoatJira.Model.PackageConnectionSettings;

    class PackageModel : ObservableObject
    {
        private string _GUID;
        private PackageConnectionSettingsModel _ConnectionSettings = new PackageConnectionSettingsModel();

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
        public PackageConnectionSettingsModel ConnectionSettings
        {
            get => _ConnectionSettings;
        }
    }
}
