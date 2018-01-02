namespace GoatJira.ViewModel
{
    using GalaSoft.MvvmLight;
    using GoatJira.Helpers;
    using GoatJira.Model.PackageConnectionSettings;

    class PackageConnectionSettingsViewModel : ViewModelBase
    {
        public IPackageConnectionSettingsModelService PackageConnectionSettingsModelService { get; }
        public IDialogService DialogService { get; }

        public PackageConnectionSettingsModel PackageConnectionSettings { get; }


        public PackageConnectionSettingsViewModel() : this(
            IsInDesignModeStatic ? (IPackageConnectionSettingsModelService)new Design.DesignPackageConnectionSettingsModelService() : new PackageConnectionSettingsModelService(), 
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

    }
}
