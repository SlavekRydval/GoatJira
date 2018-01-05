﻿namespace GoatJira.ViewModel
{
    using System;
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
