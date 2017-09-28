using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HebrewLanguageLearning_Admin.Models
{
    public class HLL_LessonsGrammarModel : HLL_CommonField
    {
        public string LessonsGrammarId { get; set; }
        public string fkLessonId { get; set; }
        public string fkGrammarId { get; set; }
        public int GrammarPoint1 { get; set; }
        public int GrammarPoint2 { get; set; }
        public int GrammarPoint3 { get; set; }
        public int GrammarPoint4 { get; set; }
        public string Description { get; set; }
    }
}