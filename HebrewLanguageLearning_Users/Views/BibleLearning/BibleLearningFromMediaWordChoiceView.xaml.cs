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
    /// Interaction logic for BibleLearningFromMediaWordChoiceView.xaml
    /// </summary>
    public partial class BibleLearningFromMediaWordChoiceView : Page
    {
        int progressStatus = 1;
        public BibleLearningFromMediaWordChoiceView()
        {
            InitializeComponent();
            setProgressBar(ref progressStatus);
        }
        private void setProgressBar(ref int status)
        {
           
            ProgressBarUC _cuc = new ProgressBarUC();
            spProgress.Children.Add(_cuc.LoadProgressbar(progressStatus));
        }
    }
}
