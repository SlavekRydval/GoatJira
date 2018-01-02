using System.Collections.ObjectModel;
using GoatJira.Model.Package;

namespace GoatJira.Model
{
    interface IMainModelService
    {
        void SaveConnectedPackages(ObservableCollection<PackageModel> ConnectedPackages);
        void ReadConnectedPackages(ObservableCollection<PackageModel> ConnectedPackages);
    }
}
