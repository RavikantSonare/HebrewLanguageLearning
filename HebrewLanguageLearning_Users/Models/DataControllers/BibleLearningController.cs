using HebrewLanguageLearning_Users.Models.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HebrewLanguageLearning_Users.GenericClasses;
using System.Xml.XPath;
using HebrewLanguageLearning_Users.Models.DataProviders;
using System.Data;

namespace HebrewLanguageLearning_Users.Models.DataControllers
{
    public class BibleLearningController
    {
        BibleLearningModel _objBLM = new BibleLearningModel();
        DBModel obj = new DBModel();
        DataTable dt = new DataTable();
        internal BibleLearningModel ShowBookData(string CurrentfileName)
        {
            return GetBookData(CurrentfileName);
        }
        internal static string fileRootPath = System.AppDomain.CurrentDomain.BaseDirectory + "Resource\\Book\\wlc\\";


        internal BibleLearningModel GetBookData(string CurrentfileName)
        {

            try
            {

                if (CurrentfileName != null)
                {
                    ObjectToXML(CurrentfileName);
                }
            }
            catch (Exception ex)
            {
                FileSetter.LogError(ex);
            }
            return _objBLM;
        }

        public void ObjectToXML(string CurrentfileName)
        {
            try
            {
                _objBLM = new BibleLearningModel();
                string _obj_lemma, _chapter_osisID, fileName = fileRootPath + CurrentfileName + ".xml"; int verseNo = 1;
                XPathDocument xmlPathDoc = new XPathDocument(fileName);
                XPathNavigator xPathNav = xmlPathDoc.CreateNavigator();
                xPathNav.MoveToRoot(); xPathNav.MoveToFirstChild(); xPathNav.MoveToFirstChild();
                xPathNav.MoveToFirstChild(); xPathNav.MoveToNext(); xPathNav.MoveToFirstChild();
                List<bookElements> _TempbookElementsList = new List<bookElements>();
                List<verseElement> _tmpVerseElementList = new List<verseElement>();
                do
                {
                    _chapter_osisID = xPathNav.GetAttribute("osisID", "");
                    _objBLM._ChapterList.Add(new SelectListItem { Text = _chapter_osisID, Value = _chapter_osisID });

                    xPathNav.MoveToFirstChild();
                    do
                    {
                        _objBLM._VerseList.Add(
                            new SelectListItem
                            {
                                Text = xPathNav.GetAttribute("osisID", ""),
                                Value = _chapter_osisID

                            });
                        xPathNav.MoveToFirstChild();
                        do
                        {
                            _obj_lemma = xPathNav.GetAttribute("lemma", "");
                            if (!string.IsNullOrEmpty(_obj_lemma))
                            {
                                if (xPathNav.MoveToFirstChild())
                                {
                                    //  _objBLM._bookElementsList.Add(new DataModels.bookElements { ElementStrogNo = _obj_lemma, ElementValue = xPathNav.Value, SelctedElementValue = _objBLM._VerseList.Count() == 20 ? true : false });
                                    _TempbookElementsList.Add(new DataModels.bookElements { ElementStrogNo = _obj_lemma, ElementValue = xPathNav.Value, SelctedElementValue = _objBLM._VerseList.Count() == 20 ? true : false, currentBookAndchapterId = CurrentfileName + "," + _chapter_osisID + "," + _objBLM._VerseList.LastOrDefault().Text });
                                    xPathNav.MoveToParent();
                                }
                            }
                        } while (xPathNav.MoveToNext());


                        _tmpVerseElementList.Add(new verseElement { verseId = _objBLM._VerseList.LastOrDefault().Text, versValue = _objBLM._VerseList.LastOrDefault().Value, wElementList = _TempbookElementsList, verseNo = verseNo }); verseNo++;
                        _TempbookElementsList = new List<bookElements>();
                    } while (xPathNav.MoveToParent() && xPathNav.MoveToNext());

                    verseNo = 1;
                    _objBLM._chepterElementsList.Add(new chepterElements { ChepterId = _chapter_osisID, Chepterval = _chapter_osisID, verseElementList = _tmpVerseElementList });
                    _tmpVerseElementList = new List<verseElement>();

                } while (xPathNav.MoveToParent() && xPathNav.MoveToNext());
            }
            catch (Exception ex)
            {
                FileSetter.LogError(ex);

            }
            finally
            {
                GC.SuppressFinalize(this);
            }
        }

        public List<VocabDecksGroupModel> tmpVocabDecks;
        public List<VocabDecksGroupModel> GetVocabDesks(string TableName)
        {
            tmpVocabDecks = new List<VocabDecksGroupModel>();
            DBModel obj = new DBModel();
            DataTable dt = new DataTable();
            dt = (DataTable)obj.SelectData(TableName);
            List<VocabularyModel> tmplist = new List<VocabularyModel>();

            foreach (DataRow row in dt.Rows)
            {
                tmplist.Add(new VocabularyModel { LessonId = row.ItemArray[1].ToString(), LessonDecks = row.ItemArray[5].ToString(), IsReviewAssociation = Convert.ToBoolean(row.ItemArray[10].ToString()), IsReviewPassive = Convert.ToBoolean(row.ItemArray[11].ToString()) });

            }

            var tmp = tmplist.GroupBy(x => x.LessonId).
                Select(y => new
                {
                    LessonId = y.Key,
                    LessonDecks = y.Select(x => x.LessonDecks).ToList()
                }).ToList();
            object objTemp;
            foreach (var item in tmp)
            {

                List<VocabularyModel> vocabularyModeltmp = new List<VocabularyModel>();
                objTemp = item.LessonDecks;

                foreach (var itemData in item.LessonDecks)
                {
                    vocabularyModeltmp.Add(new VocabularyModel { LessonDecks = itemData });
                }

                tmpVocabDecks.Add(new VocabDecksGroupModel { LessonId = TableName == "HLL_VocabDecks" ? "Lesson " + item.LessonId : "" + item.LessonId, vocabularyModel = vocabularyModeltmp });
            }

            return tmpVocabDecks;

        }

        public List<VocabDecksGroupModel> GetVocabDesksLessonData(string TableName, string LessonId)
        {
            try
            {
                tmpVocabDecks = new List<VocabDecksGroupModel>();
                DBModel obj = new DBModel();
                DataTable dt = new DataTable();
                dt = (DataTable)obj.SelectData(TableName, LessonId);
                List<VocabularyModel> tmplist = new List<VocabularyModel>();

                foreach (DataRow row in dt.Rows)
                {
                    tmplist.Add(new VocabularyModel
                    {
                        StrongNo = row.ItemArray[2].ToString(),
                        LessonId = row.ItemArray[1].ToString(),
                        LessonDecks = row.ItemArray[5].ToString(),
                        IsReviewAssociation = Convert.ToBoolean(row.ItemArray[10]),
                        IsReviewPassive = Convert.ToBoolean(row.ItemArray[11]),
                        IsReviewGameA = Convert.ToBoolean(row.ItemArray[12]),
                        IsActiveKnowledge = Convert.ToBoolean(row.ItemArray[13]),
                        IsReviewGameB = Convert.ToBoolean(row.ItemArray[14]),
                        IsReviewAssociationGrammar = Convert.ToBoolean(row.ItemArray[15]),
                        IsPassiveKnowledgeGrammar = Convert.ToBoolean(row.ItemArray[16]),
                        IsActiveKnowledgeGrammar = Convert.ToBoolean(row.ItemArray[17]),
                    });
                }

                var tmp = tmplist.GroupBy(x => x.LessonId).
                    Select(y => new
                    {
                        LessonId = y.Key,
                        LessonDecks = y.Select(x => new { x.LessonDecks, x.StrongNo, x.IsReviewAssociation, x.IsReviewPassive, x.IsReviewGameA, x.IsActiveKnowledge, x.IsReviewGameB, x.IsReviewAssociationGrammar, x.IsPassiveKnowledgeGrammar, x.IsActiveKnowledgeGrammar }).ToList()
                    }).ToList();
                object objTemp;
                foreach (var item in tmp)
                {

                    List<VocabularyModel> vocabularyModeltmp = new List<VocabularyModel>();
                    objTemp = item.LessonDecks;

                    foreach (var itemData in item.LessonDecks)
                    {
                        vocabularyModeltmp.Add(new VocabularyModel { LessonDecks = itemData.LessonDecks, StrongNo = itemData.StrongNo, IsReviewAssociation = itemData.IsReviewAssociation, IsReviewPassive = itemData.IsReviewPassive,
                            IsReviewGameA = itemData.IsReviewGameA, IsActiveKnowledge = itemData.IsActiveKnowledge, IsReviewGameB = itemData.IsReviewGameB,
                            IsReviewAssociationGrammar = itemData.IsReviewAssociationGrammar,
                            IsPassiveKnowledgeGrammar = itemData.IsPassiveKnowledgeGrammar,
                            IsActiveKnowledgeGrammar = itemData.IsActiveKnowledgeGrammar
                        });
                    }
                    tmpVocabDecks.Add(new VocabDecksGroupModel { LessonId = TableName == "HLL_VocabDecks" ? "Lesson " : "" + item.LessonId, vocabularyModel = vocabularyModeltmp });
                }
            }
            catch { }
            return tmpVocabDecks;

        }

        // Insert Data 
        public bool SetVocabDesks(VocabularyModel _obj)
        {
            var listOfTable = "[" + JToken.FromObject(_obj).ToString() + "]";
            obj.InsertData("HLL_VocabDecks", listOfTable);
            return true;

        }

        // Update Data
        public void UpdateReviewData(string TableName, string StrongNo)
        {
            DataTable dt = new DataTable();
            obj.UpdateData(TableName, StrongNo);

        }

        internal void DeleteLesson(string vocabDocLessonId)
        {
            obj.DeleteLesson("HLL_VocabDecksDeleteLesson", vocabDocLessonId);
        }

        public int getIsReviewAssociationCount(string LessonId)
        {
            DashboardModel _objModel = new DashboardModel();
            try
            {
                {
                    object _obj = obj.SelectData("HLL_VocabDecks_IsReviewAssociationCount", LessonId);
                    DataTable _dt = _obj as DataTable;
                    foreach (DataRow row in _dt.Rows)
                    {
                        return Convert.ToInt32(row.ItemArray[0].ToString());
                    }
                }
            }
            catch
            {

            }
            return 0;

        }

        public int setProgressOfUserChapter(string BookAndChapter)
        {
            try
            {
                obj.UpdateData("HLL_ProgressOfUserChapter", BookAndChapter);
            }
            catch { }
            return 0;
        }
    }



}

