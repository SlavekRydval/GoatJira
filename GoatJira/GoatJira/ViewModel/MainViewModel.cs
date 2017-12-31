namespace GoatJira.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GoatJira.Commands;
    using GoatJira.Helpers;
    using GoatJira.Model;
    using System;
    using System.Collections.ObjectModel;

    class MainViewModel: ViewModelBase
    {
        private readonly IDialogService dialogService;
        private readonly MainModel mainModel;
        private readonly IMainModelService mainModelService; 
        private EA.Repository eaRepository;

        public MainViewModel(MainModel MainModel, IMainModelService MainModelService, IDialogService DialogService)
        {
            //initialization
            mainModel = MainModel;
            mainModelService = MainModelService;
            dialogService = DialogService;
            eaRepository = null;

            //read list of packages this addin already has connected with JIRA
            mainModelService.ReadConnectedPackages(ConnectedPackages);
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
            set {
                eaRepository = value;
                //if (eaRepository == null || String.IsNullOrEmpty (eaRepository.ProjectGUID))  
                //    ConnectedPackages.Clear();
                //else
                //    mainModelService.ReadConnectedPackages(ConnectedPackages);
            }
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
        #endregion


        private bool ExecuteConnectPackageWithJiraCommand(EA.Package Package)
        {
            ConnectedPackages.Add(new PackageModel() { GUID = Package.PackageGUID });
            mainModelService.SaveConnectedPackages(ConnectedPackages);
            return true;
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

        private void ExecuteAbout()
        {
            dialogService.ShowAboutDialog();
        }


    }
}
