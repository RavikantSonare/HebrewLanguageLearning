﻿using Caliburn.Micro;
using HebrewLanguageLearning_Users.Models.DataControllers;
using HebrewLanguageLearning_Users.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HebrewLanguageLearning_Users.ViewModels.Dashboard

{
    public class DashboardViewModel : Conductor<object>
    {
        DashboardController ObjDC = new DashboardController();
        DashboardModel _objModel = new DashboardModel();
        BibleLearningController _ObjBLC = new BibleLearningController();
        private readonly INavigationService navigationService;
        public DashboardViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            VocabDecksMenu();
        }
        //        //private string _bibleTxt = @"בראשית ברא אלוהים את השמים ואת הארץ.
        //והארץ היתה ללא צורה, וריק; ואת החושך על פני המעמקים.ורוח אלוהים נעה על פני המים.
        //ויאמר אלהים יהי אור ויהי אור.
        //וירא ה 'את האור, כי טוב: ואלוהים חילק את האור מהחושך.
        //ואלוהים קרא את יום האור, ואת החושך הוא קרא לילה.והערב והבוקר היו היום הראשון.
        //ויאמר אלהים, יהי ברקע בתוך המים, ונתן לו לחלק את המים מן המים.";
        //     private ItemsControl _progressBox;
        //     public ItemsControl ProgressBox
        //     {
        //         get { return _progressBox; }
        //         set
        //         {
        //             _bibleTxt.ItemsSource = Enumerable.Range(1, 60)
        //.Select(r => new KeyValuePair<string, string>(r.ToString(), r.ToString())).ToList(); ;
        //             NotifyOfPropertyChange(() => ProgressBox);
        //         }
        //     }
        //public string BibleTxt
        //{
        //    get { return _bibleTxt; }
        //    set
        //    {
        //        _bibleTxt = value;
        //        NotifyOfPropertyChange(() => BibleTxt);
        //    }
        //}
        protected override void OnActivate()
        {
            base.OnActivate();

            //MessageBox.Show("Deshboard");//This is for testing - currently doesn't display
        }
        public void btn_continue_passage()
        {
            navigationService.NavigateToViewModel(typeof(BibleLearning.BibleLearningViewModel));
        }
        public void btn_continue_lesson()
        {
            Application.Current.Properties["CurretProgressbar"] = null;
            Application.Current.Properties["CurretGreenDot"] = null;
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
        public List<VocabDecksGroupModel> _VocabularyLesson;
        public List<VocabDecksGroupModel> VocabularyLesson
        {
            get { return _VocabularyLesson; }
            set
            {
                _VocabularyLesson = value;
                NotifyOfPropertyChange(() => VocabularyLesson);
            }
        }
        public List<VocabDecksGroupModel> _VocabularyLesson_Custom;
        public List<VocabDecksGroupModel> VocabularyLesson_Custom
        {
            get { return _VocabularyLesson_Custom; }
            set
            {
                _VocabularyLesson_Custom = value;
                NotifyOfPropertyChange(() => VocabularyLesson_Custom);
            }
        }

        // HLL_VocabDecks_Custom

        public void VocabDecksMenu()
        {
            VocabularyLesson = _ObjBLC.GetVocabDesks("HLL_VocabDecks_RightPanel");
            VocabularyLesson_Custom = _ObjBLC.GetVocabDesks("HLL_VocabDecks_Custom");
        }
    }
}
