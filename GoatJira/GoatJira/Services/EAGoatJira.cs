namespace GoatJira.Services
{
    class EAGoatJira
    {
        //Following names have to correspond with names of stereotype attributes (names of tag values)
        public const string TagValueNameJiraKey = "JiraKey";
        public const string TagValueNameLastUpdateAt = "LastUpdateAt";
        public const string TagValueNameData = "Data";
        public const string TagValueNameOperation = "LastUpdateInfo";
        public const string TagValueNamePriority = "Priority";
        public const string TagValueNameStatus = "Status";
        public const string TagValueNameType = "Type";

        //Following names have to correspond with names of stereotypes defined in Profile
        public const string JiraIssueMetaType = "JiraIssue";
        public const string JiraEpicMetaType = "JiraEpic";
        public const string JiraStoryMetaType = "JiraStory";
        public const string JiraTaskMetaType = "JiraTask";
        public const string JiraSubtaskMetaType = "JiraSubtask";
        public const string JiraBugMetaType = "JiraBug";
        public const string JiraTechnicalStoryMetaType = "JiraTechnicalStory";

        //Following names have to correspond with names of Jira issue types
        //(these string could be variable and should be read from config file)
        public const string JiraBugTypeName = "bug";
        public const string JiraEpicTypeName = "epic";
        public const string JiraStoryTypeName = "story";
        public const string JiraTaskTypeName = "task";
        public const string JiraSubtaskTypeName = "subtask";
        public const string JiraTechnicalStoryTypeName = "technical us";

        //
        public const string JiraOutputWindowName = "Jura";


        /// <summary>
        /// According to issue type of an issue as a parameter returns metatype name of the element in EA
        /// </summary>
        /// <param name="Issue"></param>
        /// <returns></returns>
        public static string GetMetaclassFromIssueType(Atlassian.Jira.Issue Issue)
        {
            switch (Issue.Type.Name.ToLower())
            {
                case JiraBugTypeName:
                    return JiraBugMetaType;
                case JiraEpicTypeName:
                    return JiraEpicMetaType;
                case JiraStoryTypeName:
                    return JiraStoryMetaType;
                case JiraTaskTypeName:
                    return JiraTaskMetaType;
                case JiraSubtaskTypeName:
                    return JiraSubtaskMetaType;
                case JiraTechnicalStoryTypeName:
                    return JiraTechnicalStoryMetaType;
                default:
                    return JiraIssueMetaType;
            }
        }



        /// <summary>
        /// Returns true if the Element is one of Jira MGD Metatype
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        public static bool IsElementJiraMetatype(EA.Element Element)
        {
            return (Element.MetaType == JiraEpicMetaType || Element.MetaType == JiraIssueMetaType || Element.MetaType == JiraTaskMetaType ||
                    Element.MetaType == JiraSubtaskMetaType || Element.MetaType == JiraBugMetaType || Element.MetaType == JiraStoryMetaType ||
                    Element.MetaType == JiraTechnicalStoryMetaType);
        }

        public static string GetJiraKeyFromElement(EA.Repository Repository, string GUID, EA.ObjectType ot)
            => GetJiraKeyFromElement(Repository, Repository.GetElementByGuid(GUID), ot);

        public static string GetJiraKeyFromElement(EA.Repository Repository, dynamic Element, EA.ObjectType ot)
        {
            if (ot == EA.ObjectType.otElement && EAGoatJira.IsElementJiraMetatype(Element))
                return ((EA.Element)Element).TaggedValues.GetByName(TagValueNameJiraKey).Value;

            return null;
        }











    }
}
