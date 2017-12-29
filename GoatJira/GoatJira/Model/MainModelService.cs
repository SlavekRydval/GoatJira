using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace GoatJira.Model
{
    class MainModelService : IMainModelService
    {
        public void ReadConnectedPackages(ObservableCollection<PackageModel> ConnectedPackages)
        {
            try
            {
                //ConnectedPackages = JsonConvert.DeserializeObject<ObservableCollection<PackageModel>>(File.ReadAllText(@"C:\TEMP\GGG.JSON"));
                ConnectedPackages.Clear();
                JsonConvert.PopulateObject(File.ReadAllText(@"C:\TEMP\GGG.JSON"), ConnectedPackages);
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
                File.WriteAllText(@"C:\TEMP\GGG.JSON", JsonConvert.SerializeObject(ConnectedPackages, Formatting.Indented));
            }
            catch (Exception)
            {
                //do nothing... saving was wrong... that is life... maybe in the future i should inform the user
            }




        }
    }
}
