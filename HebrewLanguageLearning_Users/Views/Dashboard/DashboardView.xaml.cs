using HebrewLanguageLearning_Users.CommonHelper;
using HebrewLanguageLearning_Users.Models.DataControllers;
using HebrewLanguageLearning_Users.Models.DataModels;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace HebrewLanguageLearning_Users.Views.Dashboard
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : Page
    {
        public static BibleLearningModel ObjBook = new BibleLearningModel();
        BibleLearningController _ObjBLC = new BibleLearningController();
        DashboardController ObjDC = new DashboardController();
        DashboardModel _objModel = new DashboardModel();
        private int toggleValue = 0;
        public DashboardView()
        {

            InitializeComponent();
            SetControl();
        }

        public void SetControl()
        {
            //Where you left off on December 12,2017
            _objModel = ObjDC.getUserProgress();
            var LeftData = Convert.ToDateTime(_objModel.LeftDate);
            LastLeftDate.Text = "Where you left off on " + LeftData.ToString("MMMM dd, yyyy"); ;
            int _CurrentLevel = (_objModel.completedLesson / 50) + 1;
            CompleteLesson_level_StatusSeter(5, _CurrentLevel, _objModel.completedLesson % 50 * 2);
            RectingleChildControllerAdd_Loaded_Left(_objModel.completedLesson, _objModel.currentScreenStatus);
            // Set Current Page Session
            System.Windows.Application.Current.Properties["CurretPage"] = _objModel.currentScreenStatus;

            // Set Current IsReviewProgress Session
            System.Windows.Application.Current.Properties["IsReviewProgress"] = _objModel.IsReviewProgress;

            // Set Current IsLearnProgress Session
            System.Windows.Application.Current.Properties["IsLearnProgress"] = _objModel.IsLearnProgress;

            // Set Last Topic Data

            getchapterForUserProgressRight(_objModel.currentBookAndchapterId);
        }
        private void CompleteLesson_level_StatusSeter(int CurrentPossion, int _CurrentLevel, int _PercentOfComplete)
        {

            ////////********************/////////////////// Set Level  //////////////****************////////////

            // _CurrentLevel = ;
            switch (_CurrentLevel)
            {
                case 1:
                    imageLevel1.Visibility = Visibility.Visible;

                    break;
                case 2:
                    imageLevelDark2.Visibility = Visibility.Visible;
                    imageLevelLight2.Visibility = Visibility.Collapsed;

                    ribbon1.Visibility = Visibility.Visible;
                    CurrentPossion = CurrentPossion + 90;
                    break;
                case 3:

                    imageLevelDark2.Visibility = Visibility.Visible;
                    imageLevelLight2.Visibility = Visibility.Collapsed;

                    imageLevelDark3.Visibility = Visibility.Visible;
                    imageLevelLight3.Visibility = Visibility.Collapsed;
                    ribbon1.Visibility = Visibility.Visible;
                    ribbon2.Visibility = Visibility.Visible;
                    CurrentPossion = CurrentPossion + 180;
                    break;
                case 4:
                    imageLevelDark2.Visibility = Visibility.Visible;
                    imageLevelLight2.Visibility = Visibility.Collapsed;

                    imageLevelDark3.Visibility = Visibility.Visible;
                    imageLevelLight3.Visibility = Visibility.Collapsed;

                    imageLevelDark4.Visibility = Visibility.Visible;
                    imageLevelLight4.Visibility = Visibility.Collapsed;

                    ribbon1.Visibility = Visibility.Visible;
                    ribbon2.Visibility = Visibility.Visible;
                    ribbon3.Visibility = Visibility.Visible;
                    CurrentPossion = CurrentPossion + 280;
                    break;
                case 5:
                    imageLevelDark2.Visibility = Visibility.Visible;
                    imageLevelLight2.Visibility = Visibility.Collapsed;

                    imageLevelDark3.Visibility = Visibility.Visible;
                    imageLevelLight3.Visibility = Visibility.Collapsed;

                    imageLevelDark4.Visibility = Visibility.Visible;
                    imageLevelLight4.Visibility = Visibility.Collapsed;

                    imageLevelDark5.Visibility = Visibility.Visible;
                    imageLevelLight5.Visibility = Visibility.Collapsed;


                    ribbon1.Visibility = Visibility.Visible;
                    ribbon2.Visibility = Visibility.Visible;
                    ribbon3.Visibility = Visibility.Visible;
                    ribbon4.Visibility = Visibility.Visible;
                    CurrentPossion = CurrentPossion + 360;
                    break;

            }

            Point Point1 = new Point(5, 0);
            Point Point2 = new Point(10, 10);
            Point Point3 = new Point(0, 10);
            var TriangleSet = ImageDrawHelper.createTriangle(Point1, Point2, Point3, "#00D5B6", "#00D5B6");
            Canvas.SetLeft(TriangleSet, CurrentPossion + 60);
            Canvas.SetTop(TriangleSet, -1);

            CompleteLessonStatus.Children.Add(TriangleSet);

            var _border = new Border();
            _border.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#00D5B6");
            _border.HorizontalAlignment = HorizontalAlignment.Center;
            _border.BorderThickness = new Thickness(0, 4, 0, 0);
            _border.Margin = new Thickness(2, -10, 2, 0);
            _border.Width = 120;
            Canvas.SetLeft(_border, CurrentPossion);
            Canvas.SetTop(_border, 15);

            CompleteLessonStatus.Children.Add(_border);
            var _txtBloack = new TextBlock
            {
                Foreground = Brushes.Black,
            };
            _txtBloack.Height = 25;
            _txtBloack.Width = 140;
            _txtBloack.Text = _PercentOfComplete + " % Complete";
            Canvas.SetLeft(_txtBloack, CurrentPossion);
            Canvas.SetTop(_txtBloack, 10);
            CompleteLessonStatus.Children.Add(_txtBloack);


        }

        private void RectingleChildControllerAdd_Loaded_Left(int _completedLesson, int _current_Screen_TopicNo)
        {
            int SetColomnHeight = 0; int SetRowWidht = 0;
            //int _current_Screen_TopicNo = 5;
            string _currentTopicName = "";//Culture
            //int _completedLesson = 35;
            int tooltip = _completedLesson + 3;
            string _currentLessonNo = Convert.ToString(_completedLesson + 1);
            for (int i = 0; i < 50; i++)
            {
                var _txtBloack = new TextBlock
                {
                    TextAlignment = TextAlignment.Center,
                    Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#00D5B6")),
                    Foreground = Brushes.White,
                };
                // Backgroud Color

                if (i > _completedLesson)
                {
                    //if (tooltip == i)
                    //{
                    //    _txtBloack.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9DE8E1"));
                    //  //  var tooltipData = ImageDrawHelper.nextTooltipLocationIndicator(5, "7", "Traditions");
                    //    //Canvas.SetLeft(tooltipData, SetRowWidht - 10);
                    //    //Canvas.SetTop(tooltipData, SetColomnHeight - 50);
                    //    //RectingleController.Children.Add(tooltipData);
                    //    Point Point1 = new Point(0, 0);
                    //    Point Point2 = new Point(7, 6);
                    //    Point Point3 = new Point(14, 0);
                    //    var TriangleSet = ImageDrawHelper.createTriangle(Point1, Point2, Point3, "#9DE8E1", "#9DE8E1");
                    //    Canvas.SetLeft(TriangleSet, SetRowWidht + 50);
                    //    Canvas.SetTop(TriangleSet, SetColomnHeight - 22);
                    //    RectingleController.Children.Add(TriangleSet);
                    //}
                    //else
                    //{
                        _txtBloack.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#C0C0C0"));
                    //}
                }
                else
                {
                    _txtBloack.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#00D5B6"));

                }

                // set Heigh width
                _txtBloack.Width = 32;
                _txtBloack.Height = 20;

                _txtBloack.Name = "Text" + i.ToString();
                if (i % 10 == 0)
                {
                    if (_completedLesson / 10 * 10 == i)
                    {
                        SetColomnHeight = SetColomnHeight + 89; _txtBloack.Text = Convert.ToString(i + 1);
                        SetRowWidht = 0;
                    }
                    else
                    {
                        SetColomnHeight = SetColomnHeight + 39; _txtBloack.Text = Convert.ToString(i + 1);
                        SetRowWidht = 0;
                    }
                }
                else
                {
                    SetRowWidht = SetRowWidht + 39;
                }
                if (i != _completedLesson)
                {
                    Canvas.SetLeft(_txtBloack, SetRowWidht);
                    Canvas.SetTop(_txtBloack, SetColomnHeight);
                    RectingleController.Children.Add(_txtBloack);
                }
                else
                {
                    var CurrentScreenLocator = ImageDrawHelper.currentLocationIndicator(1, _current_Screen_TopicNo);
                    Point Point1 = new Point(-5, 0);
                    Point Point2 = new Point(5, 12);
                    Point Point3 = new Point(15, 0);
                    var TriangleSet = ImageDrawHelper.createTriangle(Point1, Point2, Point3, "#C3C3C3", "#D9DCDF");
                    var CurrentLessonLable = ImageDrawHelper.createCurrentLesson(ref _currentLessonNo, ref _currentTopicName);

                    Canvas.SetLeft(CurrentLessonLable, SetRowWidht);
                    Canvas.SetTop(CurrentLessonLable, SetColomnHeight - 45);

                    Canvas.SetLeft(TriangleSet, _current_Screen_TopicNo * (SetRowWidht + 9.7));
                    Canvas.SetTop(TriangleSet, SetColomnHeight - 25);

                    Canvas.SetLeft(CurrentScreenLocator, SetRowWidht);
                    Canvas.SetTop(CurrentScreenLocator, SetColomnHeight);

                    RectingleController.Children.Add(CurrentLessonLable);
                    RectingleController.Children.Add(TriangleSet);
                    RectingleController.Children.Add(CurrentScreenLocator);

                    SetRowWidht = SetRowWidht + 78;
                }

            }
        }

        private void getchapterForUserProgressRight(string BookAndChapterId)
        {
            string[] tmpBookAndChapterId = BookAndChapterId.Split(',');

            ObjBook = _ObjBLC.ShowBookData(tmpBookAndChapterId[0]);
            var Chepter = ObjBook._chepterElementsList.Where(p => p.ChepterId == tmpBookAndChapterId[1]).ToList();
            string tmpTxt = string.Empty;
            if (Chepter != null)
            {
                var tmpChepter = Chepter.SelectMany(x => x.verseElementList).ToList();
                int i = 0;
                foreach (var itemChecpter in tmpChepter)
                {
                    //tmpTxt = "_"+Convert.ToString(i+" ");
                    foreach (var item in itemChecpter.wElementList)
                    {
                        tmpTxt += item.ElementValue + " ";
                    }
                    BibleTxt.Text += tmpTxt + "\n";
                    tmpTxt = string.Empty;
                    if (i >= 5)
                    {
                        break;
                    }
                    i++;
                }

            }
            
        }

        private void btnAddVocabDecks_Click(object sender, RoutedEventArgs e)
        {
            showhide();
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
        private void _treeView1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem item = sender as TreeViewItem;
            if (item != null)
            {
                if (item.IsExpanded) { item.IsExpanded = false; } else { item.IsExpanded = true; }

                e.Handled = true;
            }
        }
    }
}
