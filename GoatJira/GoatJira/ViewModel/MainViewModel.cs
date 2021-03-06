﻿namespace GoatJira.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GoatJira.Commands;
    using GoatJira.Helpers;
    using GoatJira.Model;
    using GoatJira.Model.About;
    using GoatJira.Model.Jira.JiraIssue;
    using GoatJira.Model.LoginInformation;
    using GoatJira.Model.Package;
    using GoatJira.Model.PackageConnectionSettings;
    using GoatJira.Services;
    using System;
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

        private RelayCommand<EA.Package> refreshIssuesCommand;
        public RelayCommand<EA.Package> RefreshIssuesCommand
        {
            get
            {
                if (refreshIssuesCommand == null)
                    refreshIssuesCommand = new RelayCommand<EA.Package>((package) => ExecuteRefreshIssuesCommand(package), (package) => CanExecuteRefreshIssuesCommand(package));
                return refreshIssuesCommand;
            }
        }

        private RelayCommandWithResult<EA.Element, bool> showIssueCommand;
        public RelayCommandWithResult<EA.Element, bool> ShowIssueCommand
        {
            get
            {
                if (showIssueCommand == null)
                    showIssueCommand = new RelayCommandWithResult<EA.Element, bool>((element) => ExecuteShowIssueCommand(element));
                return showIssueCommand;
            }
        }
        #endregion


        private bool ExecuteConnectPackageWithJiraCommand(EA.Package Package)
        {
            ConnectedPackages.Add(new PackageModel() { GUID = Package.PackageGUID });
            mainModelService.SaveConnectedPackages(ConnectedPackages);

            SetPackageSettingsCommand.Execute(Package);
            if (SetPackageSettingsCommand.Result)
            {
                Package.Element.Refresh();
                return true;
            }

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
                    //setting off the namespace is due to EA is setting is as namespace
                    //this works but user has to refresh the GUI :-( 
                    //need to be solved
                    Package.IsNamespace = false;
                    //Package.Element.Stereotype = "";
                    //Package.Element.Update();
                    Package.Update();
                    //Package.Element.Refresh();
                    return true;
                }
            }
            return false; 
        }

        private void ExecuteAbout() =>
            dialogService.ShowAboutDialog(new AboutViewModel(new AboutModelService(), dialogService));

        private bool CanExecuteSetPackageSettings(EA.Package Package)
        {
#if MDG
            foreach (var ConnectedPackage in ConnectedPackages)
                if (ConnectedPackage.GUID == Package.PackageGUID)
                    return true;
            return false;
#else
            return Package != null;
#endif
        }

        private bool ExecuteSetPackageSettings(EA.Package Package) =>
            (new PackageConnectionSettingsViewModel(new PackageConnectionSettingsModelService(Package), dialogService)).EditPackageConnectionSettings();

        private bool ExecuteSetLoginInformationCommand()
        {
            try
            {
                bool result;

                if (!loginInformationViewModel.WasDataRead)
                    loginInformationViewModel.ReadData(EARepository);

                result = dialogService.ShowSetLoginInformationDialog(loginInformationViewModel);
                if (result)
                    loginInformationViewModel.SaveData(EARepository);
                return result;
            }
            catch (Exception e)
            {
                dialogService.ShowError(Utils.ExceptionString(e));
                return false; 
            }
        }

        private void ExecuteRefreshIssuesCommand(EA.Package Package)
        {
            try
            {
                if (!loginInformationViewModel.WasDataRead)
                    loginInformationViewModel.ReadData(EARepository);

                if (!loginInformationViewModel.LoginInformation.SavePassword && (String.IsNullOrEmpty (loginInformationViewModel.LoginInformation.Password)))
                {
                    SetLoginInformationCommand.Execute(null);
                    if (!SetLoginInformationCommand.Result)
                        return; 
                }

                var jiraConnection = new JiraConnection();
                jiraConnection.Login(loginInformationViewModel);
                SynchronizingService.SynchronizePackageWithJIRA(eaRepository, Package, new PackageConnectionSettingsViewModel (new PackageConnectionSettingsModelService(Package), dialogService), jiraConnection);
            }
            catch (Exception e)
            {
                if (e.InnerException?.Message.Contains("<title>Unauthorized (401)</title>") == true)
                    dialogService.ShowError("Wrong username or password.");
                if (e.InnerException?.Message.Contains("<h1>Page unavailable</h1>") == true)
                    dialogService.ShowError("Enter correct URL for your JIRA server.");
                else
                    dialogService.ShowError(Utils.ExceptionString(e));
            }
        }

        private bool CanExecuteRefreshIssuesCommand(EA.Package Package) =>
         (new PackageConnectionSettingsViewModel(new PackageConnectionSettingsModelService(Package), dialogService)).AreDataSet();
        

        private void ExecuteShowIssueCommand(EA.Element Element)
        {
            ShowIssueCommand.Result = false;
            try
            {
                string JsonIssue = ((EA.TaggedValue)Element.TaggedValues.GetByName(EAGoatJira.TagValueNameData)).Notes;
                try
                {
                    JiraIssueViewModel JiraIssue = new JiraIssueViewModel(new JSONJiraIssueModelService(JsonIssue), dialogService);
                    try
                    {
                        JiraIssue.ShowIssue();
                        ShowIssueCommand.Result = true;
                    }
                    catch (Exception e)
                    {
                        dialogService.ShowError($"Cannot show Jira Issue Dialog, default Properties dialog will be shown.\n{Utils.ExceptionString(e)}");
                    }
                }
                catch (Exception e)
                {
                    dialogService.ShowError($"Cannot read issue data, default Properties dialog will be shown. There is probably broken data in tagged value {EAGoatJira.TagValueNameData}\n{Utils.ExceptionString(e)}");
                }
            }
            catch (Exception e)
            {
                dialogService.ShowError($"Cannot read the tagged value {EAGoatJira.TagValueNameData}, default Properties dialog will be shown.\n{Utils.ExceptionString(e)}");
            }
        }
    }
}
