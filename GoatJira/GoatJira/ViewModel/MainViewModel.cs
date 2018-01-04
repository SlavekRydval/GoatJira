namespace GoatJira.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GoatJira.Commands;
    using GoatJira.Helpers;
    using GoatJira.Model;
    using GoatJira.Model.About;
    using GoatJira.Model.LoginInformation;
    using GoatJira.Model.Package;
    using GoatJira.Model.PackageConnectionSettings;
    using System.Collections.ObjectModel;

    class MainViewModel: ViewModelBase
    {
        private readonly IDialogService dialogService;
        private readonly MainModel mainModel;
        private readonly IMainModelService mainModelService; 
        private EA.Repository eaRepository;
        private readonly LoginInformationViewModel loginInformationViewModel;
        

        public MainViewModel(MainModel MainModel, IMainModelService MainModelService, IDialogService DialogService, ILoginInformationModelService LoginInformationModelService)
        {
            //initialization
            eaRepository = null;

            mainModel = MainModel;
            mainModelService = MainModelService;
            dialogService = DialogService;

            //read list of packages this addin already has connected with JIRA
            mainModelService.ReadConnectedPackages(ConnectedPackages);
            loginInformationViewModel = new LoginInformationViewModel(LoginInformationModelService, dialogService);
        }

        /// <summary>
        /// List of all connected packages
        /// </summary>
        public ObservableCollection<PackageModel> ConnectedPackages
        {
            get => mainModel.ConnectedPackages;
        }

        /// <summary>
        /// Sparx EA Repository which we are cooperating with
        /// </summary>
        public EA.Repository EARepository
        {
            get => eaRepository;
            set => eaRepository = value;
        }


        #region Commands
        private RelayCommandWithResult<EA.Package, bool> connectPackageWithJiraCommand;
        public RelayCommandWithResult<EA.Package, bool> ConnectPackageWithJiraCommand
        {
            get
            {
                if (connectPackageWithJiraCommand == null)
                    connectPackageWithJiraCommand = new RelayCommandWithResult<EA.Package, bool>(
                        (package) => { ConnectPackageWithJiraCommand.Result = ExecuteConnectPackageWithJiraCommand(package);});
                return connectPackageWithJiraCommand;
            }

        }

        private RelayCommandWithResult<EA.Package, bool> disconnectPackageFromJiraCommand;
        public RelayCommandWithResult<EA.Package, bool> DisconnectPackageFromJiraCommand
        {
            get
            {
                if (disconnectPackageFromJiraCommand == null)
                    disconnectPackageFromJiraCommand = new RelayCommandWithResult<EA.Package, bool>(
                        (package) => { DisconnectPackageFromJiraCommand.Result = ExecuteDisconnectPackageFromJiraCommand(package); });
                    ;
                return disconnectPackageFromJiraCommand;

            }
        }

        private RelayCommand aboutCommand;
        public RelayCommand AboutCommand
        {
            get
            {
                if (aboutCommand == null)
                    aboutCommand = new RelayCommand(() => ExecuteAbout());
                return aboutCommand;

            }
        }

        private RelayCommandWithResult<EA.Package, bool> setPackageSettingsCommand;
        public RelayCommandWithResult<EA.Package, bool> SetPackageSettingsCommand
        {
            get
            {
                if (setPackageSettingsCommand == null)
                    setPackageSettingsCommand = new RelayCommandWithResult<EA.Package, bool>((package) => SetPackageSettingsCommand.Result = ExecuteSetPackageSettings(package),
                        (package) => CanExecuteSetPackageSettings(package));
                return setPackageSettingsCommand;
            }
        }

        private RelayCommandWithResult<bool> setLoginInformationCommand;
        public RelayCommandWithResult<bool> SetLoginInformationCommand
        {
            get
            {
                if (setLoginInformationCommand == null)
                    setLoginInformationCommand = new RelayCommandWithResult<bool>(() => SetLoginInformationCommand.Result = ExecuteSetLoginInformationCommand());
                return setLoginInformationCommand;
            }
        }
        #endregion


        private bool ExecuteConnectPackageWithJiraCommand(EA.Package Package)
        {
            ConnectedPackages.Add(new PackageModel() { GUID = Package.PackageGUID });
            mainModelService.SaveConnectedPackages(ConnectedPackages);

            SetPackageSettingsCommand.Execute(Package);
            if (SetPackageSettingsCommand.Result)
                return true;

            DisconnectPackageFromJiraCommand.Execute(Package);
            return false; 
        }

        private bool ExecuteDisconnectPackageFromJiraCommand(EA.Package Package)
        {
            foreach (var ConnectedPackage in ConnectedPackages)
            {
                if (ConnectedPackage.GUID == Package.PackageGUID)
                {
                    ConnectedPackages.Remove(ConnectedPackage);
                    return true;
                }
            }
            return false; 
        }

        private void ExecuteAbout() =>
            dialogService.ShowAboutDialog(new AboutViewModel(new AboutModelService(), dialogService));

        private bool CanExecuteSetPackageSettings(EA.Package Package)
        {
            foreach (var ConnectedPackage in ConnectedPackages)
                if (ConnectedPackage.GUID == Package.PackageGUID)
                    return true;
            return false;
        }

        private bool ExecuteSetPackageSettings(EA.Package Package) =>
            (new PackageConnectionSettingsViewModel(new PackageConnectionSettingsModelService(Package), dialogService)).EditPackageConnectionSettings();

        private bool ExecuteSetLoginInformationCommand()
        {
            bool result; 

            loginInformationViewModel.ReadData(EARepository);
            result = dialogService.ShowSetLoginInformationDialog(loginInformationViewModel);
            if (result)
                loginInformationViewModel.SaveData(EARepository);
            return result;
        }


    }
}
