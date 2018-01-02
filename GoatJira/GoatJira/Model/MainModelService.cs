using GoatJira.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using GoatJira.Model.Package;

namespace GoatJira.Model
{
    class MainModelService : IMainModelService
    {

        /// <summary>
        /// Return full file name with a configuration.
        /// </summary>
        /// <param name="IDString">ID which distinguishes repostitory from each other</param>
        /// <returns></returns>
        private string GetFullFileName()
        {
            return $@"{Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)).FullName}\EAGoatJiraPackages.json";
        }


        public void ReadConnectedPackages(ObservableCollection<PackageModel> ConnectedPackages)
        {
            try
            {
                //ConnectedPackages = JsonConvert.DeserializeObject<ObservableCollection<PackageModel>>(File.ReadAllText(@"C:\TEMP\GGG.JSON"));
                ConnectedPackages.Clear();
                JsonConvert.PopulateObject(File.ReadAllText(GetFullFileName()), ConnectedPackages);
            }
            catch (Exception)
            {
                //do nothing... reading was wrong... that is life... maybe in the future i should inform the user
            }
        }

        public void SaveConnectedPackages(ObservableCollection<PackageModel> ConnectedPackages)
        {
            try
            {
                File.WriteAllText(GetFullFileName(), JsonConvert.SerializeObject(ConnectedPackages, Formatting.Indented));
            }
            catch (Exception)
            {
                //do nothing... saving was wrong... that is life... maybe in the future i should inform the user
            }




        }
    }
}
