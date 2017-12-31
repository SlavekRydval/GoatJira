namespace GoatJira.Model
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
                ProductWebPage = "http:// will be defined later on",
                AddinName = "GOAT Jira",
                MajorVersion = version.Major,
                MinorVersion = version.Minor,
                Revision = version.Revision,
                VersionAdditionalInfo = "beta"
            };
        }
    }
}
