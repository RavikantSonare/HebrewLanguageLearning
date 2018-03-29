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
    /// Interaction logic for BibleLearningWordTypingView.xaml
    /// </summary>
    public partial class BibleLearningWordTypingView : Page
    {
        int GreenDotstatus = 3;
        public BibleLearningWordTypingView()
        {
            InitializeComponent();
            setProgressBar(GreenDotstatus);
        }
        private void setProgressBar(int GreenDotstatus)
        {

            var ScreenTemp = Application.Current.Properties["CurretPage"];
            int ScreenNo = 4;
            int TypeOfProgressBar = 0;
            if (ScreenTemp != null)
            {

                ScreenNo = Convert.ToInt32(ScreenTemp);
                if (ScreenNo == 8)
                {

                    GreenDotstatus = 7;
                }
                if (ScreenNo == 9)
                {

                    GreenDotstatus = 9;
                }
                var tmpProgress = Application.Current.Properties["CurretProgressbar"];
                var tmpGreenDot = Application.Current.Properties["CurretGreenDot"];
                if (tmpProgress != null && tmpGreenDot != null)
                {
                    TypeOfProgressBar = Convert.ToInt32(tmpProgress);
                    GreenDotstatus = 4;
                  
                }
            }
            
            ProgressBarUC _cuc = new ProgressBarUC();
            spProgress.Children.Add(_cuc.LoadProgressbar(TypeOfProgressBar, GreenDotstatus));
        }
    }
}
