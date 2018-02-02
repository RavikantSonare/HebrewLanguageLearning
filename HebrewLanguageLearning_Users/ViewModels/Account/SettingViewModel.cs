using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HebrewLanguageLearning_Users.Views.Account;
using HebrewLanguageLearning_Users.Models.DataControllers;

namespace HebrewLanguageLearning_Users.ViewModels.Account
{
    public class SettingViewModel : Screen
    {
        SettingController _ObjSC = new SettingController();
        private readonly INavigationService navigationService;
        public SettingViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
        public void btn_SyncData()
        {
            string UsersID = "1";
            var Result = _ObjSC.SyncData(UsersID);

        }
        public void btn_close()
        {
            navigationService.NavigateToViewModel(typeof(BibleLearning.BibleLearningViewModel));

        }

    }
}
