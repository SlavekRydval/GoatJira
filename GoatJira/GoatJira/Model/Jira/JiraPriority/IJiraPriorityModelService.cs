namespace GoatJira.Model.Jira.JiraPriority
{
    interface IJiraPriorityModelService
    {
        JiraPriorityModel Read();
        void Save(JiraPriorityModel JiraPriority);
    }
}
