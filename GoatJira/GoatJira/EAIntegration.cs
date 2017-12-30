using GoatJira.ViewModel;
using System;

namespace GoatJira
{
    public class EAIntegration
    {
        private MainViewModel mainViewModel;


        /// <summary>
        /// This method calls EA to discover the type of this plugin. As we are MDG we need to say it loud and clear in return parameter.
        /// </summary>
        /// <param name="Repository"></param>
        /// <returns>MDG when plugin is MDG type, empty string otherwise</returns>
        public String EA_Connect(EA.Repository Repository)
        {
#if DEBUG
            System.Windows.Forms.MessageBox.Show("Append a debugger if needed.");
#endif

            mainViewModel = new MainViewModel(new Model.MainModel(), new Model.MainModelService(), new Helpers.DialogService())
            {
                EARepository = Repository
            };
            return "MDG";
        }

        /// <summary>
        /// Lets tidy up the mess we have created
        /// It is called just once when EA is about to end
        /// </summary>
        public void EA_Disconnect()
        {
            mainViewModel.EARepository = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public object EA_OnInitializeTechnologies(EA.Repository Repository)
        {
            return Properties.Resources.GoatJiraMDG;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Repository"></param>
        public void EA_FileNew(EA.Repository Repository) => mainViewModel.EARepository = Repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Repository"></param>
        public void EA_FileOpen(EA.Repository Repository) => mainViewModel.EARepository = Repository;


        #region Menu Definition
        /// 
        /// Definujeme si někteté položku v Menu
        /// 
        const string menuHeader = "-JIRA Connection via GOAT";


        const string MDGConnectExternalProject = "&Connect External Project";


        const string menuItemManageJiraConnection = "Manage JIRA Connection…";
        const string menuItemShowWebsite = "&Show JIRA Main Web Site";

        //const string menuItemConnectToJira = "Connect package to JIRA...";
        const string menuItemReadRefreshIssues = "Read/Refresh issues in package";

        const string menuItemNavigateIssueToWeb = "&Show Selected JIRA Issue in Browser";

        const string menuItemAbout = "&About…";

        /// <summary>
        /// Building the menu
        /// </summary>
        /// <param name="Repository"></param>
        /// <param name="Location"></param>
        /// <param name="MenuName"></param>
        /// <returns></returns>
        public object EA_GetMenuItems(EA.Repository Repository, string Location, string MenuName)
        {
            switch (MenuName)
            {
                case "":
                    return menuHeader;
                case menuHeader:
                    string[] subMenus = {
                                            "PROJECT",
                                            menuItemManageJiraConnection,
                                            menuItemShowWebsite,
                                            "-",
                                            "PACKAGE",
                                            //menuItemConnectToJira,
                                            menuItemReadRefreshIssues,
                                            "-",
                                            "ITEM",
                                            menuItemNavigateIssueToWeb,
                                            "-",
                                            menuItemAbout
                                        };
                    return subMenus;
            }

            return "";
        }


        public void EA_GetMenuState(EA.Repository Repository, string Location, string MenuName, string ItemName, ref bool IsEnabled, ref bool IsChecked)
        {
            /////////            bool vIsProjectOpen = EAUtils.IsProjectOpen(Repository);
            /////////EA.ObjectType vOT = vIsProjectOpen ? EAUtils.emergencyGetContextItemType(Repository) : EA.ObjectType.otNone;


            switch (ItemName)
            {
                case menuItemManageJiraConnection:
                    /////////                    IsEnabled = AddinViewModel.SetUpJiraConnectionCommand.CanExecute(null);
                    break;
                case menuItemAbout:
                    IsEnabled = true;
                    break;
                case menuItemShowWebsite:
                    /////////                    IsEnabled = AddinViewModel.LoginCommand.CanExecute(null); // vIsProjectOpen
                    break;
                case menuItemReadRefreshIssues:
                case MDGConnectExternalProject:
                    //case menuItemConnectToJira:
                    /////////                    IsEnabled = vIsProjectOpen && (vOT == EA.ObjectType.otPackage);
                    break;
                case menuItemNavigateIssueToWeb:
                    /////////                    IsEnabled = vIsProjectOpen && (vOT == EA.ObjectType.otElement);
                    break;
                default:
                    IsEnabled = false;
                    break;
            }
        }


        /// Konečně! Někdo zvolil některé z našeho menu, takže směle do toho.
        public void EA_MenuClick(EA.Repository Repository, string Location, string MenuName, string ItemName)
        {
            switch (ItemName)
            {
                //case MDGConnectExternalProject: 
                case menuItemManageJiraConnection:
                    //EAJuraBridge.LinkCurrentProjectWithJira(Repository);
/////////                    AddinViewModel.SetUpJiraConnectionCommand.Execute(null);
                    //AddinViewModel.Login();
                    break;

                case menuItemShowWebsite:
                    /////////                    EAJuraBridge.ShowWebSite(Repository);
                    break;

                case menuItemAbout:
                    /////////                    (new frmAbout()).ShowDialog();
                    break;

                //case menuItemConnectToJira:
                //    EAJuraBridge.ConnectPackageToJIRA(Repository.GetContextObject());
                //    break;

                case menuItemReadRefreshIssues:
                    /////////                    EAJuraBridge.SynchronizePackageWithJira(Repository, Repository.GetContextObject());
                    break;

                case menuItemNavigateIssueToWeb:
                    /////////                    string JiraKey = EAJuraBridge.GetJiraKeyFromElement(Repository, Repository.GetContextObject(), EAUtils.emergencyGetContextItemType(Repository));
                    ///TODO: Test na pr8zdn7 key
/////////                    EAJuraBridge.ShowWebSite(Repository, JiraKey, "/browse/" + JiraKey);
                    break;

                default:
                    /////////                    MessageBox.Show($"Unhandled menu item '{ItemName}'!");
                    break;
            }
        }











        #endregion







        #region MDG mandatory methods
        ///-----------------
        ///MGD
        ///-----------------
        ///

        public void MDG_BuildProject(EA.Repository Repository, EA.Package Package) { }

        /// <summary>
        /// An Add-In uses MDG_Connect to handle a user driven request to connect a model branch to an external application.
        ///The function is called when the user attempts to connect a particular Enterprise Architect Package to an as yet
        ///unspecified external project.The Add-In calls the event to interact with the user to specify such a project.
        ///The Add-In is responsible for retaining the connection details, which should be stored on a per-user or per-workstation
        ///basis. That is, users who share a common Enterprise Architect model over a network should be able to connect and
        ///disconnect to external projects independently of one another.
        ///The Add-In should therefore not store connection details in an Enterprise Architect repository. A suitable place to store
        ///such details would be:
        ///SHGetFolderPath(..CSIDL_APPDATA..)\AddinName
        ///The PackageGuid parameter is the same identifier as is required for most events relating to the MDG Add-In.Therefore
        ///it is recommended that the connection details be indexed using the PackageGuid value.
        ///The PackageID parameter is provided to aid fast retrieval of Package details from Enterprise Architect, should this be
        ///required.
        /// </summary>
        /// <param name="PackageGuid">The unique ID identifying the project provided by the Add-In when a
        ///connection to a project branch of an Enterprise Architect model was first
        ///established.</param>
        /// <param name="PackageID">The PackageID of the Enterprise Architect Package the user has
        ///requested to have connected to an external project.</param>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise
        ///Architect model.Poll its members to retrieve model data and user interface status
        ///information.</param>
        /// <returns>Returns a non-zero to indicate that a connection has been made; a zero indicates that the user has not nominated a project
        /// and connection should not proceed.</returns>
        public int MDG_Connect(EA.Repository Repository, int PackageID, string PackageGuid)
        {
            mainViewModel.ConnectPackageWithJiraCommand.Execute(Repository.GetPackageByID (PackageID));
            return mainViewModel.ConnectPackageWithJiraCommand.Result ? PackageID : 0;
        }

        /// <summary>
        /// Add-Ins can use MDG_Disconnect to respond to user requests to disconnect the model branch from an external project.
        ///This function is called when the user attempts to disconnect an associated external project.The Add-In is required to
        ///delete the details of the connection.
        /// </summary>
        /// <param name="Repository"></param>
        /// <param name="PackageGuid"></param>
        /// <returns></returns>
        public int MDG_Disconnect(EA.Repository Repository, string PackageGuid)
        {
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Repository"></param>
        /// <returns>Returns an array of GUID strings representing individual Enterprise Architect Packages.</returns>
        public string[] MDG_GetConnectedPackages(EA.Repository Repository)
        {
            string[] result = new string[mainViewModel.ConnectedPackages.Count];
            for (int i = 0; i < mainViewModel.ConnectedPackages.Count; i++)
                result[i] = mainViewModel.ConnectedPackages[i].GUID;
            return result;
        }

        /// <summary>
        /// This function is called by Enterprise Architect to poll the Add-In for information relating to the PropertyName. This
        /// event should occur in as short a duration as possible, as Enterprise Architect does not cache the information provided by
        ///the function.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently-open Enterprise
        /// Architect model.Poll its members to retrieve model data and user interface status
        /// information.</param>
        /// <param name="PackageGuid">The GUID identifying the Enterprise Architect Package sub-tree that is
        /// controlled by the Add-In.</param>
        /// <param name="PropertyName">IconID - Return the name of a DLL and a resource identifier in the format #ResID, where the resource ID indicates
        /// an icon
        /// c:\program files\myapp\myapp.dlll#101, Language - Return the default language that Classes should be assigned when they are created in Enterprise
        /// Architect
        /// HiddenMenus - Return one or more values from the MDGMenus enumeration to hide menus that do not apply to
        /// your Add-In
        /// </param>
        /// <returns></returns>
        public object MDG_GetProperty(EA.Repository Repository, string PackageGuid, string PropertyName)
        {
            switch (PropertyName)
            {
                //case "IconID": return System.Reflection.Assembly.GetExecutingAssembly().Location + "#treeview.ico";
                case "IconID": return @"c:\Slávek\Projects\Jura\Debug\Resources.dll#treeview.ico";
                //case "IconID": return @"e:\Projects\Visual Studio Solutions\Pokusy\Resources\Debug\Resources.dll#101";
                case "Language": return "";
                case "HiddenMenus": return EA.MDGMenus.mgBuildProject | EA.MDGMenus.mgMerge | EA.MDGMenus.mgRun;
                default: return null;
            }
        }

        public int MDG_Merge(EA.Repository Repository, string PackageGuid, object SynchObjects, string SynchType, object ExportObjects, object ExportFiles, object ImportFiles, string IgnoreLocked, string Language)
        {
            return 1;
        }

        public string MDG_NewClass(EA.Repository Repository, string PackageGuid, string CodeID, string Language) => null;

        public int MDG_PostGenerate(EA.Repository Repository, string PackageGuid, string FilePath, string FileContents) => 0;

        public int MDG_PostMerge(EA.Repository Repository, string PackageGuid) => 0;

        public int MDG_PreGenerate(EA.Repository Repository, string PackageGuid) => 0;

        public int MDG_PreMerge(EA.Repository Repository, string PackageGuid) => 0;

        public void MDG_PreReverse(EA.Repository Repository, string PackageGuid, object FilePaths) { }

        public void MDG_RunExe(EA.Repository Repository, string PackageGuid) { }

        public int MDG_View(EA.Repository Repository, string PackageGuid, string CodeID) => 0;
        #endregion
    }
}
