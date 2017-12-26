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

namespace HebrewLanguageLearning_Users.Models.DataControllers
{
    public class BibleLearningController
    {
        internal void ShowBookData(string CurrentfileName)
        {
            var DataGet = GetBookData(CurrentfileName);
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
            //return _examqueanslist;
            return null;
        }
        public List<ChapterList> _ChapterList = new List<ChapterList>();
        public List<VerseList> _VerseList = new List<VerseList>();

        public void ObjectToXML(string CurrentfileName)
        {
            XmlDocument doc = new XmlDocument();
            string fileName = fileRootPath + CurrentfileName + ".xml";

            //  string fileName = fileRootPath + "XMLFile1.xml";

            XElement xmlsp = XElement.Load(fileName);
            XNode DivNode = ((System.Xml.Linq.XContainer)xmlsp.LastNode).LastNode;
            //  XElement elementq = XElement.Load(fileName);
            
            doc.LoadXml(DivNode.ToString());
            XmlElement root = doc.DocumentElement;
            var ChildNodesList = root.ChildNodes;
            
            foreach (dynamic chapterChildNodesList in ChildNodesList)
            {
                XmlElement rootData = chapterChildNodesList; var AttrData = rootData.Attributes["osisID"].Value;
                _ChapterList.Add(new ChapterList { Value = AttrData, Name = AttrData });

                foreach (dynamic verseChildNodesList in rootData.ChildNodes)
                {
                    XmlElement verserootData = verseChildNodesList; var verseAttrData = verserootData.Attributes["osisID"].Value;
                    _VerseList.Add(new VerseList { Name = AttrData, Value = verseAttrData });

                }
                // var DAta = licenName;
            }

            #region Fetch All the Books 
            //var d1 = document.Root;
            //var d2 = document.Descendants("div");
            //var d3 = document.Descendants("w");
            //var d4 = document.Descendants("div");
            //// var books = document.Descendants("chapter").Select(p => new { osisID= p.Element("chapter").Attribute("osisID").Value }).ToList();
            //var books = document.Descendants("osis").Select(p => new { type = p.Element("osis").Attribute("xmlns").Value }).ToList();

            ////select new
            ////{
            ////    verse = r.Element("verse").Value,
            ////    //Title = r.Element("title").Value,
            ////    //Genere = r.Element("genre").Value,
            ////    //Price = r.Element("price").Value,
            ////    //PublishDate = r.Element("publish_date").Value,
            ////    //Description = r.Element("description").Value,

            ////};

            //foreach (var r in books)
            //{
            //    // Console.WriteLine(r.PublishDate + r.Title + r.Author);
            //}

            //Console.ReadKey(true);
            //#endregion

            //#region Fetching a particular  Book            
            //var selectedBook = from r in document.Descendants("book").Where
            //                       (r => (string)r.Attribute("id") == "bk102")
            //                   select new
            //                   {
            //                       Author = r.Element("author").Value,
            //                       Title = r.Element("title").Value,
            //                       Genere = r.Element("genre").Value,
            //                       Price = r.Element("price").Value,
            //                       PublishDate = r.Element("publish_date").Value,
            //                       Description = r.Element("description").Value,

            //                   };

            //foreach (var r in selectedBook)
            //{
            //    Console.WriteLine(r.PublishDate + r.Title + r.Author);
            //}
            //Console.ReadKey(true);
            //#endregion



            //#region Fetching a particular  Book


            //var selectedBookAttribute = (from r in document.Descendants("book").Where
            //                            (r => (string)r.Attribute("id") == "bk102")
            //                             select r.Element("author").Attribute("id").Value).FirstOrDefault();

            //Console.WriteLine(selectedBookAttribute);

            //Console.ReadKey(true);

            //#endregion


            //#region Fetching all Authors 


            //var allauthors = from r in document.Descendants("book")
            //                 select r.Element("author").Value;
            //foreach (var r in allauthors)
            //{
            //    Console.WriteLine(r.ToString());
            //}


            //Console.ReadKey(true);

            #endregion
        }


    }

}


//string s = root.Attributes["osisID"].Value;



//var ok = lstNode.ToString();
//var dt = XDocument.Parse(ok);

//XDocument doc = XDocument.Parse(ok);
//var strs = "";
//var DAta = licenName.Attributes["osisID"].Value;
//string Dra = ((System.Xml.XmlElement)chapterChildNodesList).OuterXml;
//XmlDocument doc6pQ = new XmlDocument();

//doc6pQ.LoadXml(Dra);

//                var ChildNodesList6 = doc6pQ.ChildNodes;

// XDocument document = new XDocument.Load("Resource\\Book\\wlc\\sk.xml");

//  string fileName = fileRootPath + "Ok.xml";
