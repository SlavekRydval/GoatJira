using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoatJira.Model.Jira.JiraIssue
{
    class JiraIssueModel : ObservableObject
    {
        #region Properties
        private string key;
        private string type;
        private string priority;
        private string components;
        private string labels;
        private string project;
        private string projectType;
        private string status;
        private string assignee;
        private string reporter;
        private DateTime? createdAt;
        private DateTime? updatedAt;
        private string description;
        private string summary;
        private string resolution;
        private DateTime? resolutionDate;
        private DateTime? dueDate;
        private Dictionary<string, string> customFields = new Dictionary<string, string>();

        public string Key { get => key; set => Set(nameof(Key), ref key, value); }
        public string Type { get => type; set => Set(nameof(Type), ref type, value); }
        public string Priority { get => priority; set => Set(nameof(Priority), ref priority, value); }
        public string Components { get => components; set => Set(nameof(Components), ref components, value); }
        public string Labels { get => labels; set => Set(nameof(Labels), ref labels, value); }
        public string Project { get => project; set => Set(nameof(Project), ref project, value); }
        public string ProjectType { get => projectType; set => Set(nameof(ProjectType), ref projectType, value); }
        public string Status { get => status; set => Set(nameof(Status), ref status, value); }
        public string Assignee { get => assignee; set => Set(nameof(Assignee), ref assignee, value); }
        public string Reporter { get => reporter; set => Set(nameof(Reporter), ref reporter, value); }
        public DateTime? CreatedAt { get => createdAt; set => Set(nameof(CreatedAt), ref createdAt, value); }
        public DateTime? UpdatedAt { get => updatedAt; set => Set(nameof(UpdatedAt), ref updatedAt, value); }
        public string Description { get => description; set => Set(nameof(Description), ref description, value); }
        public string Summary { get => summary; set => Set(nameof(Summary), ref summary, value); }
        public string Resolution { get => resolution; set => Set(nameof(Resolution), ref resolution, value); }
        public DateTime? ResolutionDate { get => resolutionDate; set => Set(nameof(ResolutionDate), ref resolutionDate, value); }
        public DateTime? DueDate { get => dueDate; set => Set(nameof(DueDate), ref dueDate, value); }
        public Dictionary<string, string> CustomFields { get => customFields; set => Set(nameof(CustomFields), ref customFields, value); }
        #endregion

        [JsonConstructor]
        public JiraIssueModel()
        {
        }
    }
}
