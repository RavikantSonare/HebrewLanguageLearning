using HebrewLanguageLearning_Admin.DBContext;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace HebrewLanguageLearning_Admin.GenericClasses
{
    public class EntityConfig
    {
        public static string getnewid(string entityname)
        {
            string ReturnValue = string.Empty;
            ObjectParameter output = new ObjectParameter("row_id", typeof(string));
            using (var context = new HLLDBContext())
            {
                var DataList = context.get_row_id(entityname, output).ToList();
                ReturnValue = Convert.ToString(output.Value);
              
            }

            //SqlConnection con = new SqlConnection("Data Source=mssql6.gear.host;Initial Catalog=hllmobi96;Persist Security Info=True;User ID=hllmobi96;Password=Km6M~n5-I561");
            //con.Open();
            //SqlCommand cmd = new SqlCommand("get_row_id", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@t_name", entityname);
            //cmd.Parameters.Add("@row_id", SqlDbType.NVarChar, 500);
            //cmd.Parameters["@row_id"].Direction = ParameterDirection.Output;
            //cmd.ExecuteNonQuery();
            //var message = (string)cmd.Parameters["@row_id"].Value;
            //con.Close();

            return ReturnValue;

        }


    }

    public class SI
    {
        public static List<SelectListItem> _PurposeforLearningHebrewList = new List<SelectListItem>() { new SelectListItem { Value = "0", Text ="Bible Translation Project" },
        new SelectListItem{Value = "1", Text ="Pastoral Ministry" },new SelectListItem{ Value = "2", Text =" Academics" },new SelectListItem{ Value = "3",Text ="Other" }};
    }
}