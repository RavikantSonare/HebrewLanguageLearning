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
    }
    public class bookElements
    {
        public string ElementStrogNo { get; set; }
        public string ElementValue { get; set; }
        public bool SelctedElementValue { get; set; }
    }
  
}