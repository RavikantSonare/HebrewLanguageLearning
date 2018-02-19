using HebrewLanguageLearning_Users.CommonHelper;
using HebrewLanguageLearning_Users.GenericClasses;
using HebrewLanguageLearning_Users.Models.DataControllers;
using HebrewLanguageLearning_Users.Models.DataModels;
using HebrewLanguageLearning_Users.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
namespace HebrewLanguageLearning_Users.Views.BibleLearning
{
    /// <summary>
    /// Interaction logic for BibleLearningView.xaml
    /// </summary>
    public partial class BibleLearningView : Page
    {
        BibleLearningController _ObjBC = new BibleLearningController();
        public static BibleLearningModel ObjBook = new BibleLearningModel();
        private int toggleValue=0;

        public BibleLearningView()
        {
            InitializeComponent();
            //image();
        }

        private void _ItemBook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectListItem _obj = (sender as ComboBox).SelectedItem as SelectListItem;
            var _currentfileName = _obj.Value;
            if (!string.IsNullOrEmpty(_currentfileName) && _currentfileName != "0")
            {
                ObjBook = _ObjBC.ShowBookData(_currentfileName);
                SelectChapter();
                SetTextBoxValue();


            }
        }
        private void _ItemBookVerse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int CurrentIndex = _ItemBookChapter.SelectedIndex, totalIndex = BibleTxtLesson.Items.Count;
            int CurrentIndexVers = _ItemBookVerse.SelectedIndex;
            double newValue = 0; if (CurrentIndex > 0 && CurrentIndexVers > 0)
            {
                var newCurrentValue = CurrentIndex + "." + CurrentIndexVers;
                if (newCurrentValue != null)
                {
                    newValue = Convert.ToDouble(newCurrentValue);
                    SetScrollingPossition(newValue, totalIndex);
                }
            }
            
        }
        private void _ItemChapter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectListItem _obj = (sender as ComboBox).SelectedItem as SelectListItem;
            if (_obj != null)
            {
                var _currentfileName = _obj.Value;
                SelectVerse(_currentfileName);
            }

            int CurrentIndex = _ItemBookChapter.SelectedIndex, totalIndex = BibleTxtLesson.Items.Count;
            //  int CurrentIndexVerse = _ItemBookVerse.SelectedIndex;

            if (CurrentIndex != 0)
            {
                SetScrollingPossition(CurrentIndex, totalIndex);
            }
        }
        public void SelectChapter()
        {
            _ItemBookChapter.ItemsSource = ObjBook._ChapterList.ToList();
            _ItemBookChapter.SelectedIndex = 0;
        }

        public void SelectVerse(string firstValue)
        {
            _ItemBookVerse.ItemsSource = ObjBook._VerseList.Where(z => z.Value == firstValue).ToList();
            _ItemBookVerse.SelectedIndex = 0;

        }
        public void SetTextBoxValue()
        {
            // BibleTxtLesson.ItemsSource = ObjBook._bookElementsList.ToList();
            BibleTxtLesson.ItemsSource = ObjBook._chepterElementsList.ToList();
            
        }
        public void SetScrollingPossition(double Set_Possion, int totalIndex)
        {
            Decorator border = VisualTreeHelper.GetChild(BibleTxtLesson, 0) as Decorator;
            if (border != null)
            {
                // Get scrollviewer
                ScrollViewer scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer != null && totalIndex > 0)
                {
                    var t = scrollViewer;
                    // center the Scroll Viewer...
                    scrollViewer.ScrollToVerticalOffset(0);
                    double center = scrollViewer.ScrollableHeight / totalIndex * Set_Possion;
                    scrollViewer.ScrollToVerticalOffset(Set_Possion);
                }

            }
            var txtCnt = BibleTxtLesson.Items.Count;
        }
        
        private void BibleTxtLesson_ScrollChanged(object sender, RoutedEventArgs e)
        {
            Decorator border = VisualTreeHelper.GetChild(BibleTxtLesson, 0) as Decorator; ScrollViewer scrollViewer = border.Child as ScrollViewer;
            _ItemBookChapter.SelectedIndex = Convert.ToInt32(scrollViewer.VerticalOffset);
        }

        private void btnAddVocabDecks_Click(object sender, RoutedEventArgs e)
        {
            showhide();

            //RightPanelPropertyChanged ObjRP = new RightPanelPropertyChanged();
            //ObjRP.onPropertyChanged("btnAddVocabDecks");

            //ShellViewModel sm = new ShellViewModel();

            //ShellView sv = new ShellView();

            // sv.showhide();
        }
      
        public void showhide()
        {
            if (toggleValue == 0)
            {

                toggleValue = 1;
                ShowHideMenu("sbShowRightMenu", VocabDecksPanel);
            }
            else
            {

                toggleValue = 0;
                ShowHideMenu("sbHideRightMenu", VocabDecksPanel);
            }
        }


        private void ShowHideMenu(string Storyboard, StackPanel pnl)
        {
            Storyboard sb = Resources[Storyboard] as Storyboard;
            sb.Begin(pnl);
            
        }

       
    }
}
