using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.XPath;
using System.IO;
using HebrewLanguageLearning_Admin.DBContext;
using HebrewLanguageLearning_Admin.Models;
using System.Reflection;
using Newtonsoft.Json;

namespace HebrewLanguageLearning_Admin.GenericClasses
{
    public class XmlSetter
    {
        public async Task SetXmlToDB(string filePath)
        {
            if (filePath != null)
            {
                try
                {
                    //   string _obj_lemma, _chapter_osisID; int verseNo = 1;
                    XPathDocument xmlPathDoc = new XPathDocument(filePath);
                    XPathNavigator xPathNav = xmlPathDoc.CreateNavigator();
                    xPathNav.MoveToRoot(); xPathNav.MoveToFirstChild(); xPathNav.MoveToFirstChild();
                    xPathNav.MoveToNext();
                    xPathNav.MoveToFirstChild(); xPathNav.MoveToNext(); xPathNav.MoveToFirstChild();
                    // List<bookElements> _TempbookElementsList = new List<bookElements>();
                    // List<verseElement> _tmpVerseElementList = new List<verseElement>();
                    do
                    {
                        //_chapter_osisID = xPathNav.GetAttribute("osisID", "");
                        //_objBLM._ChapterList.Add(new SelectListItem { Text = _chapter_osisID, Value = _chapter_osisID });

                        //xPathNav.MoveToFirstChild();
                        //do
                        //{
                        //    _objBLM._VerseList.Add(
                        //        new SelectListItem
                        //        {
                        //            Text = xPathNav.GetAttribute("osisID", ""),
                        //            Value = _chapter_osisID

                        //        });
                        //    xPathNav.MoveToFirstChild();
                        //    do
                        //    {
                        //        _obj_lemma = xPathNav.GetAttribute("lemma", "");
                        //        if (!string.IsNullOrEmpty(_obj_lemma))
                        //        {
                        //            if (xPathNav.MoveToFirstChild())
                        //            {
                        //                //  _objBLM._bookElementsList.Add(new DataModels.bookElements { ElementStrogNo = _obj_lemma, ElementValue = xPathNav.Value, SelctedElementValue = _objBLM._VerseList.Count() == 20 ? true : false });
                        //                _TempbookElementsList.Add(new DataModels.bookElements { ElementStrogNo = _obj_lemma, ElementValue = xPathNav.Value, SelctedElementValue = _objBLM._VerseList.Count() == 20 ? true : false });
                        //                xPathNav.MoveToParent();
                        //            }
                        //        }
                        //    } while (xPathNav.MoveToNext());


                        //    _tmpVerseElementList.Add(new verseElement { verseId = _objBLM._VerseList.LastOrDefault().Text, versValue = _objBLM._VerseList.LastOrDefault().Value, wElementList = _TempbookElementsList, verseNo = verseNo }); verseNo++;
                        //    _TempbookElementsList = new List<bookElements>();
                        //} while (xPathNav.MoveToParent() && xPathNav.MoveToNext());

                        //verseNo = 1;
                        //_objBLM._chepterElementsList.Add(new chepterElements { ChepterId = _chapter_osisID, Chepterval = _chapter_osisID, verseElementList = _tmpVerseElementList });
                        //_tmpVerseElementList = new List<verseElement>();

                    } while (xPathNav.MoveToParent() && xPathNav.MoveToNext());
                }
                catch (Exception ex)
                {
                    //FileSetter.LogError(ex);

                }

                finally
                {
                    GC.SuppressFinalize(this);
                }
            }
        }
        public static void LogError(string ex)
        {
            string message = string.Format("Time: {0}", DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex);
            message += Environment.NewLine;
            //message += string.Format("StackTrace: {0}", ex.StackTrace);
            //message += Environment.NewLine;
            //message += string.Format("Source: {0}", ex.Source);
            //message += Environment.NewLine;
            //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            //message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            string path = System.Web.HttpContext.Current.Server.MapPath("~/ErrorLog.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }

        private HLLDBContext db = new HLLDBContext();
        HLL_DictionaryModel DataModelDictionary;
        public List<DictionaryModel> GetDBToXMl()
        {
            // Select(x => new { x.DicEnglish, x.DicHebrew, x.DicStrongNo, x.DictionaryEntriesId }).
            List<HLL_DictionaryEntries> DictionaryObj = db.HLL_DictionaryEntries.ToList();
            List<DictionaryModel> DataModelDictionary = new List<DictionaryModel>();
            // for Sound
            List<HLL_Media_Sound> _Media_SoundList = db.HLL_Media_Sound.ToList();


            try
            {
                if (DictionaryObj != null)
                {
                    AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_DictionaryEntries, DictionaryModel>(); });
                    DataModelDictionary = AutoMapper.Mapper.Map<List<HLL_DictionaryEntries>, List<DictionaryModel>>(DictionaryObj);
                }

                foreach (var Item in DataModelDictionary)
                {

                    var _currentDef = db.HLL_Definition.Where(x => x.DicEntId.Equals(Item.DictionaryEntriesId)).ToList();
                    var _currentLLD = db.HLL_LanguageLearningDefinition.Where(x => x.DicEntId.Equals(Item.DictionaryEntriesId)).ToList();

                    Item.Count_LLD = _currentLLD.Count();
                    Item.Count_Definition = _currentDef.Count();

                    if (_currentDef != null && _currentDef.Count() > 0)
                    {
                        var _curDefLst = _currentDef.Select(z => z.DefinitionId).ToList();
                        Item.ListOfSemanticDomain = db.HLL_SemanticDomain.Where(x => _curDefLst.Contains(x.MasterTableId)).Select(z => z.Title).ToList();
                        Item.ListOfPictures = db.HLL_Media_Pictures.Where(p => _curDefLst.Contains(p.MasterTableId)).Select(z => z.ImgUrl).ToList();
                        Item.ListOfDefinition = _currentDef.Select(z => z.Title).ToList();
                        Item.ListOfExample = db.HLL_Example.Where(p => _curDefLst.Contains(p.MasterTableId)).Select(z => z.Title).ToList();
                        Item.ListOfDictionaryReference = db.HLL_DictionaryReference.Where(p => _curDefLst.Contains(p.MasterTableId)).Select(z => z.Title).ToList();

                        var chkSound = _Media_SoundList.FirstOrDefault(x => x.MasterTableId.Equals(Item.DictionaryEntriesId));
                        if (chkSound != null)
                        {
                            Item.SoundUrl = chkSound.AudioUrl;
                        }
                    }
                    if (_currentLLD != null && _currentLLD.Count() > 0)
                    {
                        var _curLLDLst = _currentLLD.Select(l => l.LanLernDefId).ToList();
                        Item.Count_Example = db.HLL_Example.Where(x => _curLLDLst.Contains(x.MasterTableId)).Count();
                        Item.Count_Pictures = Item.Count_Pictures + db.HLL_Media_Pictures.Where(p => _curLLDLst.Contains(p.MasterTableId)).Count();
                        // Item.ListOfSemanticDomain = db.HLL_DictionaryReference.Where(p => _curDefLst.Contains(p.MasterTableId)).Count();

                    }
                    Item.ListOfDefinition = db.HLL_Definition.Where(z => z.DicEntId == Item.DictionaryEntriesId).Select(z => z.Title).ToList();
                }

            }
            catch
            {

            }
            return DataModelDictionary;
        }
        private List<HLL_DictionaryEntries> VocabularyInLessonList = new List<HLL_DictionaryEntries>();
        private List<HLL_Vocabulary> _ObjHLL_Vocabulary = new List<HLL_Vocabulary>();
        public object GetDBTable(string _tblname)
        {
            object _objlist = new object();
            try
            {

                if (_tblname == "HLL_Vocabulary")
                {

                    _ObjHLL_Vocabulary = db.HLL_Vocabulary.Where(x => x.IsDelete == false).ToList();
                    var objVocabularyList = _ObjHLL_Vocabulary.Select(z => new { z.DictionaryEntriesId, z.LessonId }).ToList();
                    var VocabularyInLessonListNew = objVocabularyList.Join(db.HLL_DictionaryEntries, s => s.DictionaryEntriesId, f => f.DictionaryEntriesId, (s, f) => new { s, f }).ToList();
                    var InLessonList = VocabularyInLessonListNew.Select(x =>
                                         new
                                         {
                                             LessonId = x.s.LessonId,
                                             StrongNo = x.f.DicStrongNo,
                                             DictionaryEntriesId = x.f.DictionaryEntriesId,
                                             LessonDecks = x.f.DicStrongNo + ", " + x.f.DicHebrew + ", " + x.f.DicEnglish,
                                             ActiveStatus = x.f.ActiveStatus,
                                             IsActive = x.f.IsActive,
                                             IsDelete = x.f.IsDelete
                                         }

                                         ).ToList();

                    _objlist = InLessonList;

                }

            }

            finally { }
            return _objlist;
        }
    }
}

public class XmlClass
{
    public string letHead { get; set; }
    public List<Entry> entryList { get; set; }
}
public class Entry
{
    public int id { get; set; }
    public string mainheadword { get; set; }
    public int mainheadwordno { get; set; }
    public List<Senses> sensesList { get; set; }

}
public class Senses
{
    public string sharedgrammaticalinfo { get; set; }
    public List<Sensecontent> sensecontentList { get; set; }
}
public class Sensecontent
{
    public int sensenumber { get; set; }
    public string definitionorgloss { get; set; }
    public List<Semanticdomains> semanticdomainsList { get; set; }
    public List<Examplescontents> examplescontentsList { get; set; }
}
public class Semanticdomains
{
    public string abbreviation { get; set; }
    public string name { get; set; }
}
public class Examplescontents
{
    public string example { get; set; }
    public string reference { get; set; }
}
