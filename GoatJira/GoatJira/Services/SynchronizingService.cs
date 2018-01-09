namespace GoatJira.Services
{
    using GoatJira.Model.Jira.JiraIssue;
    using GoatJira.Model.PackageConnectionSettings;
    using GoatJira.ViewModel;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    class SynchronizingService
    {
        //Following names have to correspond with names of stereotype attributes (names of tag values)
        private const string TagValueNameJiraKey = "JiraKey";
        private const string TagValueNameLastUpdateAt = "LastUpdateAt";
        private const string TagValueNameData = "Data";
        private const string TagValueNameOperation = "LastUpdateInfo";
        private const string TagValueNamePriority = "Priority";
        private const string TagValueNameStatus = "Status";
        private const string TagValueNameType = "Type";

        //Following names have to correspond with names of stereotypes defined in Profile
        private const string JiraIssueMetaType = "JiraIssue";
        private const string JiraEpicMetaType = "JiraEpic";
        private const string JiraStoryMetaType = "JiraStory";
        private const string JiraTaskMetaType = "JiraTask";
        private const string JiraSubtaskMetaType = "JiraSubtask";
        private const string JiraBugMetaType = "JiraBug";
        private const string JiraTechnicalStoryMetaType = "JiraTechnicalStory";

        //Following names have to correspond with names of Jira issue types
        //(these string could be variable and should be read from config file)
        private const string JiraBugTypeName = "bug";
        private const string JiraEpicTypeName = "epic";
        private const string JiraStoryTypeName = "story";
        private const string JiraTaskTypeName = "task";
        private const string JiraSubtaskTypeName = "subtask";
        private const string JiraTechnicalStoryTypeName = "technical us";

        //
        private const string JiraOutputWindowName = "Jura";



        public static void SynchronizePackageWithJIRA(EA.Repository Repository, EA.Package Package, IPackageConnectionSettingsModelService PackageConnectionSettingsModelService, JiraConnection JiraConnection)
        {
            //Steps:
            //      1. User must be able to login to JIRA -- must ensure caller
            //      2. Creating Repository.Output -- well... am thinking about event, JTS so far
            //      3. Get the proper JQL -- i will get from parameter
            //      4. Do to job

            Repository.CreateOutputTab(JiraOutputWindowName);
            Repository.EnsureOutputVisible(JiraOutputWindowName);
            Repository.ClearOutput(JiraOutputWindowName);
            Repository.WriteOutput(JiraOutputWindowName, "Reading data...", 0);

            var pcs = PackageConnectionSettingsModelService.Read();
            string UserJql = pcs.Jql;

            Dictionary<string, EA.Element> IssuesInEA = ReadIssuesFromEA(Repository, Package.Elements);

            var issues = JiraConnection.GetJiraIssues(UserJql);
            foreach (var issue in issues)
            {
                EA.Element EAElementForIssue = SynchronizeItem(issue, Repository, Package, null, IssuesInEA);

                if (pcs.Type == PackageConnectionSettingsType.EpicsAndStories)
                {
                    var epicissues = JiraConnection.GetJiraIssues($"\"Epic Link\" = {issue.Key}");
                    foreach (var epicissue in epicissues)
                        SynchronizeItem(epicissue, Repository, Package, EAElementForIssue, IssuesInEA);
                }
                EAElementForIssue.Update();
            }

            if (IssuesInEA.Count > 0)
            { //some issues that are in the Package are not in the issues (they were deleted or moved or the Jql has chaned)
                EA.Package PackageForRemovedIssues = PreparePackageForRemovedIssues(Package);
                foreach (var removedItem in IssuesInEA)
                {
                    Repository.WriteOutput(JiraOutputWindowName, $"Removing issue {removedItem.Key}", removedItem.Value.ElementID);
                    removedItem.Value.PackageID = PackageForRemovedIssues.PackageID;
                    removedItem.Value.Update();
                    SetTaggedValue(removedItem.Value, TagValueNameOperation, "Issue was " + IssueOperation.Removed.ToString() + ".", PutValueToNotes: false);
                }
                PackageForRemovedIssues.Update();
            }

            Package.Update();
            Repository.WriteOutput(JiraOutputWindowName, $"Done.", 0);

        }

        private enum IssueOperation { Inserted, Updated, Removed }

        /// <summary>
        /// Synchronization of an issue. 
        /// </summary>
        /// <param name="Issue">Issue read from JIRA server that should be synchronized.</param>
        /// <param name="Repository">Sparx EA Repository.</param>
        /// <param name="Package">Packages that owns all issues that are not owned by any element withint this package.</param>
        /// <param name="Element">If it is not null then this element will be owner of the issue element instead of Package.</param>
        /// <param name="IssuesInEA">All issues that were within the packages just before the synchronization begins.</param>
        /// <returns>EA Element that represents synchronized issue.</returns>
        private static EA.Element SynchronizeItem(Atlassian.Jira.Issue Issue, EA.Repository Repository, EA.Package Package, EA.Element Element, Dictionary<string, EA.Element> IssuesInEA)
        {
            IssueOperation Operation;
            EA.Element EAElementForIssue;
            if (IssuesInEA.ContainsKey(Issue.Key.Value))
            { //this issue is already saved in EA. Let's do something with it
                Operation = IssueOperation.Updated;
                EAElementForIssue = IssuesInEA[Issue.Key.Value];
                Repository.WriteOutput(JiraOutputWindowName, $"Updating issue {Issue.Key.Value}", EAElementForIssue.ElementID);
                if (Element == null)
                    EAElementForIssue.PackageID = Package.PackageID;
                else
                    EAElementForIssue.ParentID = Element.ElementID;

                ///TODO: I should change the stereotype, if the type of issue has been chaged. 
                ///      On the other hand, some configuration of JIRA doesn't allow these changes to ordinary users. 
                ///EAElementForIssue.Type = GetMetaclassFromIssueType(Issue);

                IssuesInEA.Remove(Issue.Key.Value);
            }
            else
            { //this issue is not in EA so far, let's create a new one EA.Element
                Operation = IssueOperation.Inserted;
                if (Element == null)
                    EAElementForIssue = Package.Elements.AddNew($"{Issue.Key} {Issue.Summary}", GetMetaclassFromIssueType(Issue));
                else
                    EAElementForIssue = Element.Elements.AddNew($"{Issue.Key} {Issue.Summary}", GetMetaclassFromIssueType(Issue));
                Repository.WriteOutput(JiraOutputWindowName, $"Inserting issue {Issue.Key.Value}", EAElementForIssue.ElementID);
            }
            FillIssue(EAElementForIssue, new JiraIssueViewModel(new JiraIssueModelService(Issue), null), Operation);

            EAElementForIssue.Update();

            return EAElementForIssue;
        }

        private static void FillIssue(EA.Element EAIssue, JiraIssueViewModel jiraIssue, IssueOperation Operation)
        {
            EAIssue.Notes = jiraIssue.JiraIssue.Description;
            EAIssue.Update();
            SetTaggedValue(EAIssue, TagValueNameJiraKey, jiraIssue.JiraIssue.Key, PutValueToNotes: false);
            SetTaggedValue(EAIssue, TagValueNameData, JsonConvert.SerializeObject(jiraIssue.JiraIssue, Formatting.Indented), PutValueToNotes: true);
            SetTaggedValue(EAIssue, TagValueNameLastUpdateAt, DateTime.Now.ToString(), PutValueToNotes: false);
            SetTaggedValue(EAIssue, TagValueNameOperation, "Issue was " + Operation.ToString().ToLower() + ".", PutValueToNotes: false);
            SetTaggedValue(EAIssue, TagValueNamePriority, jiraIssue.JiraIssue.Priority, PutValueToNotes: false);
            SetTaggedValue(EAIssue, TagValueNameStatus, jiraIssue.JiraIssue.Status, PutValueToNotes: false);
            SetTaggedValue(EAIssue, TagValueNameType, jiraIssue.JiraIssue.Type, PutValueToNotes: false);
        }


        /// <summary>
        /// According to issue type of an issue as a parameter returns metatype name of the element in EA
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private static string GetMetaclassFromIssueType(Atlassian.Jira.Issue i)
        {
            switch (i.Type.Name.ToLower())
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


        private static void SetTaggedValue(EA.Element element, string Name, string Value, bool PutValueToNotes)
        {
            EA.TaggedValue tv = element.TaggedValues.GetByName(Name);
            if (PutValueToNotes)
                tv.Notes = Value;
            else
                tv.Value = Value;
            tv.Update();
        }



        private static Dictionary<string, EA.Element> ReadIssuesFromEA(EA.Repository Repository, EA.Collection EAElements)
        {
            Dictionary<string, EA.Element> result = new Dictionary<string, EA.Element>();

            foreach (EA.Element EAIssue in EAElements)
            {
                if (IsElementJiraMetatype(EAIssue))
                {
                    result.Add(GetJiraKeyFromElement(Repository, EAIssue, EA.ObjectType.otElement), EAIssue);
                    foreach (EA.Element EASubissue in EAIssue.Elements)
                    {
                        if (IsElementJiraMetatype(EASubissue)) ///TODO: Zapřemýšlet, zda to nemůže vyhučet na tom, že v sobě bude mít issue, které se dostane i v původním resultu
                            result.Add(GetJiraKeyFromElement(Repository, EASubissue, EA.ObjectType.otElement), EASubissue);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Returns true if the Element is one of Jira MGD Metatype
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        private static bool IsElementJiraMetatype(EA.Element Element)
        {
            return (Element.MetaType == JiraEpicMetaType || Element.MetaType == JiraIssueMetaType || Element.MetaType == JiraTaskMetaType ||
                    Element.MetaType == JiraSubtaskMetaType || Element.MetaType == JiraBugMetaType || Element.MetaType == JiraStoryMetaType ||
                    Element.MetaType == JiraTechnicalStoryMetaType);

        }

        private static string GetJiraKeyFromElement(EA.Repository Repository, string GUID, EA.ObjectType ot)
        {
            if (ot == EA.ObjectType.otPackage)
                throw new NotImplementedException();

            if (ot == EA.ObjectType.otElement)
                return Repository.GetElementByGuid(GUID).TaggedValues.GetByName(TagValueNameJiraKey).Value;

            return null;
        }

        private static string GetJiraKeyFromElement(EA.Repository Repository, dynamic Element, EA.ObjectType ot)
        {
            ///TODO: Test, zda je to jira element, abych to nepovolil pomalu ka6d0mu elementu s aliasem
            if (ot == EA.ObjectType.otPackage)
                throw new NotImplementedException();

            if (ot == EA.ObjectType.otElement && IsElementJiraMetatype(Element))
                return ((EA.Element)Element).TaggedValues.GetByName(TagValueNameJiraKey).Value;

            return null;
        }

        private static EA.Package PreparePackageForRemovedIssues(EA.Package Package)
        {
            string pkgName = "Removed Issues";
            foreach (EA.Package Subpackage in Package.Packages)
            {
                if (Subpackage.Name == pkgName)
                    return Subpackage;

            }

            EA.Package result = Package.Packages.AddNew(pkgName, "Package");
            result.Update();
            Package.Packages.Refresh();
            return result;
        }






    }
}
