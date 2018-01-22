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
                    string _obj_lemma, _chapter_osisID; int verseNo = 1;
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


                }
                finally
                {
                    GC.SuppressFinalize(this);
                }
            }
        }

        private HLLDBContext db = new HLLDBContext();
        HLL_DictionaryModel DataModelDictionary;
        public List<HLL_DictionaryModel> GetDBToXMl()
        {
            var DictionaryObj = db.HLL_DictionaryEntries.ToList();
            List<HLL_DictionaryModel> DataModelDictionary = new List<HLL_DictionaryModel>();

            try
            {
                if (DictionaryObj != null)
                {
                    AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_DictionaryEntries, HLL_DictionaryModel>(); });
                    DataModelDictionary = AutoMapper.Mapper.Map<List<HLL_DictionaryEntries>, List<HLL_DictionaryModel>>(DictionaryObj);
                }

                foreach (var Item in DataModelDictionary)
                {

                    var _currentDef = db.HLL_Definition.Where(x => x.DicEntId.Equals(Item.DictionaryEntriesId)).ToList();
                    var _currentLLD = db.HLL_LanguageLearningDefinition.Where(x => x.DicEntId.Equals(Item.DictionaryEntriesId)).ToList();

                    Item.Count_Sound = db.HLL_Media_Sound.Where(x => x.MasterTableId.Equals(Item.DictionaryEntriesId)).Count();
                    Item.Count_LLD = _currentLLD.Count();
                    Item.Count_Definition = _currentDef.Count();

                    if (_currentDef != null)
                    {
                        var _curDefLst = _currentDef.Select(z => z.DefinitionId).ToList();
                        Item.Count_SemanticDomain = db.HLL_SemanticDomain.Where(x => _curDefLst.Contains(x.MasterTableId)).Count();
                        Item.Count_Pictures = db.HLL_Media_Pictures.Where(p => _curDefLst.Contains(p.MasterTableId)).Count();
                    }
                    if (_currentLLD != null)
                    {
                        var _curLLDLst = _currentLLD.Select(l => l.LanLernDefId).ToList();
                        Item.Count_Example = db.HLL_Example.Where(x => _curLLDLst.Contains(x.MasterTableId)).Count();
                        Item.Count_Pictures = Item.Count_Pictures + db.HLL_Media_Pictures.Where(p => _curLLDLst.Contains(p.MasterTableId)).Count();
                    }
                }

            }
            catch
            {

            }
            return DataModelDictionary;
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
