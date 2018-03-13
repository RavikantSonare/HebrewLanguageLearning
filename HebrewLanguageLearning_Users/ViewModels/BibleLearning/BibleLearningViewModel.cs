using Caliburn.Micro;
using HebrewLanguageLearning_Users.GenericClasses;
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
using System.Windows.Forms;
using System.Windows.Media;

namespace HebrewLanguageLearning_Users.ViewModels.BibleLearning
{
    public class BibleLearningViewModel : Conductor<object>
    {
        BibleLearningController _ObjBLC = new BibleLearningController();
        SettingController _ObjSC = new SettingController();
        public static BibleLearningModel ObjBook = new BibleLearningModel();
        public List<bookElements> _BibleTxtLesson;
        public List<bookElements> BibleTxtLesson
        {
            get { return _BibleTxtLesson; }
            set
            {
                _BibleTxtLesson = value;
                NotifyOfPropertyChange(() => BibleTxtLesson);
            }
        }


        // Getting Data From Database
        DashboardController ObjDC = new DashboardController();
        DashboardModel _objModel = new DashboardModel();

        // Set Lesson Id
        static string LessonId = string.Empty;

        //for Right Panel and overleap screen.

        public string _bibleTxtdocks;
        public string _bibleTxtVocabdocks;
        public string _strongNo;


        /// <summary>
        /// 
        /// </summary>

        public string _bibleTxtHebrew;
        public string _bibleTxtEnglish;
        public string _bibleTxtMediaUrl;
        public string _descriptionTxt;
        public string _semanticDomainTxt;
        public string _exampleTxt;
        public string _dictonaryReference;


        public List<SelectListItem> _ItemBook = EntityConfig._booksTitleList;
        public List<SelectListItem> ItemBook
        {
            get { return _ItemBook; }
            set
            {
                _ItemBook = value;
                NotifyOfPropertyChange(() => ItemBook);
            }
        }
        public List<SelectListItem> _ItemBookChapter = new List<SelectListItem>();
        public List<SelectListItem> ItemBookChapter
        {
            get { return _ItemBookChapter; }
            set
            {
                _ItemBookChapter = value;
                NotifyOfPropertyChange(() => ItemBookChapter);

            }
        }

        private BindableCollection<SelectListItem> _ItemBookChapterSelected = new BindableCollection<SelectListItem>();
        public BindableCollection<SelectListItem> ItemBookChapterSelected
        {
            get { return _ItemBookChapterSelected; }
            set
            {
                _ItemBookChapterSelected = value;

                NotifyOfPropertyChange(() => ItemBookChapterSelected);
            }
        }

        private int _SelectedIndex;
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                _SelectedIndex = value;
                NotifyOfPropertyChange(() => SelectedIndex);
            }
        }
        public List<SelectListItem> _ItemBookVerse;
        public List<SelectListItem> ItemBookVerse
        {
            get { return _ItemBookVerse; }
            set
            {
                _ItemBookVerse = value;
                NotifyOfPropertyChange(() => ItemBookVerse);
            }
        }

        public string BibleTxtdocks
        {
            get { return _bibleTxtdocks; }
            set
            {
                _bibleTxtdocks = value;
                NotifyOfPropertyChange(() => BibleTxtdocks);
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

        public string StrongNo
        {
            get { return _strongNo; }
            set
            {
                _strongNo = value;
                NotifyOfPropertyChange(() => StrongNo);
            }
        }

        public string BibleTxtMediaUrl
        {
            get { return _bibleTxtMediaUrl; }
            set
            {
                _bibleTxtMediaUrl = value;
                NotifyOfPropertyChange(() => BibleTxtMediaUrl);
            }
        }

        public string DescriptionTxt
        {
            get { return _descriptionTxt; }
            set
            {
                _descriptionTxt = value;
                NotifyOfPropertyChange(() => DescriptionTxt);
            }
        }
        public string SemanticDomainTxt
        {
            get { return _semanticDomainTxt; }
            set
            {
                _semanticDomainTxt = value;
                NotifyOfPropertyChange(() => SemanticDomainTxt);
            }
        }
        public string ExampleTxt
        {
            get { return _exampleTxt; }
            set
            {
                _exampleTxt = value;
                NotifyOfPropertyChange(() => ExampleTxt);
            }
        }
        public string DictonaryReference
        {
            get { return _dictonaryReference; }
            set
            {
                _dictonaryReference = value;
                NotifyOfPropertyChange(() => DictonaryReference);
            }
        }

        static List<DictionaryModel> _dictionaryModellist = new List<DictionaryModel>();

        private readonly INavigationService navigationService;
        public BibleLearningViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        protected override void OnActivate()
        {

            base.OnActivate();
            _dictionaryModellist = _ObjSC.getDataFromLocalFile();
            VocabDecksMenu();

            //MessageBox.Show("Deshboard");//This is for testing - currently doesn't display
        }

        public void DDLBookSelect(SelectListItem _ObjList)
        {

            string _currentfileName = _ObjList.Value;
            if (!string.IsNullOrEmpty(_currentfileName))
            {
                ObjBook = _ObjBLC.ShowBookData(_currentfileName);
                //  ItemBookChapter = ObjBook._ChapterList;
                //  SelectVerse(ObjBook._ChapterList.FirstOrDefault().Value);

                //  var firstValueOfElement = ObjBook._ChapterList.FirstOrDefault().Value;
                //  ObjBook._bookElementsList.ForEach(x => BibleTxtLesson+= "<LineBreak/>" + x.ElementValue); //Select(z => z.ElementValue);
                SelectChapter();
                BibleTxtLesson = ObjBook._bookElementsList.ToList();

            }
        }

        public void ItemBookChapterSelectedChange(SelectListItem _ObjList)
        {
            if (_ObjList != null)
            {
                string _currentfileName = _ObjList.Value;
                SelectVerse(_currentfileName);
            }
        }
        public void SelectChapter()
        {
            ItemBookChapter = ObjBook._ChapterList.ToList();
            SelectedIndex = 1;
        }
        public void ItemBookVerseSelectedChange(SelectListItem _ObjList)
        {
            string _currentfileName = _ObjList.Value;
            SelectVerse(_currentfileName);
        }
        public void SelectVerse(string firstValue)
        {
            ItemBookVerse = ObjBook._VerseList.Where(z => z.Value == firstValue).ToList();

        }
        public void MouseDown_WordList(object sender, MouseEventArgs e)
        {
            if (sender != null)
            {
                var strongNo = ((System.Windows.FrameworkElement)sender).Tag;
                var BookAndChapterId = ((System.Windows.FrameworkElement)sender).ToolTip;
                SetRightSideData(Convert.ToString(strongNo));
                setchapterForUserProgress(Convert.ToString(BookAndChapterId));
            }

        }
        void SetRightSideData(string strongNo)
        {
            StrongNo = strongNo;
            BibileTextList _obj = GetDescription(strongNo);
            BibleTxtHebrew = _obj.proBibleTxtHebrew;
            BibleTxtEnglish = _obj.proBibleTxtEnglish;
            BibleTxtMediaUrl = EntityConfig.MediaUriPictures + _obj.proMediaURl;
            DescriptionTxt = _obj.proDescriptionTxt;
            ExampleTxt = _obj.proExampleTxt;
            SemanticDomainTxt = _obj.proSemanticDomainTxt;
            DictonaryReference = _obj.proDictonaryReferenceTxt;

            BibleTxtdocks = "Choose a deck to add  " + _obj.proBibleTxtHebrew + " ->";

        }
        BibileTextList GetDescription(string strongNo)
        {
            var DicData = _dictionaryModellist.FirstOrDefault(x => x.DicStrongNo == strongNo);
            BibileTextList _obj = new BibileTextList();
            if (DicData != null)
            {
                //_obj.proBibleTxt = DicData.DicEnglish;

                _obj.proBibleTxtHebrew = DicData.DicHebrew;
                _obj.proBibleTxtEnglish = DicData.DicEnglish;
                _obj.proMediaURl += DicData.ListOfPictures.LastOrDefault();
                _obj.proDescriptionTxt = DicData.ListOfDefinition.FirstOrDefault();
                _obj.proExampleTxt = DicData.ListOfExample.FirstOrDefault();
                _obj.proSemanticDomainTxt = DicData.ListOfSemanticDomain.FirstOrDefault();
                _obj.proDictonaryReferenceTxt = string.Join(",", DicData.ListOfDictionaryReference.ToArray()); //  .Select(x => x.Split(',')[0]).ToList();

            }
            else
            {
                var tempData = _dictionaryModellist.FirstOrDefault();
                _obj.proBibleTxtHebrew = tempData.DicHebrew;
                _obj.proBibleTxtEnglish = tempData.DicEnglish;
                _obj.proMediaURl += tempData.ListOfPictures.LastOrDefault();
                _obj.proDescriptionTxt = tempData.ListOfDefinition.FirstOrDefault();
                _obj.proExampleTxt = tempData.ListOfExample.FirstOrDefault();
                _obj.proSemanticDomainTxt = tempData.ListOfSemanticDomain.FirstOrDefault();
                _obj.proDictonaryReferenceTxt = tempData.ListOfDictionaryReference.FirstOrDefault();
            }
            return _obj;
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
            VocabularyLesson = _ObjBLC.GetVocabDesks("HLL_VocabDecks");
            VocabularyLesson_Custom = _ObjBLC.GetVocabDesks("HLL_VocabDecks_Custom");
        }

        public void MouseDown_CustomDecks(object sender, MouseEventArgs e)
        {
            VocabularyModel _obj = new VocabularyModel();
            var ListViewData = String.Empty;
            if (String.IsNullOrEmpty(BibleTxtVocabdocks))
            {
                ListViewData = Convert.ToString(((HeaderedItemsControl)sender).Header);
            }
            if (!String.IsNullOrWhiteSpace(BibleTxtVocabdocks) || !String.IsNullOrWhiteSpace(ListViewData))
            {
                _obj.StrongNo = StrongNo;
                _obj.LessonId = String.IsNullOrEmpty(ListViewData) ? BibleTxtVocabdocks : ListViewData;
                _obj.LessonDecks = StrongNo + ',' + BibleTxtHebrew + ',' + BibleTxtEnglish;
                _obj.IsCustomeDecks = true;
                _obj.ActiveStatus = true;
                _obj.IsActive = true;
                _obj.IsDelete = false;
                _ObjBLC.SetVocabDesks(_obj);
            }
            VocabDecksMenu();
        }

        public void MouseDown_CustomDecksReview(object sender, MouseEventArgs e)
        {

            // navigationService.NavigateToViewModel(typeof(BibleLearningFromMediaWordChoiceViewModel));

            if (String.IsNullOrEmpty(BibleTxtVocabdocks))
            {
                //System.Windows.Application.Current.Properties["WordName"] = Convert.ToString(((System.Windows.FrameworkElement)sender).Tag);
                System.Windows.Application.Current.Properties["CurretRedirection"] = 3;
                var Id = Convert.ToString(((System.Windows.FrameworkElement)sender).Tag);
                if (Id != null)
                {
                    Id = Id.Replace("Lesson ", string.Empty);
                }
                var GetLessonStatus = GetDataFromDataBase(Id);
                if (!GetLessonStatus)
                {
                    navigationService.NavigateToViewModel(typeof(BibleLearningFromMediaViewModel));
                }
                else
                {
                    navigationService.NavigateToViewModel(typeof(BibleLearningFromMediaWordChoiceViewModel));
                }
            }

        }
        public void MouseDown_CustomDecksDelete(object sender, MouseEventArgs e)
        {
            VocabularyModel _obj = new VocabularyModel();
            var VocabDocLessonId = Convert.ToString(((System.Windows.FrameworkElement)sender).Tag);

            if (!String.IsNullOrWhiteSpace(VocabDocLessonId))
            {
                _ObjBLC.DeleteLesson(VocabDocLessonId);
            }
            VocabDecksMenu();
        }

        public void MouseDown_LeftPanel()
        {
            navigationService.NavigateToViewModel(typeof(Dashboard.DashboardViewModel));
        }

        /// <summary>
        /// Get Data From Database for Assocciations Or passive 
        /// </summary>
        /// <param name="CurrentLessionId"></param>
        /// <returns></returns>

        public bool GetDataFromDataBase(string CurrentLessionId)
        {
            var total_Cnt = VocabularyLesson.Where(p => p.LessonId == "Lesson " + CurrentLessionId).FirstOrDefault().vocabularyModel.Count();
            var totalTrue_Cnt = _ObjBLC.getIsReviewAssociationCount(CurrentLessionId);

            LessonId = Convert.ToString(_objModel.completedLesson);

            //if (Convert.ToInt32(CurrentLessionId) <= Convert.ToInt32(LessonId))
            if (total_Cnt == totalTrue_Cnt)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        void setchapterForUserProgress(string BookAndChapterId)
        {
            _ObjBLC.setProgressOfUserChapter(BookAndChapterId);
        }
    }

}
