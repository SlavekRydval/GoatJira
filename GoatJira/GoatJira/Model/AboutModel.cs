using GalaSoft.MvvmLight;

namespace GoatJira.Model
{
    class AboutModel: ObservableObject
    {
        public string Author { get; set; }
        public string Email { get; set; }
        public string AuthorHomePage { get; set; }
        public string ProductWebPage { get; set; }
        public string AddinName { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public int Revision { get; set; }
        public string VersionAdditionalInfo { get; set; }
    }
}
