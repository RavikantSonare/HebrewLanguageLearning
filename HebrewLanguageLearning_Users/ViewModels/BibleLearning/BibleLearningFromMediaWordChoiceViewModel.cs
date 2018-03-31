using Caliburn.Micro;
using HebrewLanguageLearning_Users.Models.DataControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HebrewLanguageLearning_Users.Models.DataModels;
using HebrewLanguageLearning_Users.CommonHelper;
using System.Windows.Controls;
using HebrewLanguageLearning_Users.GenericClasses;
using System.Windows.Forms;
using HebrewLanguageLearning_Users.Views.CommonUserControls;
using System.Timers;

namespace HebrewLanguageLearning_Users.ViewModels.BibleLearning
{

    public class BibleLearningFromMediaWordChoiceViewModel : Conductor<object>
    {
        BibleLearningController _ObjBC = new BibleLearningController();
        static List<DictionaryModel> _dictionaryModellist = new List<DictionaryModel>();
        SettingController _ObjSC = new SettingController();

        // Getting Data From Database
        DashboardController ObjDC = new DashboardController();
        DashboardModel _objModel = new DashboardModel();

        // Set Lesson Id
        static string LessonId = string.Empty;

        // Set Currunt Screen
        static int CurruntScreenNo = 2;

        // Set Current Image
        static VocabularyModel _ObjCurrentImage = new VocabularyModel();



        private readonly INavigationService navigationService;
        public BibleLearningFromMediaWordChoiceViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            var ScreenTemp = System.Windows.Application.Current.Properties["CurretPage"];
            if (ScreenTemp != null)
            {
                CurruntScreenNo = Convert.ToInt32(ScreenTemp);
            }
            GetDataFromDataBase();
            // Convert.ToString(System.Windows.Application.Current.Properties["WordName"]);
            //string LessonId = Convert.ToString(System.Windows.Application.Current.Properties["WordLessonId"]);
            //if (!string.IsNullOrEmpty(LessonId)) { VocabDecksLesson(); };
        }
        public void GetDataFromDataBase()
        {
            _objModel = ObjDC.getUserProgress();
            switch (CurruntScreenNo)
            {
                case 101:
                    LessonId = Convert.ToString(System.Windows.Application.Current.Properties["LessonId"]);
                    break;
                case 102:
                    LessonId = Convert.ToString(System.Windows.Application.Current.Properties["LessonId"]);
                    break;
                default:
                    LessonId = Convert.ToString(_objModel.completedLesson + 1);
                    break;
            }
            VocabDecksLesson();
        }
        //public void GetDataFromDataBase()
        //{
        //    _objModel = ObjDC.getUserProgress(); LessonId = Convert.ToString(_objModel.completedLesson + 1);
        //    VocabDecksLesson();
        //}
        //public void SetDataFromDataBase(string completedLesson, string completed_Screen_Status)
        //{
        //    //  var Data = ObjDC.SetUserProgress(completedLesson, completed_Screen_Status);
        //    var Data = ObjDC.SetUserProgressScreen(completed_Screen_Status);
        //    LessonId = Convert.ToString(_objModel.completedLesson);
        //}
        protected override void OnActivate()
        {
            base.OnActivate();
            fileData();
        }


        #region Property
        public string _reviewCounter;
        public string _bibleTxtVocabdocks;
        public string _bibleTxtHebrew;
        public string _bibleTxtEnglish;
        public List<VocabularyModel> _pnlWordChoiceList;
        public string _bibleTxtMediaUrl;
        public string _borderBoxColor = "#A9A9A9";//#A9CDDA
        public string _rightAnsShow = "Collapsed";

        public string ReviewCounter
        {
            get { return _reviewCounter; }
            set
            {
                _reviewCounter = value;
                NotifyOfPropertyChange(() => ReviewCounter);
            }
        }
        public string BibleTxtVocabdocks
        {
            get { return _bibleTxtVocabdocks; }
            set
            {
                _bibleTxtVocabdocks = value;
                NotifyOfPropertyChange(() => BibleTxtVocabdocks);
            }
        }
        public string BibleTxtHebrew
        {
            get { return _bibleTxtHebrew; }
            set
            {
                _bibleTxtHebrew = value;
                NotifyOfPropertyChange(() => BibleTxtHebrew);
            }
        }
        public string BibleTxtEnglish
        {
            get { return _bibleTxtEnglish; }
            set
            {
                _bibleTxtEnglish = value;
                NotifyOfPropertyChange(() => BibleTxtEnglish);
            }
        }

        public List<VocabularyModel> PnlWordChoiceList
        {
            get { return _pnlWordChoiceList; }
            set
            {
                _pnlWordChoiceList = value;
                NotifyOfPropertyChange(() => PnlWordChoiceList);
            }
        }
        public List<VocabDecksGroupModel> VocabularyLesson { get; private set; }

        public string BibleTxtMediaUrl
        {
            get { return _bibleTxtMediaUrl; }
            set
            {
                _bibleTxtMediaUrl = value;
                NotifyOfPropertyChange(() => BibleTxtMediaUrl);
            }
        }

        public string _showPapUp = "Collapsed";

        public string ShowPapUp
        {
            get { return _showPapUp; }
            set
            {
                _showPapUp = value;
                NotifyOfPropertyChange(() => ShowPapUp);
            }
        }

        public string BorderBoxColor
        {
            get { return _borderBoxColor; }
            set
            {
                _borderBoxColor = value;
                NotifyOfPropertyChange(() => _borderBoxColor);
            }
        }        
        public string _bibleWrongSoundMediaUrl;
        public string BibleWrongSoundMediaUrl
        {
            get { return _bibleWrongSoundMediaUrl; }
            set
            {
                _bibleWrongSoundMediaUrl = value;
                NotifyOfPropertyChange(() => BibleWrongSoundMediaUrl);
            }
        }

        public string _bibleSoundMediaUrl;
        public string BibleSoundMediaUrl
        {
            get { return _bibleSoundMediaUrl; }
            set
            {
                _bibleSoundMediaUrl = value;
                NotifyOfPropertyChange(() => BibleSoundMediaUrl);
            }
        }

        public string RightAnsShow
        {
            get { return _rightAnsShow; }
            set
            {
                _rightAnsShow = value;
                NotifyOfPropertyChange(() => RightAnsShow);
            }
        }

        private MediaElement _mediaElementObject;

        public MediaElement MediaElementObject
        {
            get { return _mediaElementObject; }
            set
            {
                _mediaElementObject = value;
                NotifyOfPropertyChange(() => MediaElementObject);
            }
        }
        #endregion

        public void VocabDecksLesson()
        {
            //string LessonId = "3"; //Convert.ToString(System.Windows.Application.Current.Properties["WordName"]);
            try
            {
                RightAnsShow = "Collapsed";
                int TotalValue;
                Random random = new Random();
                List<VocabularyModel> tmpVM = new List<VocabularyModel>();

                switch (CurruntScreenNo)
                {
                    case 101:
                        VocabularyLesson = _ObjBC.GetVocabDesksLessonData("HLL_VocabDecks_CustomSecond", LessonId);
                        break;
                    case 102:
                        VocabularyLesson = _ObjBC.GetVocabDesksLessonData("HLL_VocabDecksLesson", LessonId);
                        break;
                    default:
                        VocabularyLesson = _ObjBC.GetVocabDesksLessonData("HLL_VocabDecksLesson", LessonId);
                        break;
                }
                //VocabularyLesson = _ObjBC.GetVocabDesksLessonData("HLL_VocabDecksLesson", LessonId);

                var CurrentGroup = VocabularyLesson.SelectMany(p => p.vocabularyModel).ToList();
                TotalValue = CurrentGroup.Count();

                switch (CurruntScreenNo)
                {
                    case 2:
                        CurrentGroup = CurrentGroup.Where(z => z.IsReviewPassive == false).ToList();
                        break;
                    case 7:
                        CurrentGroup = CurrentGroup.Where(z => z.IsPassiveKnowledgeGrammar == false).ToList();
                        break;
                    case 101:
                        CurrentGroup = CurrentGroup.Where(z => z.IsReviewPassive == false).ToList();
                        break;
                    case 102:
                        CurrentGroup = CurrentGroup.Where(z => z.IsLessonPassiveKnowledg == false).ToList();
                        break;
                    default:
                        CurrentGroup = CurrentGroup.Where(z => z.IsReviewPassive == false).ToList();
                        break;
                }

                SetCounter(TotalValue - CurrentGroup.Count(), TotalValue);
                int totalCheck = 0;
                var rand = new Random();
            _lblCheckAgain:
                foreach (var item in CurrentGroup)
                {
                    if (tmpVM.Count() < 8 && CurrentGroup.Count > 0)
                    {
                        var rndmTmp = CurrentGroup[rand.Next(CurrentGroup.Count)];
                        if (!tmpVM.Where(z => z.StrongNo == rndmTmp.StrongNo).Any())
                        {
                            tmpVM.Add(rndmTmp);
                        }
                    }

                }


                if (tmpVM.Count() < CurrentGroup.Count() && tmpVM.Count() < 8) { if (totalCheck <= CurrentGroup.Count) { totalCheck++;
                        goto  _lblCheckAgain;  } }
                PnlWordChoiceList = new List<VocabularyModel>(tmpVM);
                // Set 2 for English Level
                PnlWordChoiceList.ForEach(z => { var Data = SetWord(z.LessonDecks); z.LessonDecks = Data.Length == 3 ? Data[1] : Data[0]; z.Description = "#A9CDDA"; });


                // set Rendom Logic
                if (PnlWordChoiceList.Count > 0)
                {
                    _ObjCurrentImage = PnlWordChoiceList[rand.Next(PnlWordChoiceList.Count)];
                    SetImage(_ObjCurrentImage.StrongNo);
                }
                else
                {

                    BibleTxtMediaUrl = "";
                }

            }
            catch (Exception ex) { }
            finally { }
            //SetWord(CurrentGroup);
            // SetWord(s.LessonDecks); PnlWordChoiceList
        }

        private string[] SetWord(string currentGroup)
        {
            string[] tempArray = currentGroup.Split(',');
            return tempArray;
        }

        public void SetDataFromDataBase(string completed_Screen_Status)
        {
            switch (CurruntScreenNo)
            {
                case 101:
                    ObjDC.SetSecondUserProgressScreen(completed_Screen_Status, false);
                    break;
                case 102:
                    ObjDC.SetSecondUserProgressScreen(completed_Screen_Status, true);
                    break;
                default:
                    ObjDC.SetUserProgressScreen(completed_Screen_Status);
                    break;
            }
        }
        public void SetCounter(int value, int Totalvalue)
        {
            ReviewCounter = value + " out of " + Totalvalue;
            string completed_Screen_Status = "3";
            
            if (value != 0 && value == Totalvalue)
            {
                switch (CurruntScreenNo)
                {
                    case 7:
                        completed_Screen_Status = "8";
                        break;
                    case 101:
                        completed_Screen_Status = "3";
                        break;
                    case 102:
                        completed_Screen_Status = "2";
                        break;
                }
                SetDataFromDataBase(completed_Screen_Status);
                showPopup();
            }

        }

        public void showPopup()
        {
            ShowPapUp = "Visible";

        }
        public void btn_Dash()
        {
            navigationService.NavigateToViewModel(typeof(Dashboard.DashboardViewModel));
        }
        public void btn_Next()
        {
            navigationService.NavigateToViewModel(typeof(CustomPopupViewModel));
        }
        void fileData()
        {
            if (_dictionaryModellist.Count() < 1)
                _dictionaryModellist = _ObjSC.getDataFromLocalFile();
        }
        void SetImage(string strongNo)
        {
            fileData();
            var DicData = _dictionaryModellist.FirstOrDefault(x => x.DicStrongNo == strongNo);
            if (DicData != null)
            {
                BibleTxtMediaUrl = EntityConfig.MediaUriPictures + DicData.ListOfPictures.LastOrDefault();
                BibleSoundMediaUrl = @"" + EntityConfig.MediaUriSounds + DicData.SoundUrl;
            }
        }
        public void MouseDown_WordClick(object sender, MouseEventArgs e)
        {
            if (RightAnsShow == "Visible")
            {
                return;
            }
            string StrongNo = Convert.ToString(((System.Windows.FrameworkElement)sender).Tag);
            string _currectStrongNo = _ObjCurrentImage.StrongNo;
            if (StrongNo == _currectStrongNo)
            {
                // PnlWordChoiceList.ForEach(z => { z.description = "#008000"; });
                PnlWordChoiceList.ForEach(z => { var CheckColor = z.StrongNo == StrongNo ? "#008000" : "#A9CDDA"; z.Description = CheckColor; });
                PnlWordChoiceList = new List<VocabularyModel>(PnlWordChoiceList);
                PlaySound(BibleSoundMediaUrl);
                BorderBoxColorLogic(0);
               
                switch (CurruntScreenNo)
                {
                    case 2:
                        _ObjBC.UpdateReviewData("HLL_VocabDecksIsReviewPassive", StrongNo);

                        break;
                    case 7:
                        _ObjBC.UpdateReviewData("HLL_VocabDecksIsPassiveKnowledgeGrammar", StrongNo);
                        break;
                    case 101:
                        _ObjBC.UpdateReviewDataByLesson("HLL_VocabDecksLearnIsReviewPassive", LessonId, StrongNo);
                        break;
                    case 102:
                        _ObjBC.UpdateReviewDataByLesson("HLL_VocabDecksIsLessonPassiveKnowledg", LessonId, StrongNo);
                        break;

                }
            }
            else
            {
                PnlWordChoiceList.ForEach(z =>
                {
                    var CheckColor = z.StrongNo == StrongNo ? "#FF0000" : "#A9CDDA"; z.Description = CheckColor;
                    var CheckColorWrong = z.StrongNo == _currectStrongNo ? "#008000" : z.Description; z.Description = CheckColorWrong;
                });
                var DicData = _dictionaryModellist.FirstOrDefault(x => x.DicStrongNo == StrongNo);
                BibleWrongSoundMediaUrl = @"" + EntityConfig.MediaUriSounds + DicData.SoundUrl;
                PnlWordChoiceList = new List<VocabularyModel>(PnlWordChoiceList);
                BorderBoxColorLogic(1);
              
                    if (RightAnsShow != "Visible")
                    {
                        RightAnsShow = "Visible";
                    }
                    else
                    {
                        RightAnsShow = "Collapsed";
                    }
               
            }
            //  VocabDecksLesson();
            // HLL_VocabDecks
            // System.Windows.Application.Current.Shutdown();
        }
        // Goto Previes Pages
        public void MouseDown_RightPanel(object sender, MouseEventArgs e)
        {
            navigationService.NavigateToViewModel(typeof(BibleLearning.BibleLearningViewModel));
        }

        public void MouseDown_LeftArrowImageClick(object sender, MouseEventArgs e)
        {
           
            VocabDecksLesson();
        }
        void BorderBoxColorLogic(int isCurrect)
        {
            switch (isCurrect)
            {
                case 0:
                    BorderBoxColor = "#008000";
                    SetTimer(2000);
                    break;
                case 1:
                    BorderBoxColor = "#FF0000";
                    break;
                default:
                    BorderBoxColor = "#A9A9A9";
                    break;
            }


        }
        private static System.Timers.Timer aTimer;
        private void SetTimer(int Tm)
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(Tm);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            // RightWorldTextBlock = "";
            BorderBoxColor = "#A9A9A9";
            aTimer.Stop();
            VocabDecksLesson();
          
        }

        public void MouseDown_SoundClick(object sender, MouseEventArgs e)
        {
            string SoundName = Convert.ToString(((System.Windows.FrameworkElement)sender).Name);
            if (SoundName == "imageRightSound")
            {
                PlaySound(BibleSoundMediaUrl);
            }
            else
            {
                PlaySound(BibleWrongSoundMediaUrl);
            }
            
        }
        void PlaySound(string BibleSoundMediaUrl)
        {
            MediaElementObject = new MediaElement()
            {
                LoadedBehavior = MediaState.Manual,
            };
            MediaElementObject.Source = new Uri(BibleSoundMediaUrl);// "ELL_PART_5_768k.wmv");  //ELL_PART_5_768k.wmv
            MediaElementObject.Play();
        }
    }
}
