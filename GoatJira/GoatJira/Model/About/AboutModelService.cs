namespace GoatJira.Model.About
{
    class AboutModelService : IAboutModelService
    {
        public AboutModel Read()
        {

            System.Version version = (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed) ?
                System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion :
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            return new AboutModel()
            {
                Author = "Slávek Rydval",
                Email = "slavek@rydval.cz",
                AuthorHomePage = "http://rydval.cz",
                ProductWebPage = "https://github.com/SlavekRydval/GoatJira",
                AddinName = "GOAT Jira",
                MajorVersion = version.Major,
                MinorVersion = version.Minor,
                Revision = version.Revision,
                VersionAdditionalInfo = "RC 1",
                CopyrightYearStart = 2016,
                CopyrightYearEnd = 2018,
                Licence = "MIT",
                LicenceURI = "https://github.com/SlavekRydval/GoatJira/blob/master/LICENSE"
            };
        }
    }
}
