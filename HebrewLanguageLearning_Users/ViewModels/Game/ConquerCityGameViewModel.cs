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

    public class ConquerCityGameViewModel : Conductor<object>
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
        public ConquerCityGameViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            GetDataFromDataBase();
            // Convert.ToString(System.Windows.Application.Current.Properties["WordName"]);
            // string LessonId = Convert.ToString(System.Windows.Application.Current.Properties["WordLessonId"]);
            // if (!string.IsNullOrEmpty(LessonId)) { VocabDecksLesson(); };
        }
        public void GetDataFromDataBase()
        {
            _objModel = ObjDC.getUserProgress();
            LessonId = Convert.ToString(_objModel.completedLesson + 1);
            VocabDecksLesson();
        }
        public void SetDataFromDataBase(string completedLesson, string completed_Screen_Status)
        {
            var Data = ObjDC.SetUserProgress(completedLesson, completed_Screen_Status);
            LessonId = Convert.ToString(_objModel.completedLesson + 1);
        }
        protected override void OnActivate()
        {
            base.OnActivate();
            fileData();
        }
        #region Property
        public string _reviewCounter;
        public string _reviewCounterRed = "0";
        public string _reviewCounterGreen = "0";
        public string _bibleTxtVocabdocks;
        public string _rightWorldTextBlock;
        public string _bibleTxtHebrew;
        public string _bibleTxtEnglish;
        public List<VocabularyModel> _pnlWordChoiceList;
        public string _bibleTxtMediaUrl;
        public List<ImagesModelList> _mapImageList;
        public string ReviewCounter
        {
            get { return _reviewCounter; }
            set
            {
                _reviewCounter = value;
                NotifyOfPropertyChange(() => ReviewCounter);
            }
        }
        public string ReviewCounterRed
        {
            get { return _reviewCounterRed; }
            set
            {
                _reviewCounterRed = value;
                NotifyOfPropertyChange(() => ReviewCounterRed);
            }
        }
        public string ReviewCounterGreen
        {
            get { return _reviewCounterGreen; }
            set
            {
                _reviewCounterGreen = value;
                NotifyOfPropertyChange(() => ReviewCounterGreen);
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
        public string RightWorldTextBlock
        {
            get { return _rightWorldTextBlock; }
            set
            {
                _rightWorldTextBlock = value;
                NotifyOfPropertyChange(() => RightWorldTextBlock);
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
        public List<ImagesModelList> MapImageList
        {
            get { return _mapImageList; }
            set
            {
                _mapImageList = value;
                NotifyOfPropertyChange(() => MapImageList);
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
                CurrentGroup = CurrentGroup.Where(z => z.IsReviewGameB == false).ToList();
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

                // ImagesListBind(true, "1", "2");

            }
            catch (Exception ex) { }
            finally { }
            // SetWord(CurrentGroup);
            // SetWord(s.LessonDecks); PnlWordChoiceList
        }
        //string data(int i)
        //{
        //    var rand = new Random();
        //    string[] str= { "green.png", "red.png", "red-gray.png" };
        //    var pic = str[rand.Next(str.Count())];
        //    return pic;
        //}
        static List<ImagesModelList> _dicList = new List<ImagesModelList>()
        {
                 new ImagesModelList { ImgHeight = "100", ImgWidth = "100", ImgTop = "0", ImgLeft = "0" } ,
                 new ImagesModelList { ImgHeight = "80", ImgWidth = "80", ImgTop = "42", ImgLeft = "177" } ,
                 new ImagesModelList { ImgHeight = "50", ImgWidth = "50", ImgTop = "65", ImgLeft = "217" } ,
                 new ImagesModelList { ImgHeight = "87", ImgWidth = "82", ImgTop = "78", ImgLeft = "152" } ,
                 new ImagesModelList { ImgHeight = "80", ImgWidth = "74", ImgTop = "80", ImgLeft = "210" } ,
                 new ImagesModelList { ImgHeight = "80", ImgWidth = "68", ImgTop = "111", ImgLeft = "195" } ,
                 new ImagesModelList { ImgHeight = "84", ImgWidth = "82", ImgTop = "130", ImgLeft = "140" } ,
                 new ImagesModelList { ImgHeight = "65", ImgWidth = "65", ImgTop = "157", ImgLeft = "196" } ,
                 new ImagesModelList { ImgHeight = "40", ImgWidth = "40", ImgTop = "186", ImgLeft = "176" } ,
                 new ImagesModelList { ImgHeight = "40", ImgWidth = "54", ImgTop = "205", ImgLeft = "192" } ,
                 new ImagesModelList { ImgHeight = "61", ImgWidth = "48", ImgTop = "186", ImgLeft = "140" } ,
                 new ImagesModelList { ImgHeight = "75", ImgWidth = "109", ImgTop = "212", ImgLeft = "134" } ,
                 new ImagesModelList { ImgHeight = "60", ImgWidth = "100", ImgTop = "264", ImgLeft = "97" } ,
                 new ImagesModelList { ImgHeight = "100", ImgWidth = "74", ImgTop = "243", ImgLeft = "170" } ,
              
                 new ImagesModelList { ImgHeight = "100", ImgWidth = "100", ImgTop = "100", ImgLeft = "210" } ,
                 new ImagesModelList { ImgHeight = "100", ImgWidth = "100", ImgTop = "125", ImgLeft = "210" } ,
                 new ImagesModelList { ImgHeight = "100", ImgWidth = "100", ImgTop = "150", ImgLeft = "210" } ,
                 new ImagesModelList { ImgHeight = "100", ImgWidth = "100", ImgTop = "175", ImgLeft = "210" } ,
                 new ImagesModelList { ImgHeight = "100", ImgWidth = "100", ImgTop = "200", ImgLeft = "210" } ,
                 new ImagesModelList { ImgHeight = "100", ImgWidth = "100", ImgTop = "225", ImgLeft = "210" } ,
                 new ImagesModelList { ImgHeight = "100", ImgWidth = "100", ImgTop = "250", ImgLeft = "210" } ,
                 new ImagesModelList { ImgHeight = "100", ImgWidth = "100", ImgTop = "275", ImgLeft = "210" } ,
                 new ImagesModelList { ImgHeight = "100", ImgWidth = "100", ImgTop = "300", ImgLeft = "210" } ,
                 new ImagesModelList { ImgHeight = "100", ImgWidth = "100", ImgTop = "325", ImgLeft = "210" } ,
                 new ImagesModelList { ImgHeight = "100", ImgWidth = "100", ImgTop = "350", ImgLeft = "210" } ,
                 new ImagesModelList { ImgHeight = "100", ImgWidth = "100", ImgTop = "375", ImgLeft = "210" } ,
            };
       
        //static Dictionary<ImagesModelList> _dicTopLeft = new Dictionary<ImagesModelList>() { { "10", "10" },
        //         { "50", "150" },  { "150", "100" },  { "175", "100" },  { "200", "100" },  { "225", "100" },
        //         { "250", "100" }, { "275", "100" }, { "300", "100" }, { "325", "100" }, { "350", "100" },
        //         { "126", "200" }, { "151", "200" }, { "176", "200" }, { "201", "200" }, { "226", "200" },
        //         { "251", "200" }, { "276", "200" }, { "301", "200" }, { "326", "200" }, { "351", "200" }};

        private void ImagesListBind(bool ImageStatus, string ReviewCounterGreen, string ReviewCounterRed)
        {
            try
            {
                var _mapImageListTmp = new List<ImagesModelList>();
                if (MapImageList != null)
                {
                    _mapImageListTmp = MapImageList;
                }

                int counter = Convert.ToInt32(ReviewCounterGreen) + Convert.ToInt32(ReviewCounterRed);
             
                var ImagesData = ImageStatus ? "MapGreen_" + counter + ".png" : "Mapred_" + counter + ".png";
                _mapImageListTmp.Add(new ImagesModelList { ImgSource = EntityConfig.LocalfileRootPath + "MapImages\\" + ImagesData, ImgHeight = _dicList.ElementAt(counter).ImgHeight , ImgWidth = _dicList.ElementAt(counter).ImgWidth, ImgTop = _dicList.ElementAt(counter).ImgTop, ImgLeft = _dicList.ElementAt(counter).ImgLeft });
              
                MapImageList = new List<ImagesModelList>(_mapImageListTmp);
              
            }
            catch (Exception ex)
            {

            }
        }

        private string[] SetWord(string currentGroup)
        {
            string[] tempArray = currentGroup.Split(',');
            return tempArray;
        }
        string AddNewValue(string OldValue)
        {
            return Convert.ToString(Convert.ToInt32(OldValue) + 1);
        }
        void SetCounter(int value, int Totalvalue)
        {
            ReviewCounter = Totalvalue - value + "  cities left! ";
            if (value != 0 && value == Totalvalue)
            {
                SetScreenNoInDataBase("6");
                //SetDataFromDataBase(LessonId, completed_Screen_Status); 
            }
        }

        private void SetScreenNoInDataBase(string screenNo)
        {
            ObjDC.SetUserProgressScreen(screenNo);
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

        /// <summary>
        /// Active Know
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        public void MouseDown_submit(object sender, MouseEventArgs e)
        {

            bool imageStatus = false;
            if (!string.IsNullOrEmpty(RightWorldTextBlock) && RightWorldTextBlock.ToUpper().Trim() == _ObjCurrentImage.LessonDecks.ToUpper().Trim())
            {
                _ObjBC.UpdateReviewData("HLL_VocabDecksIsReviewGameB", _ObjCurrentImage.StrongNo);
                ReviewCounterGreen = AddNewValue(ReviewCounterGreen);
                imageStatus = true;
            }
            else
            {
                ReviewCounterRed = AddNewValue(ReviewCounterRed);
            }
            RightWorldTextBlock = "";
            VocabDecksLesson();
            ImagesListBind(imageStatus, ReviewCounterGreen, ReviewCounterRed);
        }
        // Goto Previes Pages
        public void MouseDown_RightPanel(object sender, MouseEventArgs e)
        {
            navigationService.NavigateToViewModel(typeof(BibleLearning.BibleLearningViewModel));
        }

    }
}




//_mapImageListTmp.Add(new ImagesModelList { ImgSource = EntityConfig.LocalfileRootPath + "MapImages\\" + "MapGreen_1.png", ImgHeight = "50", ImgWidth = "50", ImgTop = "100", ImgLeft = "250" });
//_mapImageListTmp.Add(new ImagesModelList { ImgSource = EntityConfig.LocalfileRootPath + "MapImages\\" + "MapGreen_2.png", ImgHeight = "50", ImgWidth = "50", ImgTop = "150", ImgLeft = "300" });

//if (_mapImageList != null)
//{
//    _mapImageListTmp.Add(new ImagesModelList { ImgSource = EntityConfig.LocalfileRootPath + "MapImages\\" + "Mapred_1.png", ImgHeight = "50", ImgWidth = "50", ImgTop = "230", ImgLeft = "250" });
//    _mapImageListTmp.Add(new ImagesModelList { ImgSource = EntityConfig.LocalfileRootPath + "MapImages\\" + "Mapred_2.png", ImgHeight = "50", ImgWidth = "50", ImgTop = "250", ImgLeft = "300" });
//}
//_mapImageListTmp.Add(new ImagesModelList { ImgSource = EntityConfig.LocalfileRootPath + "MapImages\\" + "Mapred_3.png", ImgHeight = "50", ImgWidth = "50", ImgTop = "200", ImgLeft = "300" });
