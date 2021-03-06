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
        internal Grid LoadProgressbar(int prgBarIdCallie, int prgBarProgressNumber)
        {
            Grid gd_main = new Grid();
            UIElementCollection gd = gd_main.Children;


            prgBarId = prgBarIdCallie;
            Possition();

            // Line Logic
            if (prgBarId == 0 || prgBarId == 3 || prgBarId == 4)
            {
                gd.Add(BlankLine(1020, HorizontalAlignment.Center, 4));
                if (prgBarIdCallie == 3 || prgBarIdCallie == 4 && prgBarProgressNumber > 1)
                {
                    if (prgBarIdCallie == 3)
                    {
                        gd.Add(FillLine(220 * (prgBarProgressNumber + 1), HorizontalAlignment.Left, 20, 4));
                    }else
                    {
                        gd.Add(FillLine(420 * (prgBarProgressNumber + 1), HorizontalAlignment.Left, 20, 4));
                    }
                }
                else
                {
                    gd.Add(FillLine(110 * (prgBarProgressNumber + 1), HorizontalAlignment.Left, 20, 4));
                }
                if (prgBarProgressNumber >= 5) { prgBarProgressNumber++; }
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
                gd.Add(FillLine(110 * (prgBarProgressNumber + 1), HorizontalAlignment.Left));
                gd.Add(BlankLine(360, HorizontalAlignment.Right));
                if (prgBarProgressNumber >= 5)
                {
                    gd.Add(FillLine(110 * (prgBarProgressNumber - 5), HorizontalAlignment.Left, 610));
                }
                gd.Add(_VertyLine);
            }

            // All Image Logic

            // gd.Add(createImage(1, new Thickness(120, -53, 0, 0)));

            switch (prgBarId)
            {
                case 0:
                    gd.Add(createImage(0, new Thickness(20, -53, 0, 0)));
                    gd.Add(createImage(1, new Thickness(120, -53, 0, 0)));
                    break;
                case 3:
                    gd.Add(createImage(1, new Thickness(180, -53, 0, 0)));
                    break;
                case 4:
                    gd.Add(createImage(1, new Thickness(180, -53, 0, 0)));
                    break;
                default:
                    gd.Add(createImage(1, new Thickness(120, -53, 0, 0)));
                    break;
            }
            switch (prgBarId)
            {
                case 0:
                    for (int i = 1; i <= 10; i++)
                    {
                        if (i != 5 && i != 9)
                        {
                            if (i <= prgBarProgressNumber)
                            {
                                gd.Add(createImage(1, new Thickness(_dic[i], -53, 0, 0)));
                            }
                            else
                            {
                                gd.Add(createImage(2, new Thickness(_dic[i], -53, 0, 0)));
                            }
                        }
                    }
                    gd.Add(createImage(0, new Thickness(575, -53, 0, 0)));
                    gd.Add(createImage(0, new Thickness(960, -53, 0, 0)));
                    break;

                case 3:
                    for (int i = 2; i <= 3; i++)
                    {
                        if (i <= prgBarProgressNumber)
                        {
                            gd.Add(createImage(1, new Thickness(_dic[i], -53, 0, 0)));
                        }
                        else
                        {
                            gd.Add(createImage(2, new Thickness(_dic[i], -53, 0, 0)));
                        }
                        // gd.Add(createImage(2, new Thickness(_dic[i], -53, 0, 0)));
                    }
                  
                    break;
                case 4:
                    for (int i = 2; i <= 2; i++)
                    {
                        if (i <= prgBarProgressNumber)
                        {
                            gd.Add(createImage(1, new Thickness(_dic[i], -53, 0, 0)));
                        }
                        else
                        {
                            gd.Add(createImage(2, new Thickness(_dic[i], -53, 0, 0)));
                        }
                       
                    }
                    
                    break;
                default:
                    for (int i = 1; i <= 10; i++)
                    {
                        if (i != 5 && i != 9)
                        {
                            // i == 1 && prgBarId == 1
                            if (i <= prgBarProgressNumber)
                            {
                                gd.Add(createImage(1, new Thickness(_dic[i], -53, 0, 0)));
                            }
                            else
                            {
                                gd.Add(createImage(2, new Thickness(_dic[i], -53, 0, 0)));
                            }
                        }
                    }
                    break;
            }

            var _stcPnl = new StackPanel();
            _stcPnl.Orientation = Orientation.Horizontal;
            _stcPnl.Margin = new Thickness(0, 20, 0, 0);

            var _canPnl = new Canvas();
            _canPnl.Margin = new Thickness(0, 20, 0, 0);
            int j = 0;


            switch (prgBarId)
            {
                case 0:
                    foreach (var Item in EntityConfig._ProgressBarAssociationList)
                    {
                        //  _stcPnl.Children.Add(createCurrentLesson(Item.id, Item.Value, Item.id == 0 || Item.id == 6 || Item.id == 9 ? true : false, Item.id == 0 || Item.id == 6 ? new Thickness(Item.id == 0 ? 110 : 5, 10, 5, 10) : new Thickness(5, 10, 5, 10), Item.id == (prgBarProgressNumber + 1) ? true : false));
                        _stcPnl.Children.Add(createCurrentLesson(Item.id, Item.Value, Item.id == 0 || Item.id == 6 || Item.id == 10 ? true : false, new Thickness(5, 10, 5, 10), Item.id == (prgBarProgressNumber + 1) ? true : false));
                    }
                    gd.Add(_stcPnl);
                    break;
                case 3:
                    int i = 1; Dictionary<int, int> _dicTmp = new Dictionary<int, int>();
                    _dicTmp.Add(1, 150);
                    _dicTmp.Add(2, 250);
                    _dicTmp.Add(3, 250);
                    foreach (var Item in EntityConfig._ProgressBarAssociationList)
                    {
                        if (Item.id != 0 && Item.id != 3 && Item.id != 5 && Item.id != 6 && Item.id != 7 && Item.id != 8 && Item.id != 9 && Item.id != 10 && Item.id != 11)
                        {
                            _stcPnl.Children.Add(createCurrentLesson(Item.id, Item.Value, Item.id == 0 || Item.id == 6 || Item.id == 9 ? true : false, new Thickness(_dicTmp[i], 10, 5, 10), false));
                            i++;
                        }
                    }
                    gd.Add(_stcPnl);
                    break;
                case 4:
                    int k = 1; Dictionary<int, int> _dicTmp4 = new Dictionary<int, int>();
                    _dicTmp4.Add(1, 130);
                    _dicTmp4.Add(2, 600);
                
                    foreach (var Item in EntityConfig._ProgressBarAssociationList)
                    {
                        if (Item.id != 0 && Item.id !=1  && Item.id != 3 && Item.id != 5 && Item.id != 6 && Item.id != 7 && Item.id != 8 && Item.id != 9 && Item.id != 10 && Item.id != 11)
                        {
                            _stcPnl.Children.Add(createCurrentLesson(Item.id, Item.Value, Item.id == 0 || Item.id == 6 || Item.id == 9 ? true : false, new Thickness(_dicTmp4[k], 10, 5, 10), false));
                            k++;
                        }
                    }
                    gd.Add(_stcPnl);
                    break;
                default:
                    foreach (var Item in EntityConfig._ProgressBarAssociationListSecond)
                    {
                        _canPnl.Children.Add(createCurrentLesson(Item.id, Item.Value, Item.id == 0 || Item.id == 6 || Item.id == 9 ? true : false, Item.id == 0 || Item.id == 6 ? new Thickness(Item.id == 0 ? 110 : 650, -96, 5, 10) : new Thickness(j, 10, 5, 10), Item.id == (prgBarProgressNumber + 1) ? true : false));
                        if (Item.id == 0 || Item.id == 6) { gd.Add(createImage(3, new Thickness(Item.id == 0 ? 190 : 715, -115, 5, 10))); }
                        j += Item.Value.Length * 8;
                    }
                    gd.Add(_canPnl);
                    break;
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
                FontSize = 10.6,
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
        internal Line BlankLine(int lnlenght, HorizontalAlignment HoriAlignment, int setTopMgn = 0)
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
        internal Line FillLine(int lnlenght, HorizontalAlignment HoriAlignment, int setLeftMgn = 20, int setTopMgn = 0)
        {
            var _FillLine = new Line
            {
                HorizontalAlignment = HoriAlignment,//HorizontalAlignment.Left,
                X1 = (lnlenght > 1000 ? 1025 : lnlenght),
                Stroke = (SolidColorBrush)new BrushConverter().ConvertFrom("#00D5B6"),
                StrokeThickness = 3,
                Margin = new Thickness(setLeftMgn, setTopMgn, 0, 0),
            };
            return _FillLine;
        }

        static Dictionary<int, int> _dic = new Dictionary<int, int>();
        internal void Possition()
        {
            _dic.Clear();
            // if (_dic.Count == 0)
            switch (prgBarId)
            {
                case 0:
                    _dic.Add(1, 210);
                    _dic.Add(2, 315);
                    _dic.Add(3, 400);
                    _dic.Add(4, 490);
                    _dic.Add(5, 550);
                    _dic.Add(6, 650);
                    _dic.Add(7, 730);
                    _dic.Add(8, 850);
                    _dic.Add(9, 850);
                    _dic.Add(10, 1030);
                    break;

                case 3:
                    _dic.Add(1, 350);
                    _dic.Add(2, 520);
                    _dic.Add(3, 880);
                    break;
                case 4:
                    _dic.Add(1, 350);
                    _dic.Add(2, 880);
                break;
                default:
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
                    break;
            }


        }
    }

}
