using GalaSoft.MvvmLight;
using GoatJira.Helpers;
using GoatJira.Model;

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
    }
}
