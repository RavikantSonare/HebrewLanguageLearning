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
            _objModel = ObjDC.getUserProgress(); LessonId = Convert.ToString(_objModel.completedLesson + 1);
            VocabDecksLesson();
        }
        public void SetDataFromDataBase(string completedLesson, string completed_Screen_Status)
        {
            //  var Data = ObjDC.SetUserProgress(completedLesson, completed_Screen_Status);
            var Data = ObjDC.SetUserProgressScreen(completed_Screen_Status);
            LessonId = Convert.ToString(_objModel.completedLesson);
        }
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

        #endregion

        public void VocabDecksLesson()
        {
            //string LessonId = "3"; //Convert.ToString(System.Windows.Application.Current.Properties["WordName"]);
            try
            {
                int TotalValue;
                Random random = new Random();
                List<VocabularyModel> tmpVM = new List<VocabularyModel>();
                VocabularyLesson = _ObjBC.GetVocabDesksLessonData("HLL_VocabDecksLesson", LessonId);
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


                if (tmpVM.Count() < CurrentGroup.Count() && tmpVM.Count() < 8) { if (totalCheck <= CurrentGroup.Count) { goto _lblCheckAgain; totalCheck++; } }
                PnlWordChoiceList = new List<VocabularyModel>(tmpVM);
                PnlWordChoiceList.ForEach(z => { var Data = SetWord(z.LessonDecks); z.LessonDecks = Data.Length == 3 ? Data[2] : Data[0]; });


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

        //void SetCounter(int value, int Totalvalue)
        //{
        //    string completed_Screen_Status = "1";
        //    ReviewCounter = value + " out of " + Totalvalue;// + "(:=> " + tt;
        //    if (value != 0 && value == Totalvalue)
        //    {
        //        SetDataFromDataBase(LessonId, completed_Screen_Status);
        //    }
        //}
        public void SetCounter(int value, int Totalvalue)
        {
            ReviewCounter = value + " out of " + Totalvalue;
            string completed_Screen_Status = "2";
            if (value != 0 && value == Totalvalue)
            {
                if (CurruntScreenNo == 7)
                {
                    completed_Screen_Status = Convert.ToString(CurruntScreenNo + 1);
                }
                SetDataFromDataBase(LessonId, completed_Screen_Status);
            }

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
            }
        }
        public void MouseDown_WordClick(object sender, MouseEventArgs e)
        {

            string StrongNo = Convert.ToString(((System.Windows.FrameworkElement)sender).Tag);
            if (StrongNo == _ObjCurrentImage.StrongNo)
            {
                _ObjBC.UpdateReviewData("HLL_VocabDecksIsReviewPassive", StrongNo);

            }
            VocabDecksLesson();
            // HLL_VocabDecks
            // System.Windows.Application.Current.Shutdown();
        }
        // Goto Previes Pages
        public void MouseDown_RightPanel(object sender, MouseEventArgs e)
        {
            navigationService.NavigateToViewModel(typeof(BibleLearning.BibleLearningViewModel));
        }
    }
}
