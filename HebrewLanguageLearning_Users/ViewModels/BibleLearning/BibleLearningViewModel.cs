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
        BibleLearningController _ObjBC = new BibleLearningController();
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

        public string _bibleTxt;
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

        public string BibleTxt
        {
            get { return _bibleTxt; }
            set
            {
                _bibleTxt = value;
                NotifyOfPropertyChange(() => BibleTxt);
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


        protected override void OnActivate()
        {

            base.OnActivate();
            _dictionaryModellist = _ObjSC.getDataFromLocalFile();

            //MessageBox.Show("Deshboard");//This is for testing - currently doesn't display
        }

        public void DDLBookSelect(SelectListItem _ObjList)
        {

            string _currentfileName = _ObjList.Value;
            if (!string.IsNullOrEmpty(_currentfileName))
            {
                ObjBook = _ObjBC.ShowBookData(_currentfileName);
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
                SetRightSideData(Convert.ToString(strongNo));
            }

        }
        void SetRightSideData(string strongNo)
        {
            BibileTextList _obj = GetDescription(strongNo);
            BibleTxt = _obj.proBibleTxt;
         
            BibleTxtMediaUrl = _obj.proMediaURl;
            DescriptionTxt = _obj.proDescriptionTxt;
            ExampleTxt = _obj.proExampleTxt;
            SemanticDomainTxt = _obj.proSemanticDomainTxt;
            DictonaryReference = _obj.proDictonaryReferenceTxt;

        }
        BibileTextList GetDescription(string strongNo)
        {
            var DicData = _dictionaryModellist.FirstOrDefault(x => x.DicStrongNo == strongNo);
            BibileTextList _obj = new BibileTextList();
            if (DicData != null)
            {
                //_obj.proBibleTxt = DicData.DicEnglish;

                _obj.proBibleTxt = DicData.DicHebrew;
                _obj.proMediaURl += DicData.ListOfPictures.LastOrDefault();
                _obj.proDescriptionTxt = DicData.ListOfDefinition.FirstOrDefault();
                _obj.proExampleTxt = DicData.ListOfExample.FirstOrDefault();
                _obj.proSemanticDomainTxt = DicData.ListOfSemanticDomain.FirstOrDefault();
                _obj.proDictonaryReferenceTxt = string.Join(",", DicData.ListOfDictionaryReference.ToArray()); //  .Select(x => x.Split(',')[0]).ToList();

            }
            else
            {
                var tempData = _dictionaryModellist.FirstOrDefault();
                _obj.proBibleTxt = tempData.DicHebrew;
                _obj.proMediaURl += tempData.ListOfPictures.LastOrDefault();
                _obj.proDescriptionTxt = tempData.ListOfDefinition.FirstOrDefault();
                _obj.proExampleTxt = tempData.ListOfExample.FirstOrDefault();
                _obj.proSemanticDomainTxt = tempData.ListOfSemanticDomain.FirstOrDefault();
                _obj.proDictonaryReferenceTxt = tempData.ListOfDictionaryReference.FirstOrDefault();
            }
            return _obj;
        }
    }
    class BibileTextList
    {
        public string proBibleTxt { get; set; }
        public string proMediaURl { get; set; } = EntityConfig.MediaUri;
        public string proDescriptionTxt { get; set; }
        public string proExampleTxt { get; set; }
        public string proSemanticDomainTxt { get; set; }
        public string proDictonaryReferenceTxt { get; set; }

    }

}
