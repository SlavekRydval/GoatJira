namespace GoatJira.ViewModel
{
    using GalaSoft.MvvmLight;
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
            mainModel = MainModel;
            mainModelService = MainModelService;
            dialogService = DialogService;
            eaRepository = null;
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
            get => EARepository;
            set {
                eaRepository = value;
                if (eaRepository == null || String.IsNullOrEmpty (eaRepository.ProjectGUID))  
                    ConnectedPackages.Clear();
                else
                    mainModelService.ReadConnectedPackages(ConnectedPackages);
            }
        }


        #region Commands
        private RelayCommandWithResult<EA.Package, bool> connectPackageWithJiraCommand;
        public RelayCommandWithResult<EA.Package, bool> ConnectPackageWithJiraCommand
        {
            get
            {
                if (connectPackageWithJiraCommand == null)
                    connectPackageWithJiraCommand = new RelayCommandWithResult<EA.Package, bool>((package) => ExecuteConnectPackageWithJiraCommand(package));
                return connectPackageWithJiraCommand;
            }

        }
        #endregion


        private bool ExecuteConnectPackageWithJiraCommand(EA.Package Package)
        {
            ConnectedPackages.Add(new PackageModel() { GUID = Package.PackageGUID });
            mainModelService.SaveConnectedPackages(ConnectedPackages);
            return true;
        }



    }
}
