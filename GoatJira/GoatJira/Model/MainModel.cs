using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace GoatJira.Model
{
    
    class MainModel : ObservableObject
    {
        public ObservableCollection<PackageModel> ConnectedPackages = new ObservableCollection<PackageModel>();
    }
}
