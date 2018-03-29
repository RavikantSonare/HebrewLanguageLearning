using Caliburn.Micro;
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
using HebrewLanguageLearning_Users.Models.DataControllers;
using HebrewLanguageLearning_Users.ViewModels.BibleLearning;

namespace HebrewLanguageLearning_Users.ViewModels.Game
{
    public class JerichoGameViewModel : Conductor<object>
    {
        BibleLearningController _ObjBC = new BibleLearningController();
        static List<DictionaryModel> _dictionaryModellist = new List<DictionaryModel>();
        SettingController _ObjSC = new SettingController();
        // Set Current Image
        static VocabularyModel _ObjCurrentImage = new VocabularyModel();

        // Set Current Screen
        DashboardController ObjDC = new DashboardController();
        DashboardModel _objModel = new DashboardModel();
        // Set Lesson Id
        static string LessonId = string.Empty;

        private readonly INavigationService navigationService;
        public JerichoGameViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            GetDataFromDataBase();
            //string LessonId = Convert.ToString(System.Windows.Application.Current.Properties["WordName"]);
            if (!string.IsNullOrEmpty(LessonId)) { VocabDecksLesson(); };
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
        #endregion

        public void GetDataFromDataBase()
        {
            _objModel = ObjDC.getUserProgress(); LessonId = Convert.ToString(_objModel.completedLesson + 1);

        }
        public static List<T> Randomize<T>(List<T> list)
        {
            List<T> randomizedList = new List<T>();
            Random rnd = new Random();
            while (list.Count > 0)
            {
                int index = rnd.Next(0, list.Count); //pick a random item from the master list
                randomizedList.Add(list[index]); //place it at the end of the randomized list
                list.RemoveAt(index);
            }
            return randomizedList;
        }


        List<string> WidthList = new List<string>() { "170", "200", "220", "247", "120" };
        public void VocabDecksLesson()
        {
            //Convert.ToString(System.Windows.Application.Current.Properties["WordName"]);
            try
            {
                int TotalValue;
                Random random = new Random();
                List<VocabularyModel> tmpVM = new List<VocabularyModel>();
                VocabularyLesson = _ObjBC.GetVocabDesksLessonData("HLL_VocabDecksLesson", LessonId);
                var CurrentGroup = VocabularyLesson.SelectMany(p => p.vocabularyModel).ToList();
                var AllListData = new List<VocabularyModel>(CurrentGroup);
                TotalValue = CurrentGroup.Count();
                CurrentGroup = CurrentGroup.Where(z => z.IsReviewGameA == false).ToList();
                SetCounter(TotalValue - CurrentGroup.Count(), TotalValue);
                var rand = new Random();

                int _maxItration = 0;
            _lblCheckAgain:
                foreach (var item in CurrentGroup)
                {
                    if (tmpVM.Count() < 15 && CurrentGroup.Count > 0)
                    {
                        var rndmTmp = CurrentGroup[rand.Next(CurrentGroup.Count)];
                        if (!tmpVM.Where(z => z.StrongNo == rndmTmp.StrongNo).Any())
                        {
                            tmpVM.Add(rndmTmp);
                        }
                    }

                }
                if (tmpVM.Count() < CurrentGroup.Count() && tmpVM.Count() < 15 && CurrentGroup.Count() > _maxItration) { _maxItration++; goto _lblCheckAgain;  }
                int cTempListCount = tmpVM.Count();
                //if (cTempListCount < 18)
                //{
                //    var NewTemp = AllListData.Take(18-cTempListCount).ToList();
                //    tmpVM.AddRange(NewTemp);
                //}

                PnlWordChoiceList = new List<VocabularyModel>(tmpVM);


                int i = 0;
                PnlWordChoiceList.ForEach(z =>
                {
                    var Data = SetWord(z.LessonDecks); z.LessonDecks =
                    Convert.ToInt16(WidthList[i]) > 180 || PnlWordChoiceList.Count <= 5 ? Data.Length == 3 ? Data[2] : Data[0] : "";
                    z.DictionaryEntriesId = WidthList[i];

                    if ((i + 1) % 5 == 0)
                    {
                        i = 0;
                        WidthList = Randomize(WidthList);

                    }
                    else { i++; }

                });


                // set Rendom Logic
                if (PnlWordChoiceList.Count > 0)
                {
                    var tmpItemList = new List<VocabularyModel>(PnlWordChoiceList.Where(d => d.LessonDecks != "" && d.IsReviewGameA == false)).ToList();
                    if (tmpItemList.Count > 0)
                    {
                        _ObjCurrentImage = tmpItemList[rand.Next(tmpItemList.Count)];
                        SetImage(_ObjCurrentImage.StrongNo);
                    }

                }
                else
                {

                    BibleTxtMediaUrl = "";
                }

            }
            finally { }
            //SetWord(CurrentGroup);
            // SetWord(s.LessonDecks); PnlWordChoiceList
        }


        private string[] SetWord(string currentGroup)
        {
            string[] tempArray = currentGroup.Split(',');
            return tempArray;
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
                _ObjBC.UpdateReviewData("HLL_VocabDecksIsReviewGameA", StrongNo);

            }
            VocabDecksLesson();
        }
        public void SetDataFromDataBase(string completed_Screen_Status)
        {
            var Data = ObjDC.SetUserProgressScreen(completed_Screen_Status);
        }
        public void SetCounter(int value, int Totalvalue)
        {
            ReviewCounter = value + " out of " + Totalvalue;
            string completed_Screen_Status = "4";
            if (value != 0 && value == Totalvalue)
            {
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
        // Goto Previes Pages
        public void MouseDown_RightPanel(object sender, MouseEventArgs e)
        {
            navigationService.NavigateToViewModel(typeof(BibleLearning.BibleLearningViewModel));
        }
    }
}
