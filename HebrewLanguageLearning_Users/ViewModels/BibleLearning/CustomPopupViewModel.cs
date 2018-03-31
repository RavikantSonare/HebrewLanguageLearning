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
        private int appCurrentphase;
        public CustomPopupViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            var tmpPhase = Application.Current.Properties["Currentphase"];
            if (tmpPhase != null)
            {
                appCurrentphase = Convert.ToInt32(tmpPhase);
                switch (appCurrentphase)
                {
                    case 2:
                        Continue_lessonPhase2_Review();
                        break;

                    case 3:
                        Continue_lessonPhase3_Learn();
                        break;

                    default:
                        Continue_lessonPhase1();
                        break;
                }
            }
            else
            {
                Continue_lessonPhase1();
            }
        }
        public void btn_Dash()
        {
            navigationService.NavigateToViewModel(typeof(Dashboard.DashboardViewModel));
        }
        public void btn_Next()
        {
            Continue_lessonPhase1();
        }
        public void Continue_lessonPhase1()
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
        public void Continue_lessonPhase2_Review()
        {


            DashboardController ObjDC = new DashboardController();
            DashboardModel _objModel = new DashboardModel();
            _objModel = ObjDC.getUserProgress();
            Application.Current.Properties["CurretPage"] = _objModel.IsReviewProgress;
            var ScreenTemp = Application.Current.Properties["CurretPage"];
            int ScreenNo = 1;
            if (ScreenTemp != null)
            {
                ScreenNo = Convert.ToInt32(ScreenTemp);
            }
            System.Windows.Application.Current.Properties["CurretPage"] = 102;
            switch (ScreenNo)
            {
                case 1:
                    System.Windows.Application.Current.Properties["CurretGreenDot"] = 1;
                    navigationService.NavigateToViewModel(typeof(BibleLearningFromMediaWordChoiceViewModel));
                    break;
                case 2:
                    System.Windows.Application.Current.Properties["CurretGreenDot"] = 2;
                    navigationService.NavigateToViewModel(typeof(BibleLearningWordTypingViewModel));
                    break;
                
            }
            

        }
        public void Continue_lessonPhase3_Learn()
        {


            DashboardController ObjDC = new DashboardController();
            DashboardModel _objModel = new DashboardModel();
            _objModel = ObjDC.getUserProgress();
            Application.Current.Properties["CurretPage"] = _objModel.IsLearnProgress;
            var ScreenTemp = Application.Current.Properties["CurretPage"];
            int ScreenNo = 1;
            if (ScreenTemp != null)
            {
                ScreenNo = Convert.ToInt32(ScreenTemp);
            }
            System.Windows.Application.Current.Properties["CurretPage"] = 101;
            switch (ScreenNo)
            {
                case 1:

                    System.Windows.Application.Current.Properties["CurretGreenDot"] = 1;
                    navigationService.NavigateToViewModel(typeof(BibleLearningFromMediaViewModel));
                    break;
                case 2:
                    System.Windows.Application.Current.Properties["CurretGreenDot"] = 2;
                    navigationService.NavigateToViewModel(typeof(BibleLearningFromMediaWordChoiceViewModel));
                    break;
                case 3:
                    System.Windows.Application.Current.Properties["CurretGreenDot"] = 4;
                    navigationService.NavigateToViewModel(typeof(BibleLearningWordTypingViewModel));
                    break;

            }

           
        }
    }
}
