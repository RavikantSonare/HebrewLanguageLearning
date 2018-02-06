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
        public JerichoGameView()
        {
            InitializeComponent();
        }

        public void Textbox()
        {
            var _txtBloack = new TextBlock
            {
                TextAlignment = TextAlignment.Center,
                Background = Brushes.Transparent,
                Foreground = Brushes.Gray,
                Text = "Lesson " ,
                FontSize = 15,
                FontWeight = FontWeights.SemiBold
            };
            wpwall.Children.Add(_txtBloack);
        }
    }
}
