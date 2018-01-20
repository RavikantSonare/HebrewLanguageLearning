using Caliburn.Micro;
using HebrewLanguageLearning_Users.Models;
using HebrewLanguageLearning_Users.Views.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HebrewLanguageLearning_Users.ViewModels.Account
{
    public class LoginViewModel : Screen
    {
      
        #region Property
        private string _userName;
        private string _password;
        private bool _rememberMe;
        private string _error;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }
        public bool RememberMe
        {
            get { return _rememberMe; }
            set
            {
                _rememberMe = value;
                NotifyOfPropertyChange(() => RememberMe);
            }
        }
        public string Error
        {
            get { return _error; }
            set
            {
                _error = value;
                NotifyOfPropertyChange(() => Error);
            }
        }
        #endregion

        private readonly INavigationService navigationService;
        public LoginViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
        public void btn_Login()
        {

            if (!string.IsNullOrEmpty(UserName) || !string.IsNullOrEmpty(Password))

            {
                if (UserName=="1" || Password=="1")
                {
                    
              
                    navigationService.NavigateToViewModel(typeof(Dashboard.DashboardViewModel));
                  
                }
                else
                {
                    Error = "Your user id or password miss matched";
                }
            }
            if (string.IsNullOrEmpty(UserName))
            {
                Error = "Please Enter User Name";
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                Error = "Please Enter User Password";
                return;
            }
        }
        public void btn_SyncData()
        {


        }

    }
   


}
