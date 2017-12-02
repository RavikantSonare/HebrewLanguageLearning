using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HebrewLanguageLearning_Users.ViewModels.Dashboard

{
    public class DashboardViewModel : Conductor<object>
    {
        //public DashboardViewModel()
        //{

        //}
        protected override void OnActivate()
        {
            base.OnActivate();
            //MessageBox.Show("Deshboard");//This is for testing - currently doesn't display
        }
    }
}
