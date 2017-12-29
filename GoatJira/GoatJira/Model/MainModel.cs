using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace GoatJira.Model
{
    
    class MainModel
    {
        public ObservableCollection<PackageModel> ConnectedPackages = new ObservableCollection<PackageModel>();
    }
}
