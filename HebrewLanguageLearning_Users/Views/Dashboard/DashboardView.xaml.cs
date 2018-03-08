﻿using HebrewLanguageLearning_Users.CommonHelper;
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

namespace HebrewLanguageLearning_Users.Views.Dashboard
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : Page
    {
        DashboardController ObjDC = new DashboardController();
        DashboardModel _objModel = new DashboardModel();
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
            int _CurrentLevel = _objModel.completedLesson;
            CompleteLessonStatusSeter(100, _CurrentLevel);
            RectingleChildControllerAdd_Loaded(_objModel.completedLesson);
        }
        private void CompleteLessonStatusSeter(int CurrentPossion, int _CurrentLevel)
        {

            ////////********************/////////////////// Set Level  //////////////****************////////////
            var data = 45 / 50;
            _CurrentLevel = (_CurrentLevel / 50);
            switch (_CurrentLevel)
            {
                case 1:
                    imageLevel1.Visibility = Visibility.Visible;
                    ribbon1.Visibility= Visibility.Visible;
                    break;
                case 2:
                    imageLevel2.Visibility = Visibility.Visible;
                    ribbon2.Visibility = Visibility.Visible;
                    break;
                case 3:
                    imageLevel3.Visibility = Visibility.Visible;
                    ribbon3.Visibility = Visibility.Visible;
                    break;
                case 4:
                    imageLevel4.Visibility = Visibility.Visible;
                    ribbon4.Visibility = Visibility.Visible;
                    break;
                case 5:
                    imageLevel5.Visibility = Visibility.Visible;
                    ribbon5.Visibility = Visibility.Visible;
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
            _txtBloack.Text = "2 % Complete";
            Canvas.SetLeft(_txtBloack, CurrentPossion);
            Canvas.SetTop(_txtBloack, 10);
            CompleteLessonStatus.Children.Add(_txtBloack);


        }

        private void RectingleChildControllerAdd_Loaded(int _completedLesson)
        {
            int SetColomnHeight = 0; int SetRowWidht = 0;
            int _currentTopicNo = 5;
            string _currentTopicName = "Culture";
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
                    if (tooltip == i)
                    {
                        _txtBloack.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9DE8E1"));
                        var tooltipData = ImageDrawHelper.nextTooltipLocationIndicator(5, "7", "Traditions");
                        Canvas.SetLeft(tooltipData, SetRowWidht - 10);
                        Canvas.SetTop(tooltipData, SetColomnHeight - 50);
                        RectingleController.Children.Add(tooltipData);
                        Point Point1 = new Point(0, 0);
                        Point Point2 = new Point(7, 6);
                        Point Point3 = new Point(14, 0);
                        var TriangleSet = ImageDrawHelper.createTriangle(Point1, Point2, Point3, "#9DE8E1", "#9DE8E1");
                        Canvas.SetLeft(TriangleSet, SetRowWidht + 50);
                        Canvas.SetTop(TriangleSet, SetColomnHeight - 22);
                        RectingleController.Children.Add(TriangleSet);
                    }
                    else
                    {
                        _txtBloack.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#C0C0C0"));
                    }
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
                    var CurrentLocator = ImageDrawHelper.currentLocationIndicator(1, _currentTopicNo);
                    Point Point1 = new Point(-5, 0);
                    Point Point2 = new Point(5, 12);
                    Point Point3 = new Point(15, 0);
                    var TriangleSet = ImageDrawHelper.createTriangle(Point1, Point2, Point3, "#C3C3C3", "#D9DCDF");
                    var CurrentLessonLable = ImageDrawHelper.createCurrentLesson(ref _currentLessonNo, ref _currentTopicName);

                    Canvas.SetLeft(CurrentLessonLable, SetRowWidht);
                    Canvas.SetTop(CurrentLessonLable, SetColomnHeight - 45);

                    Canvas.SetLeft(TriangleSet, SetRowWidht + 9 * _currentTopicNo);
                    Canvas.SetTop(TriangleSet, SetColomnHeight - 25);

                    Canvas.SetLeft(CurrentLocator, SetRowWidht);
                    Canvas.SetTop(CurrentLocator, SetColomnHeight);

                    RectingleController.Children.Add(CurrentLessonLable);
                    RectingleController.Children.Add(TriangleSet);
                    RectingleController.Children.Add(CurrentLocator);

                    SetRowWidht = SetRowWidht + 78;
                }

            }
        }

    }
    //public class rectelgleProp
    //{
    //    public string ColorProp { get; set; }
    //    public string Key { get; set; }
    //    public string Value { get; set; }
    //    public string rectType { get; set; }
    //    public string margin { get; set; }
    //    public int width { get; set; }


    //    public rectelgleProp(string v1, string v2, string v3, string v4, string v5, int v6)
    //    {
    //        this.ColorProp = v2 == "5" ? null : v1;
    //        this.Key = v2;
    //        this.Value = v3;
    //        this.rectType = v4;
    //        this.margin = v5;
    //        this.width = v2 == "5" ? 100 : v6;
    //    }


    //}

    //UIElement parent = App.Current.MainWindow;
    //parent.IsEnabled = true;

    //var numberButtons = Enumerable.Range(1, 50).Select(r => new rectelgleProp(r < 5 ? "#00D5B6" : "#C3C3C3", r.ToString(), (r + 9) % 10 == 0 ? r.ToString() : "",
    //    r == 5 ? null : "", r == 6 ? Convert.ToString(r * 100) + " 0 0 0" : Convert.ToString((r > 10 ? r % 10 : r) * 32 + (r > 6 ? 38 : 0)) + " " + Convert.ToString(r / 10 > 0 ? r / 10 * 50 : 0)
    //    + " " + "0 0", r == 5 ? 100 : 30)).ToList();
    //// numberButtonItems.ItemsSource = "";//numberButtons;
    //var numberButtons = Enumerable.Range(1, 50).Select(r => new KeyValuePair<string, string>(r.ToString(), (r + 9) % 10 == 0 ? r.ToString() : "")).ToList();
    //numberButtonItems.ItemsSource = numberButtons;
}
