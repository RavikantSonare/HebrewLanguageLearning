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
        string commonUri= @"D:\Ravi\Projects\HLL\HebrewLanguageLearning\HebrewLanguageLearning_Users\Media\";
        public BibleLearningFromMediaView()
        {
            InitializeComponent();
           // genList();
            setProgressBar(ref progressStatus);
            mePlayer.Source = new Uri(commonUri+ "Videos/ELL_PART_5_768k.wmv");  //ELL_PART_5_768k.wmv
           // MediaPlayer.Play();
        }
        private void setProgressBar(ref int status )
        {
            ProgressBarUC _cuc = new ProgressBarUC();
            spProgress.Children.Add(_cuc.LoadProgressbar(status));
        }
        

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            mePlayer.Play();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
          //  mePlayer.Pause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
          //  mePlayer.Stop();
        }
        public class TodoItem
        {
            public int Id { get; set; }
            public string Image { get; set; }
            public int Type { get; set; }
        }


       public void genList()
        {
            //List<TodoItem> items = new List<TodoItem>();
            //items.Add(new TodoItem() { Id = 1, Image = @"\Assets\volume.png", Type = 1 });
            //items.Add(new TodoItem() { Id = 2, Image = @"\Assets\volume.png", Type = 2 });
            //items.Add(new TodoItem() { Id = 3, Image = @"\Assets\volume.png", Type = 3 });
            //items.Add(new TodoItem() { Id = 4, Image = @"\Assets\volume.png", Type = 1 });
            //items.Add(new TodoItem() { Id = 5, Image = @"\Assets\volume.png", Type = 2 });
            //items.Add(new TodoItem() { Id = 6, Image = @"\Assets\volume.png", Type = 3 });
            //items.Add(new TodoItem() { Id = 7, Image = @"\Assets\volume.png", Type = 1 });
            //items.Add(new TodoItem() { Id = 8, Image = @"\Assets\volume.png", Type = 2 });
            //items.Add(new TodoItem() { Id = 9, Image = @"\Assets\volume.png", Type = 3 });
            //items.Add(new TodoItem() { Id = 10, Image = @"\Assets\volume.png", Type = 1 });
            //items.Add(new TodoItem() { Id = 11, Image = @"\Assets\volume.png", Type = 2 });
            //items.Add(new TodoItem() { Id = 12, Image = @"\Assets\volume.png", Type = 3 });
            //items.Add(new TodoItem() { Id = 13, Image = @"\Assets\volume.png", Type = 1 });
            //items.Add(new TodoItem() { Id = 14, Image = @"\Assets\volume.png", Type = 2 });
            //items.Add(new TodoItem() { Id = 15, Image = @"\Assets\volume.png", Type = 3 });
            //items.Add(new TodoItem() { Id = 16, Image = @"\Assets\volume.png", Type = 1 });
            //items.Add(new TodoItem() { Id = 17, Image = @"\Assets\volume.png", Type = 2 });
            //items.Add(new TodoItem() { Id = 18, Image = @"\Assets\volume.png", Type = 3 });
            //items.Add(new TodoItem() { Id = 19, Image = @"\Assets\volume.png", Type = 2 });
            //items.Add(new TodoItem() { Id = 20, Image = @"\Assets\volume.png", Type = 3 });
            //itmecontrol.ItemsSource = items;
        }

    }
}
