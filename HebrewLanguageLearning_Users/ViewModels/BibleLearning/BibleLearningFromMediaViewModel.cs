using Caliburn.Micro;
using HebrewLanguageLearning_Users.Models.DataControllers;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using HebrewLanguageLearning_Users.Models.DataModels;
using HebrewLanguageLearning_Users.GenericClasses;
using System.Windows.Forms;
using System.Windows.Controls.Primitives;

namespace HebrewLanguageLearning_Users.ViewModels.BibleLearning
{
    // This Is 10 association v2
    /// <summary>
    ///  Current Screen 100 and 101 used for Second Section it is working for the review, learn Sections.
    /// </summary>

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

        // Set Currunt Screen
        static int CurruntScreenNo = 1;

        // Set Current Image
        static VocabularyModel _ObjCurrentImage = new VocabularyModel();

        // for Pop up
        IWindowManager manager = new WindowManager();

        private readonly INavigationService navigationService;
        public BibleLearningFromMediaViewModel(INavigationService navigationService)
        {


            if (navigationService != null)
                this.navigationService = navigationService;

            var ScreenTemp = System.Windows.Application.Current.Properties["CurretPage"];
            if (ScreenTemp != null)
            {
                CurruntScreenNo = Convert.ToInt32(ScreenTemp);
            }
            GetDataFromDataBase();
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
        public string _currentBibleStrongNo;

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
        public string CurrentBibleStrongNo
        {
            get { return _currentBibleStrongNo; }
            set
            {
                _currentBibleStrongNo = value;
                NotifyOfPropertyChange(() => CurrentBibleStrongNo);
            }
        }

        #endregion

        public void GetDataFromDataBase()
        {
            _objModel = ObjDC.getUserProgress();
            switch (CurruntScreenNo)
            {
                case 101:
                    LessonId = Convert.ToString(System.Windows.Application.Current.Properties["LessonId"]);
                    break;
                default:
                    LessonId = Convert.ToString(_objModel.completedLesson + 1);
                    break;
            }
        }

        public void VocabDecksLesson(string strongNo = "")
        {
            GetDataFromDataBase();
            //string LessonId = "3"; //Convert.ToString(System.Windows.Application.Current.Properties["WordName"]);
            try
            {
                int TotalValue;
                Random random = new Random();
                List<VocabularyModel> tmpVM = new List<VocabularyModel>();
                switch (CurruntScreenNo)
                {
                    case 101:
                        VocabularyLesson = _ObjBC.GetVocabDesksLessonData("HLL_VocabDecks_CustomSecond", LessonId);
                        break;
                    default:
                        VocabularyLesson = _ObjBC.GetVocabDesksLessonData("HLL_VocabDecksLesson", LessonId);
                        break;
                }

                var CurrentGroup = VocabularyLesson.SelectMany(p => p.vocabularyModel).ToList();
                TotalValue = CurrentGroup.Count();
                switch (CurruntScreenNo)
                {
                    case 1:
                        var TakeSelectedValueTmp = CurrentGroup.Where(z => z.IsReviewAssociation == false).ToList();
                        SetCounter(TotalValue - TakeSelectedValueTmp.Count(), TotalValue);
                        break;
                    case 6:
                        TakeSelectedValueTmp = CurrentGroup.Where(z => z.IsReviewAssociationGrammar == false).ToList();
                        SetCounter(TotalValue - TakeSelectedValueTmp.Count(), TotalValue);
                        break;
                    case 101:
                        TakeSelectedValueTmp = CurrentGroup.Where(z => z.IsReviewAssociation == false).ToList();
                        SetCounter(TotalValue - TakeSelectedValueTmp.Count(), TotalValue);
                        break;
                    default:
                        TakeSelectedValueTmp = CurrentGroup.Where(z => z.IsReviewAssociation == false).ToList();
                        SetCounter(TotalValue - TakeSelectedValueTmp.Count(), TotalValue);
                        break;
                }
                // var TakeData = CurrentGroup.Where(z => z.IsReviewAssociation == false).ToList();


                PnlWordChoiceList = new List<VocabularyModel>(CurrentGroup);
                PnlWordChoiceList.ForEach(z =>
                {
                    // for english language set 2
                    var Data = SetWord(z.LessonDecks); z.LessonDecks = Data.Length == 3 ? Data[2] : Data[0]; z.Description = _getImageUrl(z.StrongNo);
                    switch (CurruntScreenNo)
                    {
                        case 1:
                            if (z.IsReviewAssociation == true)
                            { z.DictionaryEntriesId = "#909090"; }
                            else { z.DictionaryEntriesId = "#eaeaea"; }
                            break;
                        case 6:
                            if (z.IsReviewAssociationGrammar == true)
                            { z.DictionaryEntriesId = "#909090"; }
                            else { z.DictionaryEntriesId = "#eaeaea"; }
                            break;
                        case 101:
                            if (z.IsReviewAssociation == true)
                            { z.DictionaryEntriesId = "#909090"; }
                            else { z.DictionaryEntriesId = "#eaeaea"; }
                            break;
                        default:
                            if (z.IsReviewAssociation == true)
                            { z.DictionaryEntriesId = "#909090"; }
                            else { z.DictionaryEntriesId = "#eaeaea"; }
                            break;
                    }

                });
                // set Rendom Logic
                if (PnlWordChoiceList.Count > 0)
                {
                    switch (CurruntScreenNo)
                    {
                        case 1:
                            _ObjCurrentImage = PnlWordChoiceList.Where(z => z.IsReviewAssociation == false).FirstOrDefault();
                            break;
                        case 6:
                            _ObjCurrentImage = PnlWordChoiceList.Where(z => z.IsReviewAssociationGrammar == false).FirstOrDefault();
                            break;
                        default:
                            _ObjCurrentImage = PnlWordChoiceList.Where(z => z.IsReviewAssociation == false).FirstOrDefault();
                            break;
                    }

                    if (!string.IsNullOrEmpty(strongNo))
                    {
                        _ObjCurrentImage = PnlWordChoiceList.Where(z => z.StrongNo == strongNo).FirstOrDefault();
                    }
                    if (_ObjCurrentImage != null)
                        BibleTxtHebrew = _ObjCurrentImage.LessonDecks;
                    if (_ObjCurrentImage != null)
                        SetImage(_ObjCurrentImage.StrongNo);
                }
                else
                {
                    BibleTxtMediaUrl = "";
                }

            }
            catch(Exception ex) { }
            finally { }

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

        public void SetDataFromDataBase(string completed_Screen_Status)
        {
            switch (CurruntScreenNo)
            {
                case 101:
                    ObjDC.SetSecondUserProgressScreen(completed_Screen_Status, false);
                    break;
                default:
                    ObjDC.SetUserProgressScreen(completed_Screen_Status);
                    break;
            }
        }

        public void SetCounter(int value, int Totalvalue)
        {
            ReviewCounter = value + " out of " + Totalvalue;
            string completed_Screen_Status = "2";
            if (value != 0 && value == Totalvalue)
            {
                switch (CurruntScreenNo)
                {
                    case 6:
                        completed_Screen_Status = "7";
                        break;
                    case 101:
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
        public void SetImage(string strongNo, bool isNext = false)
        {
            CurrentBibleStrongNo = strongNo;
            fileData();
            var DicData = _dictionaryModellist.FirstOrDefault(x => x.DicStrongNo == strongNo);
            BibleTxtHebrew = DicData.DicHebrew;

            if (isNext)
            {
                DicData = GetNext(_dictionaryModellist, DicData); //_dictionaryModellist.next(x => x.DicStrongNo == strongNo);
                CurrentBibleStrongNo = DicData.DicStrongNo;
            }

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
            PlaySound(BibleSoundMediaUrl);
        }
        public static T GetNext<T>(IList<T> collection, T value)
        {
            int nextIndex = collection.IndexOf(value) + 1;
            if (nextIndex < collection.Count)
            {
                return collection[nextIndex];
            }
            else
            {
                return value; //Or throw an exception
            }
        }
        public void MouseDown_ImageClick(object sender, MouseEventArgs e)
        {

            string StrongNo = Convert.ToString(((System.Windows.FrameworkElement)sender).Tag);
            string ImageUrl = Convert.ToString(((System.Windows.FrameworkElement)sender).Uid);

            // _ObjBC.UpdateReviewData("HLL_VocabDecksIsReviewAssociation", StrongNo);


            UpdateData(StrongNo);
            SetImage(StrongNo);


            VocabDecksLesson(StrongNo);

        }
        void UpdateData(string StrongNo)
        {
            switch (CurruntScreenNo)
            {
                case 1:
                    _ObjBC.UpdateReviewData("HLL_VocabDecksIsReviewAssociation", StrongNo);

                    break;
                case 6:
                    _ObjBC.UpdateReviewData("HLL_ProgressOfUserIsReviewAssociationGrammar", StrongNo);
                    break;
                case 101:
                    _ObjBC.UpdateReviewDataByLesson("HLL_VocabDecksLearnIsReviewAssociation", LessonId, StrongNo);
                    break;

            }
        }

        public void MouseDown_LeftArrowImageClick(object sender, MouseEventArgs e)
        {
            // PlaySound(BibleSoundMediaUrl);
            UpdateData(CurrentBibleStrongNo);
            SetImage(CurrentBibleStrongNo, true);
            VocabDecksLesson();
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
            PlaySound(BibleSoundMediaUrl);

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

