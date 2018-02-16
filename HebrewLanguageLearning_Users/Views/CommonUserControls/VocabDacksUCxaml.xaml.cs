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

namespace HebrewLanguageLearning_Users.Views.CommonUserControls
{
    /// <summary>
    /// Interaction logic for VocabDacksUCxaml.xaml
    /// </summary>
    public partial class VocabDacksUCxaml : UserControl,INotifyPropertyChanged
    {
        public VocabDacksUCxaml()
        {
            InitializeComponent();
        }
        public static int toggleValue = 1;

        public event PropertyChangedEventHandler PropertyChanged;
        public void onPropertyChanged(string property)
        {

            //if (PropertyChanged != null)
            //{
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            //}
        }
        private void MenuIconOpenRight_Click(object sender, RoutedEventArgs e)
        {
            showhide();
           
        }
        
        public void showhide()
        {
           // ShellView obj = new ShellView();
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

        }
    }
}
