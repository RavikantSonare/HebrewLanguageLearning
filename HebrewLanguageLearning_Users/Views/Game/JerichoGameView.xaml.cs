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

namespace HebrewLanguageLearning_Users.Views.Game
{
    /// <summary>
    /// Interaction logic for JerichoGameView.xaml
    /// </summary>
    public partial class JerichoGameView : Page
    {
        int GreenDotstatus = 2; int TypeOfProgressBar = 2;
        public JerichoGameView()
        {
            InitializeComponent();
            setProgressBar(GreenDotstatus);
        }

        private void setProgressBar(int GreenDotstatus)
        {
            ProgressBarUC _cuc = new ProgressBarUC();
            spProgress.Children.Add(_cuc.LoadProgressbar(TypeOfProgressBar, GreenDotstatus));
        }
    }
}
