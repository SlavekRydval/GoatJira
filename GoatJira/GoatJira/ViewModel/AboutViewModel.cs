using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GoatJira.Helpers;
using GoatJira.Model.About;
using System.Diagnostics;

namespace GoatJira.ViewModel
{
    class AboutViewModel: ViewModelBase
    {
        public AboutModel Metadata { get; }

        public AboutViewModel(): this (IsInDesignModeStatic ? (IAboutModelService)new Design.DesignAboutModelService() : new AboutModelService(), new DialogService())
        {
        }

        public AboutViewModel(IAboutModelService AboutModelService, IDialogService DialogService) => Metadata = AboutModelService.Read();

        public string AboutTitle { get => $"About {Metadata.AddinName}"; }
        public string FullVersion { get => $"{Metadata.MajorVersion}.{Metadata.MinorVersion} rev {Metadata.Revision} {Metadata.VersionAdditionalInfo}"; }
        public string FullNameWithVersion { get => $"{Metadata.AddinName} {FullVersion}"; }
        public string EmailURI { get => $"mailto:{Metadata.Email}"; }
        public string Copyrigth { get => $"© {Metadata.CopyrightYearStart}-{Metadata.CopyrightYearEnd} {Metadata.Author}"; }


        private RelayCommand<string> runURI;
        public RelayCommand<string> RunURI
        {
            get
            {
                if (runURI == null)
                    runURI = new RelayCommand<string>((uri) => { Process.Start(uri); });
                return runURI;
            }
        }
    }
}
