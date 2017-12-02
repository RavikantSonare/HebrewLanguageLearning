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

namespace HebrewLanguageLearning_Users.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
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

        private void MenuIconOpenRight_Click(object sender, RoutedEventArgs e)
        {
            StateMenuRight.Toggle(1);
        }
    }
}
