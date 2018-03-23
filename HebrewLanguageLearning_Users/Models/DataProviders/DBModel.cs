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
        public static SQLiteConnection _dbcon;
        static DBModel()
        {
            _dbcon = new SQLiteConnection("Data Source=database.biblingoContext");
            DBModel obj = new DBModel();
            if (!File.Exists("./database.biblingoContext"))
            {
                SQLiteConnection.CreateFile("database.biblingoContext");
            }
            obj.CreateTables();

        }
        public void CreateTables()
        {
            try
            {
                /* --------- Create HLL_AuthenticateUser And Insert Table --------- */
                #region AuthenticateUser

                var _TableString = @"CREATE TABLE if not exists HLL_AuthenticateUser(UserId INTEGER PRIMARY KEY ,
                [UserName] [nvarchar](200) NULL,
                [Password] [nvarchar](200) NULL,
                [EmailId] [nvarchar](200) NULL,
                [ActiveStatus] [bit] NULL,
	            [Date] [nvarchar](100) NULL,
	            [IsActive] [bit] NULL,
	            [IsDelete] [bit] NULL)";
                ExcecuteTheQuery(_TableString, "I");
                object _obj = SelectData("HLL_AuthenticateUser");
                DataTable _dt = _obj as DataTable;
                if (_dt.Rows.Count <= 0)
                {
                    InsertData("HLL_AuthenticateUser", "");
                }
                #endregion

                /* --------- Create HLL_VocabDecks Table --------- */
                #region VocabDecks


                _TableString = @"CREATE TABLE if not exists HLL_VocabDecks(  VocabDecksId INTEGER PRIMARY KEY,
                [LessonId] [nvarchar](50) NULL,
                [StrongNo] [nvarchar](250) NULL,
                [DictionaryEntriesId] [nvarchar](250) NULL,
	            [Description] [nvarchar](500) NULL,	
                [LessonDecks] [nvarchar](500) NULL,	
                [IsCustomeDecks] [bit] NULL,	
                [ActiveStatus] [bit] NULL,
	            [IsActive] [bit] NULL,
	            [IsDelete] [bit] NULL,
                [IsReviewAssociation] [bit] NULL,
                [IsReviewPassive] [bit] NULL,
                [IsReviewGameA] [bit] NULL,
                [IsActiveKnowledge] [bit] NULL,
                [IsReviewGameB] [bit] NULL,
                [IsReviewAssociationGrammar] [bit] NULL,
                [IsPassiveKnowledgeGrammar] [bit] NULL,
                [IsActiveKnowledgeGrammar] [bit] NULL,
                [TheFinalActApplication] [bit] NULL)";
                ExcecuteTheQuery(_TableString, "I");
                #endregion

                /* --------- Create HLL_ProgressOfUser  Table --------- */
                #region ProgressOfUser

                _TableString = @"CREATE TABLE if not exists HLL_ProgressOfUser(ProgressId INTEGER PRIMARY KEY ,
                [completedLesson] [INTEGER] NULL,
                [completedPhases] [INTEGER] NULL,
                [completedParagraph] [INTEGER] NULL,
                [currentBookAndchapterId] [nvarchar](200) NULL,
                [currentScreenStatus] [INTEGER] NULL,
                [LeftDate] [nvarchar](200) NULL,
	            [IsActive] [bit] NULL,
	            [IsDelete] [bit] NULL)";
                ExcecuteTheQuery(_TableString, "I");
                _obj = SelectData("HLL_ProgressOfUser");
                _dt = _obj as DataTable;
                if (_dt.Rows.Count <= 0)
                {
                    InsertData("HLL_ProgressOfUser", "");
                }

                #endregion


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
                        customQuery.Append(@"INSERT OR REPLACE INTO HLL_VocabDecks(VocabDecksId,LessonId, StrongNo, DictionaryEntriesId, Description,
LessonDecks, IsCustomeDecks, ActiveStatus, IsActive, IsDelete, IsReviewAssociation, IsReviewPassive, IsReviewGameA, IsActiveKnowledge, IsReviewGameB, IsReviewAssociationGrammar, IsPassiveKnowledgeGrammar, IsActiveKnowledgeGrammar,TheFinalActApplication) 
values");
                        int tempValue = 1;
                        foreach (var item in _DictionaryModellist)
                        {
                            customQuery.Append("("+ tempValue +", '" + item.LessonId + "','" + item.StrongNo + "','" + item.DictionaryEntriesId + "', '"
    + item.Description + "','" + item.LessonDecks + "','" + item.IsCustomeDecks + "','" +
    item.ActiveStatus + "','" + item.IsActive + "','" + item.IsDelete + "','False','False','False','False','False','False','False','False','False'),");
                            tempValue++;
                        }
                        break;
                    case "HLL_AuthenticateUser":
                        customQuery.Append(@"insert into HLL_AuthenticateUser(UserName, Password, EmailId, ActiveStatus, Date, IsActive, IsDelete) 
values ('user','user@123','User@hll.com', 'True','" + DateTime.UtcNow + "','True','False')");
                        break;
                    case "HLL_ProgressOfUser":
                        customQuery.Append(@"insert into HLL_ProgressOfUser(completedLesson, completedPhases, completedParagraph, currentBookAndchapterId, currentScreenStatus, LeftDate, IsActive, IsDelete) 
values (0,0,1,'1Chr,1Chr.1',1,'" + DateTime.UtcNow + "','True','False')");

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
LessonDecks,IsCustomeDecks, ActiveStatus, IsActive,IsDelete, IsReviewAssociation, IsReviewPassive from HLL_VocabDecks where IsCustomeDecks='False'");
                        break;
                    case "HLL_VocabDecks_Custom":

                        customQuery.Append(@"Select VocabDecksId, LessonId, StrongNo, DictionaryEntriesId, Description,
LessonDecks,IsCustomeDecks, ActiveStatus, IsActive,IsDelete, IsReviewAssociation, IsReviewPassive from HLL_VocabDecks where IsCustomeDecks='True'");
                        break;
                    case "HLL_VocabDecks_IsReviewAssociationCount":

                        customQuery.Append(@"Select count() from HLL_VocabDecks where IsReviewAssociation='True' and LessonId='" + dataFilter + "'");
                        break;
                    case "HLL_VocabDecksLesson":
                        // IsReview = 'False' and
                        customQuery.Append(@"Select VocabDecksId, LessonId, StrongNo, DictionaryEntriesId, Description,
LessonDecks,IsCustomeDecks, ActiveStatus, IsActive,IsDelete, IsReviewAssociation, IsReviewPassive, IsReviewGameA, IsActiveKnowledge, IsReviewGameB, IsReviewAssociationGrammar, IsPassiveKnowledgeGrammar, IsActiveKnowledgeGrammar,TheFinalActApplication from HLL_VocabDecks where LessonId='" + dataFilter + "'");
                        break;

                    case "HLL_AuthenticateUser":
                        // IsReview = 'False' and
                        customQuery.Append(@"Select UserName, Password FROM HLL_AuthenticateUser ");
                        break;
                    case "HLL_ProgressOfUser":
                        customQuery.Append(@"Select completedLesson, completedPhases, completedParagraph, currentScreenStatus, currentBookAndchapterId, LeftDate FROM HLL_ProgressOfUser ");
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
                    case "HLL_VocabDecksIsReviewAssociation":
                        customQuery.Append(@"update HLL_VocabDecks set IsReviewAssociation='True' where StrongNo='" + tableData.Trim() + "'");
                        break;
                    case "HLL_VocabDecksIsReviewPassive":
                        customQuery.Append(@"update HLL_VocabDecks set IsReviewPassive='True' where StrongNo='" + tableData.Trim() + "'");
                        break;
                    case "HLL_ProgressOfUser":
                        customQuery.Append(@"update HLL_ProgressOfUser set completedLesson=" + tableData.Trim() + "");
                        break;
                    case "HLL_ProgressOfUserChapter":
                        customQuery.Append(@"update HLL_ProgressOfUser set currentBookAndchapterId='" + tableData.Trim() + "'");
                        break;
                    case "HLL_ProgressOfUsercurrentScreenStatus":
                        customQuery.Append(@"update HLL_ProgressOfUser set currentScreenStatus='" + tableData.Trim() + "'");
                        break;
                    case "HLL_ProgressOfUserIsActiveKnowledge":
                        customQuery.Append(@"update HLL_VocabDecks set IsActiveKnowledge='True' where StrongNo='" + tableData.Trim() + "'");
                        break;
                    case "HLL_VocabDecksIsReviewGameA":
                        customQuery.Append(@"update HLL_VocabDecks set IsReviewGameA='True' where StrongNo='" + tableData.Trim() + "'");
                        break;
                    case "HLL_VocabDecksIsReviewGameB":
                        customQuery.Append(@"update HLL_VocabDecks set IsReviewGameB='True' where StrongNo='" + tableData.Trim() + "'");
                        break;
                    case "HLL_ProgressOfUserIsReviewAssociationGrammar":
                        customQuery.Append(@"update HLL_VocabDecks set IsReviewAssociationGrammar='True' where StrongNo='" + tableData.Trim() + "'");
                        break;
                    case "HLL_VocabDecksIsPassiveKnowledgeGrammar":
                        customQuery.Append(@"update HLL_VocabDecks set IsPassiveKnowledgeGrammar='True' where StrongNo='" + tableData.Trim() + "'");
                        break;
                    case "HLL_VocabDecksIsActiveKnowledgeGrammar":
                        customQuery.Append(@"update HLL_VocabDecks set IsActiveKnowledgeGrammar='True' where StrongNo='" + tableData.Trim() + "'");
                        break;
                    case "HLL_VocabDecksTheFinalActApplication":
                        customQuery.Append(@"update HLL_VocabDecks set TheFinalActApplication='True' where StrongNo='" + tableData.Trim() + "'");
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

