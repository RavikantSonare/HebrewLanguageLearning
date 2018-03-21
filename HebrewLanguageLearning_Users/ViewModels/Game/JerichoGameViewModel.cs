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

namespace HebrewLanguageLearning_Users.ViewModels.Game
{
    public class JerichoGameViewModel : Conductor<object>
    {
        BibleLearningController _ObjBC = new BibleLearningController();
        static List<DictionaryModel> _dictionaryModellist = new List<DictionaryModel>();
        SettingController _ObjSC = new SettingController();
        // Set Current Image
        static VocabularyModel _ObjCurrentImage = new VocabularyModel();

        private readonly INavigationService navigationService;
        public JerichoGameViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            string LessonId = "3";//Convert.ToString(System.Windows.Application.Current.Properties["WordName"]);
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


        #endregion  

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


        List<string> WidthList = new List<string>() { "150", "180", "200", "240", "130", "100" };
        public void VocabDecksLesson()
        {
            string LessonId = "3"; //Convert.ToString(System.Windows.Application.Current.Properties["WordName"]);
            try
            {
                int TotalValue;
                Random random = new Random();
                List<VocabularyModel> tmpVM = new List<VocabularyModel>();
                VocabularyLesson = _ObjBC.GetVocabDesksLessonData("HLL_VocabDecksLesson", LessonId);
                var CurrentGroup = VocabularyLesson.SelectMany(p => p.vocabularyModel).ToList();
                TotalValue = CurrentGroup.Count();
                CurrentGroup = CurrentGroup.Where(z => z.IsReviewPassive == false).ToList();
                SetCounter(TotalValue - CurrentGroup.Count(), TotalValue);
                var rand = new Random();
            _lblCheckAgain:

                
                foreach (var item in CurrentGroup)
                {
                    if (tmpVM.Count() < 18 && CurrentGroup.Count > 0)
                    {
                        var rndmTmp = CurrentGroup[rand.Next(CurrentGroup.Count)];
                        if (!tmpVM.Where(z => z.StrongNo == rndmTmp.StrongNo).Any())
                        {
                            tmpVM.Add(rndmTmp);
                        }
                    }

                }
                if (tmpVM.Count() < CurrentGroup.Count() && tmpVM.Count() < 18) { goto _lblCheckAgain; }
                PnlWordChoiceList = new List<VocabularyModel>(tmpVM);
                int i = 0;
                PnlWordChoiceList.ForEach(z => 
                { var Data = SetWord(z.LessonDecks); z.LessonDecks =  Convert.ToInt16(WidthList[i])>180? Data.Length == 3 ? Data[2] : Data[0]:""; z.DictionaryEntriesId = WidthList[i];
                   
                    if ((i+1)%6==0)
                    {
                        i = 0;
                        WidthList = Randomize(WidthList);

                    }else { i++; }
                   
                });


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
            finally { }
            //SetWord(CurrentGroup);
            // SetWord(s.LessonDecks); PnlWordChoiceList
        }

       
        private string[] SetWord(string currentGroup)
        {
            string[] tempArray = currentGroup.Split(',');
            return tempArray;
        }
        static int tt = 0;
        void SetCounter(int value, int Totalvalue)
        {
            ReviewCounter = value + " out of " + Totalvalue;// + "(:=> " + tt;
            //value != 0 &&
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
            tt++;
            string StrongNo = Convert.ToString(((System.Windows.FrameworkElement)sender).Tag);
            if (StrongNo == _ObjCurrentImage.StrongNo)
            {
                //_ObjBC.UpdateReviewData("HLL_VocabDecksIsReView", StrongNo);

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
