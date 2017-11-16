using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebrewLanguageLearning_Users.ViewModels
{
    public class RootViewModel : Conductor<object>
    {
        public RootViewModel()
        {
           LoadLoginView();
        }

        public void LoadLoginView()
        {
            ActivateItem(new Account.LoginViewModel());
        }
       
    }
}
