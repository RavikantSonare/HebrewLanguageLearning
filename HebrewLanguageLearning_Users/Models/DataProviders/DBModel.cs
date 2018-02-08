using HebrewLanguageLearning_Users.Models.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
                [ActiveStatus] [bit] NULL,
	            [IsActive] [bit] NULL,
	            [IsDelete] [bit] NULL,
	            [CreatedBy] [int] NULL,
	            [CreatedDate] [datetime] NULL,
	            [UpdatedBy][int] NULL,
	            [UpdatedDate] [datetime] NULL)";

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
            StringBuilder customQuery = new StringBuilder();
            switch (tableName)
            {
                case "HLL_VocabDecks":

                    var _DictionaryModellist = JsonConvert.DeserializeObject<List<VocabularyModel>>(tableData);
                    foreach (var item in _DictionaryModellist)
                    {
                        customQuery.Append(@"insert into HLL_VocabDecks(VocabularyId, LessonId, DictionaryEntriesId,
Description,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate) 
values('" + item.VocabularyId + "','" + item.LessonId + "','" + item.DictionaryEntriesId + "', '"
+ item.Description + "','" + item.ActiveStatus + "','" + item.IsActive + "','" +
item.IsDelete + "','" + item.CreatedBy + "','" + item.CreatedDate + "','" + item.UpdatedBy + "','" + item.UpdatedDate + "')");
                    }


                    break;
            }
            string Qry = Convert.ToString(customQuery);
            using (SQLiteCommand command = new SQLiteCommand(Qry, _dbcon))
            {
                OpenConnection();
                command.ExecuteNonQuery();
                CloseConnection();
            }

        }


    }
}
