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
        public static List<SelectListItem> _PurposeforLessonList = new List<SelectListItem>()
        {
         new SelectListItem{ Value = "0",  Text ="Please Select Lesson" }
        ,new SelectListItem{ Value = "1",  Text ="Lesson-1" }
        ,new SelectListItem{ Value = "2",  Text ="Lesson-2" }
        ,new SelectListItem{ Value = "3",  Text ="Lesson-3" }
        ,new SelectListItem{ Value = "4",  Text ="Lesson-4" }
        ,new SelectListItem{ Value = "5",  Text ="Lesson-5" }
        ,new SelectListItem{ Value = "6",  Text ="Lesson-6" }
        ,new SelectListItem{ Value = "7",  Text ="Lesson-7" }
        ,new SelectListItem{ Value = "8",  Text ="Lesson-8" }
        ,new SelectListItem{ Value = "9",  Text ="Lesson-9" }
        ,new SelectListItem{ Value = "10", Text ="Lesson-10" }
        ,new SelectListItem{ Value = "11", Text ="Lesson-11" }
        ,new SelectListItem{ Value = "12", Text ="Lesson-12" }
        ,new SelectListItem{ Value = "13", Text ="Lesson-13" }
        ,new SelectListItem{ Value = "14", Text ="Lesson-14" }
        ,new SelectListItem{ Value = "15", Text ="Lesson-15" }
        ,new SelectListItem{ Value = "16", Text ="Lesson-16" }
        ,new SelectListItem{ Value = "17", Text ="Lesson-17" }
        ,new SelectListItem{ Value = "18", Text ="Lesson-18" }
        ,new SelectListItem{ Value = "19", Text ="Lesson-19" }
        ,new SelectListItem{ Value = "20", Text ="Lesson-20" }
        ,new SelectListItem{ Value = "21", Text ="Lesson-21" }
        ,new SelectListItem{ Value = "22", Text ="Lesson-22" }
        ,new SelectListItem{ Value = "23", Text ="Lesson-23" }
        ,new SelectListItem{ Value = "24", Text ="Lesson-24" }
        ,new SelectListItem{ Value = "25", Text ="Lesson-25" }
        ,new SelectListItem{ Value = "26", Text ="Lesson-26" }
        ,new SelectListItem{ Value = "27", Text ="Lesson-27" }
        ,new SelectListItem{ Value = "28", Text ="Lesson-28" }
        ,new SelectListItem{ Value = "29", Text ="Lesson-29" }
        ,new SelectListItem{ Value = "30", Text ="Lesson-30" }
        ,new SelectListItem{ Value = "31", Text ="Lesson-31" }
        ,new SelectListItem{ Value = "32", Text ="Lesson-32" }
        ,new SelectListItem{ Value = "33", Text ="Lesson-33" }
        ,new SelectListItem{ Value = "34", Text ="Lesson-34" }
        ,new SelectListItem{ Value = "35", Text ="Lesson-35" }
        ,new SelectListItem{ Value = "36", Text ="Lesson-36" }
        ,new SelectListItem{ Value = "37", Text ="Lesson-37" }
        ,new SelectListItem{ Value = "38", Text ="Lesson-38" }
        ,new SelectListItem{ Value = "39", Text ="Lesson-39" }
        ,new SelectListItem{ Value = "40", Text ="Lesson-40" }
        ,new SelectListItem{ Value = "41", Text ="Lesson-41" }
        ,new SelectListItem{ Value = "42", Text ="Lesson-42" }
        ,new SelectListItem{ Value = "43", Text ="Lesson-43" }
        ,new SelectListItem{ Value = "44", Text ="Lesson-44" }
        ,new SelectListItem{ Value = "45", Text ="Lesson-45" }
        ,new SelectListItem{ Value = "46", Text ="Lesson-46" }
        ,new SelectListItem{ Value = "47", Text ="Lesson-47" }
        ,new SelectListItem{ Value = "48", Text ="Lesson-48" }
        ,new SelectListItem{ Value = "49", Text ="Lesson-49" }
        ,new SelectListItem{ Value = "50", Text ="Lesson-50" }

        };


        public static Dictionary<int, string> fileSavedLocation = new Dictionary<int, string> { { 0, "Sounds" }, { 1, "Pictures" }, { 2, "Videos" } };

        public static List<SelectListItem> _GrammarPointList = new List<SelectListItem>() { new SelectListItem { Value = "0", Text ="Choose Phases" },
        new SelectListItem{Value = "1", Text ="All Phases" },new SelectListItem{ Value = "2", Text ="Assoc. & Act." },new SelectListItem{ Value = "3",Text ="Assoc. & Pass" },new SelectListItem {Value="4",Text="Pass & Act."  } };
    }
}