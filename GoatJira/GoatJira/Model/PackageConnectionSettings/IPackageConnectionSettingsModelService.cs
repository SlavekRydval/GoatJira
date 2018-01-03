namespace GoatJira.Model.PackageConnectionSettings
{
    interface IPackageConnectionSettingsModelService
    {
        PackageConnectionSettingsModel Read();
        void Save(PackageConnectionSettingsModel PackageConnectionSettings);
    }
}
