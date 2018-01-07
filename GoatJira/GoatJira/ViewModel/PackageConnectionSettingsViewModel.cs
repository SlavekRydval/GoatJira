namespace GoatJira.ViewModel
{
    using System;
    using GalaSoft.MvvmLight;
    using GoatJira.Helpers;
    using GoatJira.Model.PackageConnectionSettings;

    class PackageConnectionSettingsViewModel : ViewModelBase
    {
        private IPackageConnectionSettingsModelService PackageConnectionSettingsModelService { get; }
        private IDialogService DialogService { get; }

        public PackageConnectionSettingsModel PackageConnectionSettings { get; private set; } = null; 

        /// <summary>
        /// Jql that return desired issues from JIRA
        /// </summary>
        public string Jql
        {
            get
            {
                if (PackageConnectionSettings == null)
                    PackageConnectionSettings = PackageConnectionSettingsModelService.Read();

                switch (PackageConnectionSettings.Type)
                {
                    case PackageConnectionSettingsType.Jql:
                        return PackageConnectionSettings.Jql;
                    case PackageConnectionSettingsType.EpicsAndStories:
                        if (String.IsNullOrWhiteSpace(PackageConnectionSettings.EpicsAndStoriesJql))
                            return "issuetype = Epic";
                        else
                            return "issuetype = Epic AND (" + PackageConnectionSettings.EpicsAndStoriesJql + ")";
                    case PackageConnectionSettingsType.UserSearch:
                        throw new NotImplementedException("UserSearch not implemented yet.");
                    default:
                        throw new NotImplementedException("Probably new type of query. Sorry, not implemented yet.");
                }
            }
        }

        public PackageConnectionSettingsViewModel() : this(
            IsInDesignModeStatic ? (IPackageConnectionSettingsModelService)new Design.DesignPackageConnectionSettingsModelService() : new PackageConnectionSettingsModelService(null), 
            new DialogService())
        {

        }

        public PackageConnectionSettingsViewModel(IPackageConnectionSettingsModelService PackageConnectionSettingsModelService,
            IDialogService DialogService)
        {
            this.PackageConnectionSettingsModelService = PackageConnectionSettingsModelService;
            this.DialogService = DialogService;

#if DEBUG
            if (IsInDesignMode)
                PackageConnectionSettings = this.PackageConnectionSettingsModelService.Read();
#endif
        }

        public bool EditPackageConnectionSettings()
        {
            bool result;
            PackageConnectionSettings = PackageConnectionSettingsModelService.Read();
            result = DialogService.ShowPackageConnectionSettingsDialog(this);
            if (result)
                PackageConnectionSettingsModelService.Save(PackageConnectionSettings);
            return result;
        }

        public bool AreDataSet()
        {
            PackageConnectionSettings = PackageConnectionSettingsModelService.Read();
            return (
                    (PackageConnectionSettings.Type == PackageConnectionSettingsType.Jql && !String.IsNullOrWhiteSpace(PackageConnectionSettings.Jql))
                    ||
                    (PackageConnectionSettings.Type == PackageConnectionSettingsType.EpicsAndStories && !String.IsNullOrWhiteSpace(PackageConnectionSettings.EpicsAndStoriesJql)));
        }
    }
}
