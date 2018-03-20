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
    /// Interaction logic for ConquerCityView.xaml
    /// </summary>
    public partial class ConquerCityGameView : Page
    {
        private int progressStatus;

        public ConquerCityGameView()
        {
            InitializeComponent();
            setProgressBar(5);
        }

        private void setProgressBar(int status)
        {
            if (Convert.ToInt32(Application.Current.Properties["CurretRedirection"]) != null)
            {
                progressStatus = Convert.ToInt32(Application.Current.Properties["CurretRedirection"]);
            }
            ProgressBarUC _cuc = new ProgressBarUC();
            spProgress.Children.Add(_cuc.LoadProgressbar(status, 4));
        }
    }
}
