using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HebrewLanguageLearning_Users.Models.DataProviders;
using System.Data;
using HebrewLanguageLearning_Users.Models.DataModels;

namespace HebrewLanguageLearning_Users.Models.DataControllers
{
  public class DashboardController
    {
        DBModel _dbobj = new DBModel();
        public DashboardModel getUserProgress()
        {
            DashboardModel _objModel = new DashboardModel();
            try
            {
                {
                    object _obj = _dbobj.SelectData("HLL_ProgressOfUser");
                    DataTable _dt = _obj as DataTable;
                    foreach (DataRow row in _dt.Rows)
                    {
                        _objModel.completedLesson = Convert.ToInt32(row.ItemArray[0].ToString());
                        _objModel.completedPhases = Convert.ToInt32(row.ItemArray[1].ToString());
                        _objModel.completedParagraph = Convert.ToInt32(row.ItemArray[2].ToString());
                        _objModel.LeftDate = row.ItemArray[3].ToString();
                      
                        //if (row.ItemArray[0].ToString() == _accountModel.UserName && row.ItemArray[1].ToString() == _accountModel.Password)
                        //{
                        //    loginStatus = true;
                        //}
                    }
                }
            }
            catch
            {

            }
            return _objModel;

        }
    }
}
