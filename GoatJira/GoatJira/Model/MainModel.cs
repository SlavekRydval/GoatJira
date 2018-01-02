using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GoatJira.Model.Package;

namespace GoatJira.Model
{
    
    class MainModel : ObservableObject
    {
        public ObservableCollection<PackageModel> ConnectedPackages = new ObservableCollection<PackageModel>();
    }
}
