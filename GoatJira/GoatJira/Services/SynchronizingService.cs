namespace GoatJira.Services
{
    using GoatJira.Helpers;
    using GoatJira.Model.Jira.JiraIssue;
    using GoatJira.Model.PackageConnectionSettings;
    using GoatJira.ViewModel;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    class SynchronizingService
    {

        public static void SynchronizePackageWithJIRA(EA.Repository Repository, EA.Package Package, IPackageConnectionSettingsModelService PackageConnectionSettingsModelService, JiraConnection JiraConnection)
        {
            //Steps:
            //      1. User must be able to login to JIRA -- must ensure caller
            //      2. Creating Repository.Output -- well... am thinking about event, JTS so far
            //      3. Get the proper JQL -- i will get from parameter
            //      4. Do to job

            Repository.CreateOutputTab(EAGoatJira.JiraOutputWindowName);
            Repository.EnsureOutputVisible(EAGoatJira.JiraOutputWindowName);
            Repository.ClearOutput(EAGoatJira.JiraOutputWindowName);
            Repository.WriteOutput(EAGoatJira.JiraOutputWindowName, "Reading data...", 0);

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
                EA.Package PackageForRemovedIssues = EAUtils.CreatePackage (Package, "Removed Issues", ForceCreation: false);
                foreach (var removedItem in IssuesInEA)
                {
                    Repository.WriteOutput(EAGoatJira.JiraOutputWindowName, $"Removing issue {removedItem.Key}", removedItem.Value.ElementID);
                    removedItem.Value.PackageID = PackageForRemovedIssues.PackageID;
                    removedItem.Value.Update();
                    EAUtils.WriteTaggedValue(removedItem.Value, EAGoatJira.TagValueNameOperation, "Issue was " + IssueOperation.Removed.ToString() + ".", WriteValueToNotes: false);
                }
                PackageForRemovedIssues.Update();
            }

            Package.Update();
            Repository.WriteOutput(EAGoatJira.JiraOutputWindowName, $"Done.", 0);
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
                Repository.WriteOutput(EAGoatJira.JiraOutputWindowName, $"Updating issue {Issue.Key.Value}", EAElementForIssue.ElementID);
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
                    EAElementForIssue = Package.Elements.AddNew($"{Issue.Key} {Issue.Summary}", EAGoatJira.GetMetaclassFromIssueType(Issue));
                else
                    EAElementForIssue = Element.Elements.AddNew($"{Issue.Key} {Issue.Summary}", EAGoatJira.GetMetaclassFromIssueType(Issue));
                Repository.WriteOutput(EAGoatJira.JiraOutputWindowName, $"Inserting issue {Issue.Key.Value}", EAElementForIssue.ElementID);
            }
            FillIssue(EAElementForIssue, new JiraIssueViewModel(new AtlassianJiraIssueModelService(Issue), null), Operation);

            EAElementForIssue.Update();

            return EAElementForIssue;
        }

        private static void FillIssue(EA.Element EAIssue, JiraIssueViewModel jiraIssue, IssueOperation Operation)
        {
            EAIssue.Notes = jiraIssue.JiraIssue.Description;
            EAIssue.Update();

            EAUtils.WriteTaggedValue(EAIssue, EAGoatJira.TagValueNameJiraKey, jiraIssue.JiraIssue.Key, WriteValueToNotes: false);
            EAUtils.WriteTaggedValue(EAIssue, EAGoatJira.TagValueNameData, JsonConvert.SerializeObject(jiraIssue.JiraIssue, Formatting.Indented), WriteValueToNotes: true);
            EAUtils.WriteTaggedValue(EAIssue, EAGoatJira.TagValueNameLastUpdateAt, DateTime.Now.ToString(), WriteValueToNotes: false);
            EAUtils.WriteTaggedValue(EAIssue, EAGoatJira.TagValueNameOperation, "Issue was " + Operation.ToString().ToLower() + ".", WriteValueToNotes: false);
            EAUtils.WriteTaggedValue(EAIssue, EAGoatJira.TagValueNamePriority, jiraIssue.JiraIssue.Priority, WriteValueToNotes: false);
            EAUtils.WriteTaggedValue(EAIssue, EAGoatJira.TagValueNameStatus, jiraIssue.JiraIssue.Status, WriteValueToNotes: false);
            EAUtils.WriteTaggedValue(EAIssue, EAGoatJira.TagValueNameType, jiraIssue.JiraIssue.Type, WriteValueToNotes: false);
        }

        private static Dictionary<string, EA.Element> ReadIssuesFromEA(EA.Repository Repository, EA.Collection EAElements)
        {
            Dictionary<string, EA.Element> result = new Dictionary<string, EA.Element>();

            foreach (EA.Element EAIssue in EAElements)
            {
                if (EAGoatJira.IsElementJiraMetatype(EAIssue))
                {
                    result.Add(EAGoatJira.GetJiraKeyFromElement(Repository, EAIssue, EA.ObjectType.otElement), EAIssue);
                    foreach (EA.Element EASubissue in EAIssue.Elements)
                    {
                        ///TODO: Zapřemýšlet, zda to nemůže vyhučet na tom, že v sobě bude mít issue, které se dostane i v původním resultu
                        if (EAGoatJira.IsElementJiraMetatype(EASubissue)) 
                            result.Add(EAGoatJira.GetJiraKeyFromElement(Repository, EASubissue, EA.ObjectType.otElement), EASubissue);
                    }
                }
            }
            return result;
        }

    }
}
