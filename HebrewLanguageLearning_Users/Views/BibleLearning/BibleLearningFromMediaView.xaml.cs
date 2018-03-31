using Caliburn.Micro;
using HebrewLanguageLearning_Users.ViewModels.BibleLearning;
using HebrewLanguageLearning_Users.Views.CommonUserControls;
using System;
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

        int GreenDotstatus = 0; int TypeOfProgressBar = 0;
        public BibleLearningFromMediaView()
        {
            InitializeComponent();
            setProgressBar(GreenDotstatus);
        }

        private void setProgressBar(int GreenDotstatus)
        {

            var ScreenTemp = Application.Current.Properties["CurretPage"];
            int ScreenNo = 1;

            if (ScreenTemp != null)
            {
                ScreenNo = Convert.ToInt32(ScreenTemp);
                if (ScreenNo == 6)
                {
                    GreenDotstatus = 5;
                }
            }

            // Second Section Review Lesson
            var tmpProgress = Application.Current.Properties["CurretProgressbar"];
            var tmpGreenDot = Application.Current.Properties["CurretGreenDot"];
            if (tmpProgress != null && tmpGreenDot != null)
            {
                TypeOfProgressBar = Convert.ToInt32(tmpProgress);
                GreenDotstatus = Convert.ToInt32(tmpGreenDot);
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

