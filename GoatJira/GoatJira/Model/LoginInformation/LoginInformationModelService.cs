namespace GoatJira.Model.LoginInformation
{
    using Newtonsoft.Json;
    using System;
    using System.IO;

    class LoginInformationModelService : ILoginInformationModelService
    {
        private static string GetConfigFile(string StorageID) => 
            $@"{Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName}\EAGoatJiraConnection_{StorageID}.json";

        public LoginInformationModel Read(string StorageID)
        {
            try
            {
                return JsonConvert.DeserializeObject<LoginInformationModel>(File.ReadAllText(GetConfigFile(StorageID)));
            }
            catch (Exception)
            {
                return new LoginInformationModel();
            }
        }

        public void Save(LoginInformationModel LoginInformation, string StorageID) =>
            File.WriteAllText(GetConfigFile(StorageID), JsonConvert.SerializeObject(LoginInformation, Formatting.Indented));
    }
}
