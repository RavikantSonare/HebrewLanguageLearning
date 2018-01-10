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
using HebrewLanguageLearning_Users.GenericClasses;
using System.Xml.XPath;



namespace HebrewLanguageLearning_Users.Models.DataControllers
{
    public class BibleLearningController
    {
        BibleLearningModel _objBLM = new BibleLearningModel();
        internal BibleLearningModel ShowBookData(string CurrentfileName)
        {
            return GetBookData(CurrentfileName);
        }
        internal static string fileRootPath = System.AppDomain.CurrentDomain.BaseDirectory + "Resource\\Book\\wlc\\";


        internal BibleLearningModel GetBookData(string CurrentfileName)
        {
            ObjectToXML(CurrentfileName);
            BibleLearningModel _examqueanslist = new BibleLearningModel();
            try
            {
                CurrentfileName = "Ok";
                if (CurrentfileName != null)
                {
                    string fileName = fileRootPath + CurrentfileName + ".xml";
                    XDocument document = XDocument.Load(fileName);
                }
            }
            catch (Exception ex)
            {

            }
            return _objBLM;
        }

        public void ObjectToXML(string CurrentfileName)
        {
            try
            {
                _objBLM = new BibleLearningModel();
                string _obj_lemma, _chapter_osisID, fileName = fileRootPath + CurrentfileName + ".xml";
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
                                    _TempbookElementsList.Add(new DataModels.bookElements { ElementStrogNo = _obj_lemma, ElementValue = xPathNav.Value, SelctedElementValue = _objBLM._VerseList.Count() == 20 ? true : false });
                                    xPathNav.MoveToParent();
                                }
                            }
                        } while (xPathNav.MoveToNext());

                   
                        _tmpVerseElementList.Add(new verseElement { verseId = _objBLM._VerseList.LastOrDefault().Text, versValue = _objBLM._VerseList.LastOrDefault().Value, wElementList = _TempbookElementsList });
                        _TempbookElementsList = new List<bookElements>();
                    } while (xPathNav.MoveToParent() && xPathNav.MoveToNext());


                    _objBLM._chepterElementsList.Add(new chepterElements { ChepterId = _chapter_osisID, Chepterval = _chapter_osisID, verseElementList = _tmpVerseElementList });
                    _tmpVerseElementList=new List<verseElement>();
                   
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
}

