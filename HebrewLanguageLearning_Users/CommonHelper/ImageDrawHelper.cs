using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HebrewLanguageLearning_Users.CommonHelper
{
    class ImageDrawHelper
    {
        internal static TextBlock createCurrentLesson(ref string currentLessonNo, ref string currentTopic)
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

        internal static Polygon createTriangle(Point Point1, Point Point2, Point Point3, string Stroke, string Fill)
        {
            var arrowTriangle = new Polygon();
            arrowTriangle.Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom(Stroke));
            arrowTriangle.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(Fill));
            arrowTriangle.StrokeThickness = .8;
            arrowTriangle.HorizontalAlignment = HorizontalAlignment.Left;
            arrowTriangle.VerticalAlignment = VerticalAlignment.Center;
            PointCollection arrowTrianglePointCollection = new PointCollection();
            arrowTrianglePointCollection.Add(Point1);
            arrowTrianglePointCollection.Add(Point2);
            arrowTrianglePointCollection.Add(Point3);
            arrowTriangle.Points = arrowTrianglePointCollection;
            return arrowTriangle;
        }

        internal static Border nextTooltipLocationIndicator(double MarginPro, string CurrentTopicNo, string toolTipTopicData)
        {
            var _border = new Border();
            _border.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#9DE8E1");
            _border.HorizontalAlignment = HorizontalAlignment.Center;
            _border.VerticalAlignment = VerticalAlignment.Center;
            _border.BorderThickness = new Thickness(4);
            _border.Width = 138;
            _border.Height = 28;
            _border.CornerRadius = new CornerRadius(4);
            _border.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9DE8E1"));
            var _canvas = new Canvas();
            var _txtBloack = new TextBlock
            {
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = Brushes.Gray,
                Margin = new Thickness(-2, 0, 0, 1),
                Width = 134,
                Height = 28,
                FontSize = 14,
                FontWeight = FontWeights.Medium,
                Text = "Lesson:" + CurrentTopicNo + " " + toolTipTopicData,

            };
            Canvas.SetLeft(_txtBloack, 1);
            Canvas.SetTop(_txtBloack, 1);
            _canvas.Children.Add(_txtBloack);
            _border.Child = _canvas;
            return _border;
        }

        internal static Border currentLocationIndicator(double MarginPro, int CurrentTopicNo)
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
}
