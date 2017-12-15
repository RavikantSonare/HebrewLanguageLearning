using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace HebrewLanguageLearning_Users.ViewModels
{

    public class ShellViewModel : Conductor<object>
    {
      
        private readonly SimpleContainer container;
        private INavigationService navigationService;

        public ShellViewModel(SimpleContainer container)
        {
            this.container = container;
        }

        public void RegisterFrame(Frame frame)
        {
            navigationService = new FrameAdapter(frame);

            container.Instance(navigationService);
               navigationService.NavigateToViewModel(typeof(BibleLearning.BibleLearningViewModel));
            // navigationService.NavigateToViewModel(typeof(Dashboard.DashboardViewModel));
            // navigationService.NavigateToViewModel(typeof(Account.LoginViewModel));
        }



        public void Logout()
        {

            // Application.Current.Shutdown();
        }
        public void Profile()
        {

        }
        public void Settings()
        {
        }
        public void MenuIconOpen()
        {
            if (StateValue == "Hidden") { StateValue = "Visible"; }
            else
            {
                StateValue = "Hidden";
            }
        }
      
        private string _state = "Hidden";
        public string StateValue
        {
            get { return _state; }
            set
            {
                _state = value;
                NotifyOfPropertyChange(() => StateValue);
            }
        }
       
    }

   
}
