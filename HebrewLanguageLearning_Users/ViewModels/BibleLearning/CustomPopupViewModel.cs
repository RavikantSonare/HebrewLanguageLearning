using Caliburn.Micro;
using HebrewLanguageLearning_Users.Models.DataControllers;
using HebrewLanguageLearning_Users.Models.DataModels;
using System;
using System.Windows;

namespace HebrewLanguageLearning_Users.ViewModels.BibleLearning
{

    public class CustomPopupViewModel : Conductor<object>
    {
        private readonly INavigationService navigationService;
        public CustomPopupViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

        }
        public void btn_Dash()
        {
            navigationService.NavigateToViewModel(typeof(Dashboard.DashboardViewModel));
        }
        public void btn_Next()
        {
            Continue_lesson();
        }
        public void Continue_lesson()
        {
            DashboardController ObjDC = new DashboardController();
            DashboardModel _objModel = new DashboardModel();
            _objModel = ObjDC.getUserProgress();
            Application.Current.Properties["CurretPage"] = _objModel.currentScreenStatus;
            var ScreenTemp = Application.Current.Properties["CurretPage"];
            int ScreenNo = 1;
            if (ScreenTemp != null)
            {
                ScreenNo = Convert.ToInt32(ScreenTemp);
            }

            switch (ScreenNo)
            {
                case 1:
                    navigationService.NavigateToViewModel(typeof(BibleLearning.BibleLearningFromMediaViewModel));
                    break;
                case 2:
                    navigationService.NavigateToViewModel(typeof(BibleLearning.BibleLearningFromMediaWordChoiceViewModel));
                    break;
                case 3:
                    navigationService.NavigateToViewModel(typeof(Game.JerichoGameViewModel));
                    break;
                case 4:
                    navigationService.NavigateToViewModel(typeof(BibleLearning.BibleLearningWordTypingViewModel));
                    break;
                case 5:
                    navigationService.NavigateToViewModel(typeof(Game.ConquerCityGameViewModel));
                    break;
                case 6:
                    navigationService.NavigateToViewModel(typeof(BibleLearning.BibleLearningFromMediaViewModel));
                    break;
                case 7:
                    navigationService.NavigateToViewModel(typeof(BibleLearning.BibleLearningFromMediaWordChoiceViewModel));
                    break;
                case 8:
                    navigationService.NavigateToViewModel(typeof(BibleLearning.BibleLearningWordTypingViewModel));
                    break;
                case 9:
                    navigationService.NavigateToViewModel(typeof(BibleLearning.BibleLearningWordTypingViewModel));
                    break;
                default:
                    navigationService.NavigateToViewModel(typeof(BibleLearning.BibleLearningFromMediaWordChoiceViewModel));
                    break;
            }
        }
    }
}
