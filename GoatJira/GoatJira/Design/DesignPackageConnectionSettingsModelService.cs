
namespace GoatJira.Design
{
    using GoatJira.Model.PackageConnectionSettings;

    class DesignPackageConnectionSettingsModelService : IPackageConnectionSettingsModelService
    {
        public PackageConnectionSettingsModel Read()
        {
            return new PackageConnectionSettingsModel()
            {
                Type = PackageConnectionSettingsType.EpicsAndStories,
                EpicsAndStoriesJql = "createdby = myself",
                Jql = "type = technical and labels = goatjira",
                UserSavedSearch = ""
            };
        }
    }
}
