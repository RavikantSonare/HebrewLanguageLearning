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
                [VocabularyId][nvarchar](250) NULL,
                [LessonId] [nvarchar](250) NULL,
                [DictionaryEntriesId] [nvarchar](250) NULL,
	            [Description] [nvarchar](500) NULL,	
                [LessonDecks] [nvarchar](500) NULL,	
                [CustomeDecks] [nvarchar](500) NULL,	
                [ActiveStatus] [bit] NULL,
	            [IsActive] [bit] NULL,
	            [IsDelete] [bit] NULL)";

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
                        customQuery.Append(@"insert into HLL_VocabDecks(VocabularyId, LessonId, DictionaryEntriesId,
Description,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate) 
values");
                        foreach (var item in _DictionaryModellist)
                        {
                            customQuery.Append("('" + item.VocabularyId + "','" + item.LessonId + "','" + item.DictionaryEntriesId + "', '"
    + item.Description + "','" + item.ActiveStatus + "','" + item.IsActive + "','" +
    item.IsDelete + "','" + item.CreatedBy + "','" + item.CreatedDate + "','" + item.UpdatedBy + "','" + item.UpdatedDate + "'),");
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
        internal object SelectData(string tableName)
        {
            object result = new object();
            try
            {
                StringBuilder customQuery = new StringBuilder();
                switch (tableName)
                {
                    case "HLL_VocabDecks":
                        //  var _DictionaryModellist = JsonConvert.DeserializeObject<List<VocabularyModel>>(tableData);
                        customQuery.Append(@"Select VocabDecksId, VocabularyId, LessonId, DictionaryEntriesId,
Description,ActiveStatus,IsActive,IsDelete from HLL_VocabDecks");
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
                        //while (sqlite_datareader.Read())
                        //{
                        //    //result += sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("VocabularyId"));
                        //    result = sqlite_datareader["VocabularyId"];
                        //}

                        break;
                    case "U":
                        result = command.ExecuteScalar();
                        break;
                    case "D":
                        result = command.ExecuteScalar();
                        break;
                }
                CloseConnection();
            }
            return result;
        }
    }
}

