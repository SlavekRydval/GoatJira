using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoatJira.Helpers
{
    interface IDialogService
    {

        void ShowMessage(string Message);
        void ShowError(string Message);
        void ShowWarning(string Message);

        void ShowAboutDialog();


    }
}
