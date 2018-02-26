using HebrewLanguageLearning_Users.Models.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebrewLanguageLearning_Users.Models.DataProviders
{
    class DBModel
    {
        public SQLiteConnection _dbcon;
        public DBModel()
        {
            _dbcon = new SQLiteConnection("Data Source=database.biblingoContext");
            if (!File.Exists("./database.biblingoContext"))
            {
                SQLiteConnection.CreateFile("database.biblingoContext");

            }
            CreateTables();
        }
        public void CreateTables()
        {
            try
            {
                var _TableString = @"CREATE TABLE if not exists HLL_VocabDecks(  VocabDecksId int IDENTITY(1,1) PRIMARY KEY,
                [LessonId] [nvarchar](50) NULL,
                [StrongNo] [nvarchar](250) NULL,
                [DictionaryEntriesId] [nvarchar](250) NULL,
	            [Description] [nvarchar](500) NULL,	
                [LessonDecks] [nvarchar](500) NULL,	
                [IsCustomeDecks] [bit] NULL,	
                [ActiveStatus] [bit] NULL,
	            [IsActive] [bit] NULL,
	            [IsDelete] [bit] NULL,
                [IsReview] [bit] NULL)";
                using (SQLiteCommand command = new SQLiteCommand(_TableString, _dbcon))
                {
                    OpenConnection();
                    command.ExecuteNonQuery();
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

        }
        public List<string> _tblList = new List<string>() { "HLL_VocabDecks", "HLL_Configuration", "HLL_Progress" };


        void OpenConnection()
        {
            if (_dbcon.State != System.Data.ConnectionState.Open)
            {
                _dbcon.Open();
            }

        }
        void CloseConnection()
        {
            if (_dbcon.State != System.Data.ConnectionState.Closed)
            {
                _dbcon.Close();
            }


        }

        /// Insert Table Data
        /// 

        internal void InsertData(string tableName, string tableData)
        {
            try
            {
                StringBuilder customQuery = new StringBuilder();
                switch (tableName)
                {
                    case "HLL_VocabDecks":

                        var _DictionaryModellist = JsonConvert.DeserializeObject<List<VocabularyModel>>(tableData);
                        customQuery.Append(@"insert into HLL_VocabDecks(LessonId, StrongNo, DictionaryEntriesId, 
Description,
LessonDecks, IsCustomeDecks, ActiveStatus, IsActive, IsDelete, IsReview) 
values");
                        foreach (var item in _DictionaryModellist)
                        {
                            customQuery.Append("('" + item.LessonId + "','" + item.StrongNo + "','" + item.DictionaryEntriesId + "', '"
    + item.Description + "','" + item.LessonDecks + "','" + item.IsCustomeDecks + "','" +
    item.ActiveStatus + "','" + item.IsActive + "','" + item.IsDelete + "','False'),");
                        }
                        break;


                }
                string Qry = Convert.ToString(customQuery);

                Qry = Qry.TrimEnd(',');
                var result = ExcecuteTheQuery(Qry, "I");

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        ///  Selete Data
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataFilter"></param>
        /// <returns></returns>
        internal object SelectData(string tableName, string dataFilter = "")
        {
            object result = new object();
            try
            {
                StringBuilder customQuery = new StringBuilder();
                switch (tableName)
                {
                    case "HLL_VocabDecks":

                        customQuery.Append(@"Select VocabDecksId, LessonId, StrongNo, DictionaryEntriesId, Description,
LessonDecks,IsCustomeDecks, ActiveStatus, IsActive,IsDelete from HLL_VocabDecks where IsCustomeDecks='False'");
                        break;
                    case "HLL_VocabDecks_Custom":

                        customQuery.Append(@"Select VocabDecksId, LessonId, StrongNo, DictionaryEntriesId, Description,
LessonDecks,IsCustomeDecks, ActiveStatus, IsActive,IsDelete from HLL_VocabDecks where IsCustomeDecks='True'");
                        break;
                    case "HLL_VocabDecksLesson":
                       // IsReview = 'False' and
                        customQuery.Append(@"Select VocabDecksId, LessonId, StrongNo, DictionaryEntriesId, Description,
LessonDecks,IsCustomeDecks, ActiveStatus, IsActive,IsDelete, IsReview from HLL_VocabDecks where LessonId='" + dataFilter + "'");
                        break;

                }
                string Qry = Convert.ToString(customQuery);
                result = ExcecuteTheQuery(Qry, "S");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Update Data
        /// </summary>
        /// <param name="Qry"></param>
        /// <param name="OprationType"></param>
        /// <returns></returns>
        /// 

        internal void UpdateData(string tableName, string tableData)
        {
            try
            {
                StringBuilder customQuery = new StringBuilder();
                switch (tableName)
                {
                    case "HLL_VocabDecksIsReView":
                        customQuery.Append(@"update HLL_VocabDecks set IsReview='True' where StrongNo='" + tableData.Trim() + "'");
                        break;
                }
                string Qry = Convert.ToString(customQuery);
                
                var result = ExcecuteTheQuery(Qry, "U");

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        ///  DeleteLesson
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="tableData"></param>
        internal void DeleteLesson(string tableName, string tableData)
        {
            try
            {
                StringBuilder customQuery = new StringBuilder();
                switch (tableName)
                {
                    case "HLL_VocabDecksDeleteLesson":
                        customQuery.Append(@"delete from HLL_VocabDecks where LessonId='" + tableData.Trim() + "'");
                        break;
                }
                string Qry = Convert.ToString(customQuery);

                var result = ExcecuteTheQuery(Qry, "D");

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private object ExcecuteTheQuery(string Qry, string OprationType)
        {
            SQLiteDataAdapter sqlite_datareader;
            DataTable dt = new DataTable();

            object result = new object();
            using (SQLiteCommand command = new SQLiteCommand(Qry, _dbcon))
            {
                OpenConnection();

                switch (OprationType)
                {
                    case "I":
                        result = command.ExecuteNonQuery();
                        break;
                    case "S":
                        sqlite_datareader = new SQLiteDataAdapter(command);
                        sqlite_datareader.Fill(dt);
                        result = dt;
                        break;
                    case "U":
                        result = command.ExecuteNonQuery();
                        break;
                    case "D":
                        result = command.ExecuteNonQuery();
                        break;
                }
                CloseConnection();
            }
            return result;
        }
    }
}

