using Caliburn.Micro;
using HebrewLanguageLearning_Users.Models.DataControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using HebrewLanguageLearning_Users.Models.DataModels;
using System.Windows.Controls;
using HebrewLanguageLearning_Users.GenericClasses;
using System.Windows.Forms;


namespace HebrewLanguageLearning_Users.ViewModels.BibleLearning
{
    // This Is 10 association v2

    public class BibleLearningFromMediaViewModel : Conductor<object>
    {
        BibleLearningController _ObjBC = new BibleLearningController();
        static List<DictionaryModel> _dictionaryModellist = new List<DictionaryModel>();
        SettingController _ObjSC = new SettingController();

        // Getting Data From Database
        DashboardController ObjDC = new DashboardController();
        DashboardModel _objModel = new DashboardModel();

        // Set Lesson Id
        static string LessonId = string.Empty;

        // Set Current Image
        static VocabularyModel _ObjCurrentImage = new VocabularyModel();

        private readonly INavigationService navigationService;
        public BibleLearningFromMediaViewModel(INavigationService navigationService)
        {
            GetDataFromDataBase();
            if (navigationService != null)
                this.navigationService = navigationService;
            //string LessonId = "3";//Convert.ToString(System.Windows.Application.Current.Properties["WordName"]);
            if (!string.IsNullOrEmpty(LessonId)) { VocabDecksLesson(); };
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
        public string _bibleSoundMediaUrl;

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
        public string BibleSoundMediaUrl
        {
            get { return _bibleSoundMediaUrl; }
            set
            {
                _bibleSoundMediaUrl = value;
                NotifyOfPropertyChange(() => BibleSoundMediaUrl);
            }
        }
        #endregion

        public void GetDataFromDataBase()
        {
            _objModel = ObjDC.getUserProgress(); LessonId = Convert.ToString(_objModel.completedLesson + 1);

        }

        public void VocabDecksLesson()
        {
            GetDataFromDataBase();
            //string LessonId = "3"; //Convert.ToString(System.Windows.Application.Current.Properties["WordName"]);
            try
            {
                int TotalValue;
                Random random = new Random();
                List<VocabularyModel> tmpVM = new List<VocabularyModel>();
                VocabularyLesson = _ObjBC.GetVocabDesksLessonData("HLL_VocabDecksLesson", LessonId);
                var CurrentGroup = VocabularyLesson.SelectMany(p => p.vocabularyModel).ToList();
                TotalValue = CurrentGroup.Count();
                var TakeData = CurrentGroup.Where(z => z.IsReviewAssociation == false).ToList();
                SetCounter(TotalValue - TakeData.Count(), TotalValue);
                var rand = new Random();
                //_lblCheckAgain:
                //    foreach (var item in CurrentGroup)
                //    {
                //        if (tmpVM.Count() < 20 && CurrentGroup.Count > 0)
                //        {
                //            //var rndmTmp = CurrentGroup[rand.Next(CurrentGroup.Count)];
                //            //if (!tmpVM.Where(z => z.StrongNo == rndmTmp.StrongNo).Any())
                //            //{
                //                tmpVM.Add(rndmTmp);
                //            //}
                //        }

                //    }
                //    //  _dictionaryModellist.ToList();
                //    if (tmpVM.Count() < CurrentGroup.Count() && tmpVM.Count() < 20) { goto _lblCheckAgain; }
                PnlWordChoiceList = new List<VocabularyModel>(CurrentGroup);
                PnlWordChoiceList.ForEach(z =>
                {
                    var Data = SetWord(z.LessonDecks); z.LessonDecks = Data.Length == 3 ? Data[2] : Data[0]; z.Description = _getImageUrl(z.StrongNo);
                    if (z.IsReviewAssociation == true) { z.DictionaryEntriesId = "#909090"; } else { z.DictionaryEntriesId = "#eaeaea"; }
                });
                // set Rendom Logic
                if (PnlWordChoiceList.Count > 0)
                {
                    _ObjCurrentImage = PnlWordChoiceList.FirstOrDefault();
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

        private string _getImageUrl(string strongNo)
        {
            if (!string.IsNullOrEmpty(strongNo))
            {
                fileData();
                var tmpData = _dictionaryModellist.FirstOrDefault(x => x.DicStrongNo == strongNo);
                if (tmpData != null)
                {
                    var tmpData1 = tmpData.ListOfPictures.LastOrDefault();
                    if (tmpData1 != null)
                    {
                        return EntityConfig.MediaUriPictures + tmpData1;
                    }
                }

            }
            return null;
        }

        private string[] SetWord(string currentGroup)
        {
            string[] tempArray = currentGroup.Split(',');
            return tempArray;
        }

        public void SetDataFromDataBase(string completedLesson)
        {
            var Data = ObjDC.SetUserProgress(completedLesson);
            LessonId = Convert.ToString(_objModel.completedLesson + 1);
        }

        void SetCounter(int value, int Totalvalue)
        {
            ReviewCounter = value + " out of " + Totalvalue;

            if (value == Totalvalue) { SetDataFromDataBase(LessonId); }


            // + "(:=> " + tt;
            //  navigationService.NavigateToViewModel(typeof(BibleLearningFromMediaWordChoiceViewModel));
            // Setting Of Data
        }
        void fileData()
        {
            if (_dictionaryModellist.Count() < 1)
                _dictionaryModellist = _ObjSC.getDataFromLocalFile();
        }
        public void SetImage(string strongNo)
        {
            fileData();
            var DicData = _dictionaryModellist.FirstOrDefault(x => x.DicStrongNo == strongNo);
            if (DicData != null && string.IsNullOrEmpty(BibleTxtMediaUrl))
            {
                BibleTxtMediaUrl = @"" + EntityConfig.MediaUriPictures + DicData.ListOfPictures.LastOrDefault();
                BibleSoundMediaUrl = @"" + EntityConfig.MediaUriSounds + DicData.SoundUrl;//EntityConfig.MediaUriSounds +
            }
            else
            {
                BibleTxtMediaUrl = @"" + EntityConfig.MediaUriPictures + DicData.ListOfPictures.LastOrDefault();
                BibleSoundMediaUrl = @"" + EntityConfig.MediaUriSounds + DicData.SoundUrl;
            }
        }
        public void MouseDown_ImageClick(object sender, MouseEventArgs e)
        {

            string StrongNo = Convert.ToString(((System.Windows.FrameworkElement)sender).Tag);
            string ImageUrl = Convert.ToString(((System.Windows.FrameworkElement)sender).Uid);
            //BibleTxtMediaUrl = ImageUrl;
            //BibleSoundMediaUrl = @"C:\Users\mobiweb\AppData\Local\HebrewLanguageLearning_Users\Media\Sounds\DictionaryEntriesHLLUSA001-SOUND00R_Sounds532018105917.wav";

            _ObjBC.UpdateReviewData("HLL_VocabDecksIsReviewAssociation", StrongNo);
            SetImage(StrongNo);


            // VocabDecksLesson();
            // HLL_VocabDecks
            // System.Windows.Application.Current.Shutdown();
        }
        // Goto Previes Pages
        public void MouseDown_RightPanel(object sender, MouseEventArgs e)
        {
            navigationService.NavigateToViewModel(typeof(BibleLearning.BibleLearningViewModel));
        }
        public void MouseDown_SoundClick(object sender, MouseEventArgs e)
        {
            string SoundName = Convert.ToString(((System.Windows.FrameworkElement)sender).Tag);
            if (SoundName != null)
            {
                BibleSoundMediaUrl = SoundName;
            }
            else
            {
                BibleSoundMediaUrl = EntityConfig.MediaUriSounds + @"DictionaryEntriesHLLUSA001-SOUND00B_Sounds532018102038.wav";//"C:\Users\mobiweb\AppData\Local\HebrewLanguageLearning_Users\Media\Sounds\DictionaryEntriesHLLUSA001-SOUND00B_Sounds532018102038.wav"//@"DictionaryEntriesHLLUSA001-SOUND00B_Sounds532018102038.wav";//"ELL_PART_5_768k.wmv";
            }
            MediaElementObject = new MediaElement()
            {
                LoadedBehavior = MediaState.Manual,
            };
            MediaElementObject.Source = new Uri(BibleSoundMediaUrl);// "ELL_PART_5_768k.wmv");  //ELL_PART_5_768k.wmv
            MediaElementObject.Play();

        }


    }


}

