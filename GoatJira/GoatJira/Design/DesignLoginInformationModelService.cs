using GoatJira.Model.LoginInformation;

namespace GoatJira.Design
{
    class DesignLoginInformationModelService : ILoginInformationModelService
    {
        public LoginInformationModel Read(string StorageID)
        {
            return new LoginInformationModel("https://jira.jiraserver.com", "bulicek", "bulda", true);
        }

        public void Save(LoginInformationModel LoginInformation, string StorageID)
        {
            throw new System.NotImplementedException();
        }
    }
}
