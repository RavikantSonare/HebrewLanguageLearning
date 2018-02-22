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

namespace HebrewLanguageLearning_Users.ViewModels.BibleLearning
{
    public class BibleLearningFromMediaWordChoiceViewModel : Conductor<object>
    {
        BibleLearningController _ObjBC = new BibleLearningController();
        static List<DictionaryModel> _dictionaryModellist = new List<DictionaryModel>();
        SettingController _ObjSC = new SettingController();
        // Set Current Image
        static VocabularyModel _ObjCurrentImage = new VocabularyModel();
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
        public string _bibleTxtMediaUrl = EntityConfig.MediaUri;

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


        public BibleLearningFromMediaWordChoiceViewModel(object ObjectData)
        {
            string LessonId = "1";//Convert.ToString(System.Windows.Application.Current.Properties["WordName"]);
            if (!string.IsNullOrEmpty(LessonId)) { VocabDecksLesson(LessonId.Replace("Lesson ", string.Empty)); };
        }

        public void VocabDecksLesson(string LessonId)
        {
            try
            {
                Random random = new Random();
                List<VocabularyModel> tmpVM = new List<VocabularyModel>();
                VocabularyLesson = _ObjBC.GetVocabDesksLessonData("HLL_VocabDecksLesson", LessonId);
                var CurrentGroup = VocabularyLesson.SelectMany(p => p.vocabularyModel).ToList();
                var rand = new Random();
                foreach (var item in CurrentGroup)
                {
                    if (tmpVM.Count() < 8)
                    {
                        tmpVM.Add(CurrentGroup[rand.Next(CurrentGroup.Count)]);
                    }
                }
                PnlWordChoiceList = new List<VocabularyModel>(tmpVM);
                PnlWordChoiceList.ForEach(z => { var Data = SetWord(z.LessonDecks); z.LessonDecks = Data.Length == 3 ? Data[2] : Data[0]; z.StrongNo = Data[0]; });
                SetCounter(CurrentGroup.Count());

                // set Rendom Logic
                _ObjCurrentImage = PnlWordChoiceList[rand.Next(PnlWordChoiceList.Count)];
                SetImage(_ObjCurrentImage.StrongNo);
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

        void SetCounter(int value)
        {
            ReviewCounter = "1 out of " + value;
        }
        void fileData()
        {
            if(_dictionaryModellist.Count()<1)
            _dictionaryModellist = _ObjSC.getDataFromLocalFile();
        }
        void SetImage(string strongNo)
        {
            fileData();
            var DicData = _dictionaryModellist.FirstOrDefault(x => x.DicStrongNo == strongNo);
            if (DicData != null)
            {
                BibleTxtMediaUrl += DicData.ListOfPictures.LastOrDefault();
            }
        }
    }
}
