using Caliburn.Micro;
using HebrewLanguageLearning_Users.ViewModels.BibleLearning;
using HebrewLanguageLearning_Users.Views.CommonUserControls;
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

namespace HebrewLanguageLearning_Users.Views.BibleLearning
{
    /// <summary>
    /// Interaction logic for BibleLearningFromMediaView.xaml
    /// </summary>
    public partial class BibleLearningFromMediaView : Page
    {
        
        int GreenDotstatus = 0;
        public BibleLearningFromMediaView()
        {
            InitializeComponent();        
            setProgressBar(GreenDotstatus);           
        }
        
        private void setProgressBar(int GreenDotstatus)
        {

            var ScreenTemp = Application.Current.Properties["CurretPage"];
            int ScreenNo = 2;
            int TypeOfProgressBar=1;
            if (ScreenTemp != null)
            {
                ScreenNo = Convert.ToInt32(ScreenTemp);
                TypeOfProgressBar = 4;
                GreenDotstatus = 6;
            }
            else
            {
                TypeOfProgressBar = 1;
            }

            
            ProgressBarUC _cuc = new ProgressBarUC();
            spProgress.Children.Add(_cuc.LoadProgressbar(TypeOfProgressBar, GreenDotstatus));
        }
        

        //private void btnPlay_Click(object sender, RoutedEventArgs e)
        //{
        //    MediaPlayer.Play();
        //}

        //private void btnPause_Click(object sender, RoutedEventArgs e)
        //{
        //    MediaPlayer.Pause();
        //}

        //private void btnStop_Click(object sender, RoutedEventArgs e)
        //{
        //    MediaPlayer.Stop();
        //}
        public class TodoItem
        {
            public int Id { get; set; }
            public string Image { get; set; }
            public int Type { get; set; }
        }
        
     }

    }

