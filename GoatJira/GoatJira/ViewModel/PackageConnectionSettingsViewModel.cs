namespace GoatJira.ViewModel
{
    using GalaSoft.MvvmLight;
    using GoatJira.Helpers;
    using GoatJira.Model.PackageConnectionSettings;

    class PackageConnectionSettingsViewModel : ViewModelBase
    {
        private IPackageConnectionSettingsModelService PackageConnectionSettingsModelService { get; }
        private IDialogService DialogService { get; }

        public PackageConnectionSettingsModel PackageConnectionSettings { get; private set; }


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

        public void EditPackageConnectionSettings()
        {
            PackageConnectionSettings = PackageConnectionSettingsModelService.Read();
            if (DialogService.ShowPackageConnectionSettingsDialog(this))
                PackageConnectionSettingsModelService.Save(PackageConnectionSettings);
        }




    }
}
