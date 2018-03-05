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
        
        int progressStatus=0;
        string commonUri = @"D:\Ravi\Project\HLL\HebrewLanguageLearning\HebrewLanguageLearning_Users\Media\Videos\";
        private  readonly INavigationService navigationService;
        
      

        public BibleLearningFromMediaView()
        {
            InitializeComponent();
          
            setProgressBar(ref progressStatus);
           
        }

       
        private void setProgressBar(ref int status )
        {
            ProgressBarUC _cuc = new ProgressBarUC();
            spProgress.Children.Add(_cuc.LoadProgressbar(status));
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

