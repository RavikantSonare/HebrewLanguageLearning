using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HebrewLanguageLearning_Admin.Models
{
    public class HLL_HebrewGrammarDataModel : HLL_CommonField
    {
        public string HebrewGrammarDataId { get; set; }
        public string MasterTableId { get; set; }
        public string HebrewGrammarData { get; set; }
        public string HebrewGrammarDataNo { get; set; }
        public string ImgVdofile { get; set; }
        public string Soundfile { get; set; }
        public string CorrectAnswers { get; set; }
        public string LessonId { get; set; }
    }
}