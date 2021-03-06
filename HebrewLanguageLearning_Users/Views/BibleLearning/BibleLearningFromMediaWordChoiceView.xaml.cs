﻿using HebrewLanguageLearning_Users.Views.CommonUserControls;
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
        int GreenDotstatus = 1;
        public BibleLearningFromMediaWordChoiceView()
        {
            InitializeComponent();
            setProgressBar(GreenDotstatus);
        }
       
        private void setProgressBar(int GreenDotstatus)
        {

            var ScreenTemp = Application.Current.Properties["CurretPage"];
            int ScreenNo = 3;
            int TypeOfProgressBar = 0;
            if (ScreenTemp != null)
            {
                ScreenNo = Convert.ToInt32(ScreenTemp);
                if (ScreenNo == 7)
                {
                   // TypeOfProgressBar = 4;
                    GreenDotstatus = 6;
                }
                if (ScreenNo == 2)
                {
                    TypeOfProgressBar = 4;
                       GreenDotstatus = 2;
                }
            }
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

    }
}
