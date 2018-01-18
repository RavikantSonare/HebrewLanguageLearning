using Caliburn.Micro;
using HebrewLanguageLearning_Users.GenericClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebrewLanguageLearning_Users.Models.DataModels
{
    public class BibleLearningModel
    {
        public List<SelectListItem> _ChapterList = new List<SelectListItem>();
        public List<SelectListItem> _VerseList = new List<SelectListItem>();
        public List<bookElements> _bookElementsList = new List<bookElements>();
        public List<chepterElements> _chepterElementsList = new List<chepterElements>();
    }
    public class chepterElements
    {
        public string ChepterId { get; set; }
        public string  Chepterval { get; set; }
        public List<verseElement> verseElementList { get; set; }
    }
    public class verseElement
    {
        public string verseId { get; set; }
        public int verseNo { get; set; }
        public string versValue { get; set; }
        public List<bookElements> wElementList { get; set; }
    }
    public class bookElements
    {
        public string ElementStrogNo { get; set; }
        public string ElementValue { get; set; }
        public bool SelctedElementValue { get; set; }
       
    }
  
}