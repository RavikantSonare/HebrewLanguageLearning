using Caliburn.Micro;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.ComponentModel;

namespace HebrewLanguageLearning_Users.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window, INotifyPropertyChanged
    {
        public ShellView()
        {
            InitializeComponent();
            //  AnimationSpeed = TimeSpan.FromMilliseconds(150);
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuIconOpen_Click(object sender, RoutedEventArgs e)
        {
            StateMenu.Toggle(0);
        }
        private void ShadowMouseDown(object sender, MouseButtonEventArgs e)
        {
            // if (ClosingType == ClosingType.Auto) Hide();
        }

        private void LogOut_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
        public int toggleValue = 0;
        private void MenuIconOpenRight_Click(object sender, RoutedEventArgs e)
        {
            showhide();
            //  StateMenuRight.Toggle(1);
            // var Data = obj.SelectData("HLL_VocabDecks");
        }
        //private void btnRightMenuHide_Click(object sender, RoutedEventArgs e)
        //{
        //    ShowHideMenu("sbHideRightMenu", btnRightMenuHide, btnRightMenuShow, pnlRightMenu);
        //}
        //private void btnRightMenuShow_Click(object sender, RoutedEventArgs e)
        //{

        //}
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

            onPropertyChanged("VocabDecksPanel");


            //if (Storyboard.Contains("Show"))
            //{
            //}
            //else if (Storyboard.Contains("Hide"))
            //{
            //    //btnHide.Visibility = System.Windows.Visibility.Hidden;
            //    //btnShow.Visibility = System.Windows.Visibility.Visible;
            //}
        }
        public void onPropertyChanged(string property)
        {

            //if (PropertyChanged != null)
            //{
         ///       PropertyChanged(this, new PropertyChangedEventArgs(property));
           // }
        }
        public event PropertyChangedEventHandler PropertyChanged;

       
    }
}

