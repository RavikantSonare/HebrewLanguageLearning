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
        public DashboardView()
        {
            InitializeComponent();
            ChildControllerAdd_Loaded();

        }
        private void ChildControllerAdd_Loaded()
        {
            int SetColomnHeight = 0; int SetRowWidht = 0;
            int _currentTopicNo = 5;
            string _currentTopicName = "Culture";
            int _completedLesson = 35;
            string _currentLessonNo = Convert.ToString(_completedLesson+1);
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
                    _txtBloack.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#C0C0C0"));
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
                    var CurrentLocator = currentLocationIndicator(1, _currentTopicNo);
                    var TriangleSet = createTriangle();
                    var CurrentLessonLable = createCurrentLesson(ref _currentLessonNo, ref _currentTopicName);
                    
                    Canvas.SetLeft(CurrentLessonLable, SetRowWidht);
                    Canvas.SetTop(CurrentLessonLable, SetColomnHeight - 55);

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
        private TextBlock createCurrentLesson(ref string currentLessonNo, ref string currentTopic)
        {
            var _txtBloack = new TextBlock
            {
                TextAlignment = TextAlignment.Center,
                Background = Brushes.Transparent,
                Foreground = Brushes.Gray,
                Text = "Lesson " + currentLessonNo + ":" + currentTopic,
                FontSize = 15,
                FontWeight = FontWeights.SemiBold
        };
            return _txtBloack;
        }
        private Polygon createTriangle()
        {
            var arrowTriangle = new Polygon();
            arrowTriangle.Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom("#C3C3C3"));
            arrowTriangle.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D9DCDF"));
            arrowTriangle.StrokeThickness = .8;
            arrowTriangle.HorizontalAlignment = HorizontalAlignment.Left;
            arrowTriangle.VerticalAlignment = VerticalAlignment.Center;
            Point Point1 = new Point(-5, 0);
            Point Point2 = new Point(5, 12);
            Point Point3 = new Point(15, 0);
            PointCollection arrowTrianglePointCollection = new PointCollection();
            arrowTrianglePointCollection.Add(Point1);
            arrowTrianglePointCollection.Add(Point2);
            arrowTrianglePointCollection.Add(Point3);
            arrowTriangle.Points = arrowTrianglePointCollection;
            return arrowTriangle;
           
        }
        private Border currentLocationIndicator(double MarginPro, int CurrentTopicNo)
        {
            var _border = new Border();
            _border.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
            _border.HorizontalAlignment = HorizontalAlignment.Center;
            _border.BorderThickness = new Thickness(2);
            _border.Margin = new Thickness(2, -10, 2, 0);
            _border.Width = 108;
            _border.Height = 36;
            var _canvas = new Canvas();
            for (int i = 1; i <= 9; i++)
            {
                var _txtBloack = new TextBlock
                {
                    TextAlignment = TextAlignment.Center,
                    Background = CurrentTopicNo >= i ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#00D5B6")) : (SolidColorBrush)(new BrushConverter().ConvertFrom("#C3C3C3")),
                    Foreground = Brushes.White,
                    Margin = new Thickness(-1, -1, 2, 5),
                    Width = 5,
                    Height = 26,
                };
                Canvas.SetLeft(_txtBloack, 10 * i);
                Canvas.SetTop(_txtBloack, 4);
                _canvas.Children.Add(_txtBloack);

            }
            _border.Child = _canvas;
            return _border;
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
