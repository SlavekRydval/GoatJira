using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoatJira.Helpers
{
    class DialogService : IDialogService
    {
        public void ShowError(string Message)
        {
            throw new NotImplementedException();
        }

        public void ShowMessage(string Message)
        {
            throw new NotImplementedException();
        }

        public void ShowWarning(string Message)
        {
            throw new NotImplementedException();
        }

        public void ShowAboutDialog(object DataContext)
        {
            var About = new View.About ();
            About.DataContext = DataContext;
            About.ShowDialog();
        }

    }
}
