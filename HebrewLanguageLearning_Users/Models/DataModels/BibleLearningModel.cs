using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebrewLanguageLearning_Users.Models.DataModels
{
    public class BibleLearningModel
    {

        public List<ChapterList> _chapterList { get; set; }
        public string C1 { get; set; }
        public string C2 { get; set; }
        public string C3 { get; set; }
        //public string MyProperty { get; set; }
        //public string MyProperty { get; set; }
        //public string MyProperty { get; set; }
        //public string MyProperty { get; set; }
        //public string MyProperty { get; set; }
        //public string MyProperty { get; set; }
        //public string MyProperty { get; set; }
        //public string MyProperty { get; set; }
        //public string MyProperty { get; set; }
        //public string MyProperty { get; set; }
        //public string MyProperty { get; set; }
    }
    public class ChapterList
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class VerseList
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}