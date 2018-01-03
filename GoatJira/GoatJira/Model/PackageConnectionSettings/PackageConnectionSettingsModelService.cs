namespace GoatJira.Model.PackageConnectionSettings
{
    using GoatJira.Helpers;
    using System;

    class PackageConnectionSettingsModelService : IPackageConnectionSettingsModelService
    {
        private readonly EA.Package package;

        public PackageConnectionSettingsModelService(EA.Package Package)
        {
            package = Package;
        }

        private const string JIRAQueryTypeTaggedValueDefName = "JIRA_QueryDefinition";
        private const string JIRAQueryTypeTaggedValueTypeName = "JIRA_QueryType";

        public PackageConnectionSettingsModel Read()
        {
            PackageConnectionSettingsModel result = new PackageConnectionSettingsModel();
            PackageConnectionSettingsType type;

            if (Enum.TryParse(EAUtils.ReadTaggedValue(package.Element.TaggedValues, JIRAQueryTypeTaggedValueTypeName, ""), out type))
                result.Type = type;
            else
                result.Type = PackageConnectionSettingsType.Jql;

            string QueryValue = EAUtils.ReadTaggedValue(package.Element.TaggedValues, JIRAQueryTypeTaggedValueDefName, "");

            result.Jql = result.EpicsAndStoriesJql = result.UserSavedSearch = "";
            switch (result.Type)
            {
                case PackageConnectionSettingsType.Jql:
                    result.Jql = QueryValue;
                    break;
                case PackageConnectionSettingsType.EpicsAndStories:
                    result.EpicsAndStoriesJql = QueryValue;
                    break;
                case PackageConnectionSettingsType.UserSearch:
                    result.UserSavedSearch = QueryValue;
                    break;
                default:
                    throw new NotImplementedException();
            }
            return result;
        }

        public void Save(PackageConnectionSettingsModel PackageConnectionSettings)
        {
            string QueryValue; 
            switch (PackageConnectionSettings.Type)
            {
                case PackageConnectionSettingsType.Jql:
                    QueryValue = PackageConnectionSettings.Jql;
                    break;
                case PackageConnectionSettingsType.EpicsAndStories:
                    QueryValue = PackageConnectionSettings.EpicsAndStoriesJql;
                    break;
                case PackageConnectionSettingsType.UserSearch:
                    QueryValue = PackageConnectionSettings.UserSavedSearch;
                    break; 
                default:
                    throw new NotImplementedException();
            }

            EAUtils.WriteTaggedValue(package.Element.TaggedValues, JIRAQueryTypeTaggedValueDefName, QueryValue);
            EAUtils.WriteTaggedValue(package.Element.TaggedValues, JIRAQueryTypeTaggedValueTypeName, PackageConnectionSettings.Type.ToString());
            package.Update();
        }
    }
}
