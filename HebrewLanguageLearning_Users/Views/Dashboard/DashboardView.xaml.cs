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

namespace HebrewLanguageLearning_Users.Views.Dashboard
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : Page
    {
        public DashboardView()
        {
            InitializeComponent();

            //UIElement parent = App.Current.MainWindow;
            //parent.IsEnabled = true;

            var numberButtons = Enumerable.Range(1, 50).Select(r => new rectelgleProp(r < 5 ? "#00D5B6" : "#C3C3C3", r.ToString(), (r + 9) % 10 == 0 ? r.ToString() : "",
                r == 5 ? null : "", r == 6 ? Convert.ToString(r * 100) + " 0 0 0" : Convert.ToString((r > 10 ? r % 10 : r) * 32 + (r > 6 ? 38 : 0)) + " " + Convert.ToString(r / 10 > 0 ? r / 10 * 50 : 0)
                + " " + "0 0", r == 5 ? 100 : 30)).ToList();
            numberButtonItems.ItemsSource = numberButtons;

            //var numberButtons = Enumerable.Range(1, 50).Select(r => new KeyValuePair<string, string>(r.ToString(), (r + 9) % 10 == 0 ? r.ToString() : "")).ToList();
            //numberButtonItems.ItemsSource = numberButtons;
        }

       
    }
    public class rectelgleProp
    {
        public string ColorProp { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string rectType { get; set; }
        public string margin { get; set; }
        public int width { get; set; }


        public rectelgleProp(string v1, string v2, string v3, string v4, string v5, int v6)
        {
            this.ColorProp = v2 == "5" ? null : v1;
            this.Key = v2;
            this.Value = v3;
            this.rectType = v4;
            this.margin = v5;
            this.width = v2 == "5" ? 100 : v6;
        }


    }


}
