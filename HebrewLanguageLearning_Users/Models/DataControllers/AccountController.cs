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
    public class AccountController
    {
        DBModel _dbobj = new DBModel();
        public bool getAuthentication(AccountModel _accountModel)
        {
            bool loginStatus = false;
            try
            {
               
                {
                    object _obj = _dbobj.SelectData("HLL_AuthenticateUser");
                    DataTable _dt = _obj as DataTable;
                    foreach (DataRow row in _dt.Rows)
                    {
                        if (row.ItemArray[0].ToString() == _accountModel.UserName && row.ItemArray[1].ToString() == _accountModel.Password)
                        {
                            loginStatus = true;
                        }
                    }
                }
            }
            catch
            {

            }
            return loginStatus;

        }
    }
}
