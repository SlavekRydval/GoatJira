using GoatJira.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoatJira.Model
{
    interface IMainModelService
    {
        void SaveConnectedPackages(ObservableCollection<PackageModel> ConnectedPackages);
        void ReadConnectedPackages(ObservableCollection<PackageModel> ConnectedPackages);
    }
}
