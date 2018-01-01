using GoatJira.Model;

namespace GoatJira.Design
{
    class DesignAboutModelService: IAboutModelService
    {
        public AboutModel Read()
        {
            return new AboutModel()
            {
                Author = "Slávek Rydval",
                Email = "slavek@rydval.cz",
                AuthorHomePage = "http://rydval.cz",
                ProductWebPage = "http:// will be defined later on",
                AddinName = "GOAT Jira",
                MajorVersion = 1,
                MinorVersion = 0,
                Revision = 42,
                VersionAdditionalInfo = "!DesMode!",
                CopyrightYearStart = 2016,
                CopyrightYearEnd = 2018,
                Licence = "MIT",
                LicenceURI = "https://github.com/SlavekRydval/GoatJira/blob/master/LICENSE"
            };
        }
    }
}
