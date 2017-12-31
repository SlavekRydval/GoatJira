using GoatJira.Model;

namespace GoatJira.Design
{
    class DesignAboutModelService: IAboutModelService
    {
        public AboutModel Read()
        {
            return new AboutModel()
            {
                Author = "*Slávek Rydval",
                Email = "*slavek@rydval.cz",
                AuthorHomePage = "*http://rydval.cz",
                ProductWebPage = "*http:// will be defined later on",
                AddinName = "*GOAT Jira",
                MajorVersion = -8,
                MinorVersion = 0,
                Revision = 42,
                VersionAdditionalInfo = "design mode"
            };
        }
    }
}
