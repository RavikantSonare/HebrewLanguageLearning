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
using System.Windows.Media;

namespace HebrewLanguageLearning_Users.ViewModels.BibleLearning
{
    public class BibleLearningViewModel : Conductor<object>
    {
        BibleLearningController _ObjBC = new BibleLearningController();
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
        public int SelectedIndex {
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
        
        protected override void OnActivate()
        {

            base.OnActivate();

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

        
    }
}
