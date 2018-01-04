namespace GoatJira.Model.LoginInformation
{
    interface ILoginInformationModelService
    {
        LoginInformationModel Read(string StorageID);
        void Save(LoginInformationModel LoginInformation, string StorageID);
    }
}
