﻿using HebrewLanguageLearning_Users.GenericClasses;
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

namespace HebrewLanguageLearning_Users.Views.CommonUserControls
{
    /// <summary>
    /// Interaction logic for ProgressBarUC.xaml
    /// </summary>
    /// 


    public partial class ProgressBarUC : UserControl
    {
        public ProgressBarUC()
        {
            InitializeComponent();
        }
        static int prgBarId = 0;
        internal Grid LoadProgressbar(int prgBarIdCallie)
        {
            Grid gd_main = new Grid();
            UIElementCollection gd = gd_main.Children;


            prgBarId = prgBarIdCallie;
            Possition();

            // Line Logic

            if (prgBarId == 0)
            {

                gd.Add(BlankLine(950, HorizontalAlignment.Center,4)); gd.Add(FillLine(150, HorizontalAlignment.Left,4));
            }
            else
            {
                var _VertyLine = new Line
                {
                    HorizontalAlignment = HorizontalAlignment.Center,//HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    // X1 = 100,
                    Stroke = (SolidColorBrush)new BrushConverter().ConvertFrom("#A9CDDA"),
                    StrokeThickness = 3,
                    Margin = new Thickness(180, -49, 0, 0),
                    Y1 = 100,

                };
                gd.Add(BlankLine(600, HorizontalAlignment.Left));
                gd.Add(FillLine(100 * 2, HorizontalAlignment.Left));
                gd.Add(BlankLine(360, HorizontalAlignment.Right)); 
                gd.Add(_VertyLine);
            }

            // All Image Logic

            gd.Add(createImage(1, new Thickness(120, -53, 0, 0)));
            if (prgBarId == 0)
            {
                gd.Add(createImage(0, new Thickness(20, -53, 0, 0)));
                gd.Add(createImage(1, new Thickness(120, -53, 0, 0)));
            }
            gd.Add(createImage(1, new Thickness(120, -53, 0, 0)));

            if (prgBarId == 0)
            {
                for (int i = 1; i <= 9; i++)
                {
                    if (i != 5 && i != 8)
                    {
                        int thinkValue = (i + 1) * 95;
                        if (i == 1 && prgBarId == 1)
                        { gd.Add(createImage(1, new Thickness(_dic[i], -53, 0, 0))); }
                        else
                        {
                            gd.Add(createImage(2, new Thickness(_dic[i], -53, 0, 0)));
                        }
                    }
                }

                gd.Add(createImage(0, new Thickness(595, -53, 0, 0)));
                gd.Add(createImage(0, new Thickness(870, -53, 0, 0)));
            }
            else
            {
                for (int i = 1; i <= 10; i++)
                {
                    if (i != 5 && i != 9)
                    {
                        if (i == 1 && prgBarId == 1)
                        { gd.Add(createImage(1, new Thickness(_dic[i], -53, 0, 0))); }
                        else
                        {
                            gd.Add(createImage(2, new Thickness(_dic[i], -53, 0, 0)));
                        }
                    }
                }



            }
            var _stcPnl = new StackPanel();
            _stcPnl.Orientation = Orientation.Horizontal;
            _stcPnl.Margin = new Thickness(0, 20, 0, 0);

            var _canPnl = new Canvas();
            _canPnl.Margin = new Thickness(0, 20, 0, 0);
            int j = 0;

            if (prgBarId == 0)
            {
                foreach (var Item in EntityConfig._ProgressBarAssociationList)
                {
                    _stcPnl.Children.Add(createCurrentLesson(Item.id, Item.Value, Item.id == 0 || Item.id == 6 || Item.id == 9 ? true : false, new Thickness(5, 10, 5, 10), false));
                }
                gd.Add(_stcPnl);
            }
            else
            {
                foreach (var Item in EntityConfig._ProgressBarAssociationListSecond)
                {
                    _canPnl.Children.Add(createCurrentLesson(Item.id, Item.Value, Item.id == 0 || Item.id == 6 || Item.id == 9 ? true : false, Item.id == 0 || Item.id == 6 ? new Thickness(Item.id == 0 ? 110 : 650, -96, 5, 10) : new Thickness(j, 10, 5, 10), Item.id == 2 ? true : false));
                    if (Item.id == 0 || Item.id == 6) { gd.Add(createImage(3, new Thickness(Item.id == 0 ? 190 : 715, -115, 5, 10))); }
                    j += Item.Value.Length * 8;
                }
                gd.Add(_canPnl);
            }


            return gd_main;
        }
        internal static Image createImage(int type, Thickness Possition)
        {

            var _redArrowImage = new Image
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = Possition,
            };
            if (type == 0)
            { _redArrowImage.Source = new BitmapImage(new Uri("/HebrewLanguageLearning_Users;component/Assets/pink_triangle.png", UriKind.Relative)); _redArrowImage.Height = 16; _redArrowImage.Width = 17; }
            if (type == 1)
            { _redArrowImage.Source = new BitmapImage(new Uri("/HebrewLanguageLearning_Users;component/Assets/active_dot.png", UriKind.RelativeOrAbsolute)); _redArrowImage.Height = 20; _redArrowImage.Width = 20; }
            if (type == 2)
            { _redArrowImage.Source = new BitmapImage(new Uri("/HebrewLanguageLearning_Users;component/Assets/gray_dot.png", UriKind.RelativeOrAbsolute)); _redArrowImage.Height = 20; _redArrowImage.Width = 20; }
            if (type == 3)
            { _redArrowImage.Source = new BitmapImage(new Uri("/HebrewLanguageLearning_Users;component/Assets/small_triangle.png", UriKind.RelativeOrAbsolute)); _redArrowImage.Height = 16; _redArrowImage.Width = 17; }

            return _redArrowImage;
        }
        internal static TextBlock createCurrentLesson(int id, string currentTopic, bool status, Thickness mrgnThik, bool currentValue)
        {

            var _txtBloack = new TextBlock
            {
                TextAlignment = TextAlignment.Left,
                Background = Brushes.Transparent,
                Text = currentTopic,
                FontSize = 11,
                FontWeight = FontWeights.SemiBold,
                Margin = mrgnThik,//new Thickness(5, 10, 5, 10),
                Height = 20

            };
            if (status)
            { _txtBloack.Foreground = Brushes.Black; }
            else
            { _txtBloack.Foreground = Brushes.Gray; }

            if (currentValue)
            { _txtBloack.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#00D5B6"); }

            return _txtBloack;
        }
        internal Line BlankLine(int lnlenght, HorizontalAlignment HoriAlignment,int setTopMgn=0)
        {
            var _blankLine = new Line
            {
                HorizontalAlignment = HoriAlignment,//HorizontalAlignment.Center,
                X1 = lnlenght,
                Stroke = (SolidColorBrush)new BrushConverter().ConvertFrom("#ccc"),
                StrokeThickness = 3,
                Margin = new Thickness(20, setTopMgn, 80, 0),
            };
            return _blankLine;
        }
        internal Line FillLine(int lnlenght, HorizontalAlignment HoriAlignment,int setTopMgn=0)
        {
            var _FillLine = new Line
            {
                HorizontalAlignment = HoriAlignment,//HorizontalAlignment.Left,
                X1 = lnlenght,
                Stroke = (SolidColorBrush)new BrushConverter().ConvertFrom("#00D5B6"),
                StrokeThickness = 3,
                Margin = new Thickness(20, setTopMgn, 0, 0),
            };
            return _FillLine;
        }

        static Dictionary<int, int> _dic = new Dictionary<int, int>();
        internal void Possition()
        {
            if (prgBarId == 0)
            {
                _dic.Add(1, 210);
                _dic.Add(2, 315);
                _dic.Add(3, 420);
                _dic.Add(4, 510);
                _dic.Add(5, 600);
                _dic.Add(6, 670);
                _dic.Add(7, 760);
                _dic.Add(8, 910);
                _dic.Add(9, 950);
        
            }
            else
            {
                _dic.Add(1, 210);
                _dic.Add(2, 330);
                _dic.Add(3, 440);
                _dic.Add(4, 550);
                _dic.Add(5, 650);
                _dic.Add(6, 700);
                _dic.Add(7, 810);
                _dic.Add(8, 910);
                _dic.Add(9, 1010);
                _dic.Add(10, 1110);
            }

        }
    }

}