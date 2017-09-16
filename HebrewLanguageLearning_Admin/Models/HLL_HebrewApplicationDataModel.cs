using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HebrewLanguageLearning_Admin.Models
{
    public class HLL_HebrewApplicationDataModel : HLL_CommonField
    {
        public string HebrewApplicationDataId { get; set; }
        public string MasterTableId { get; set; }
        public string HebrewApplicationData { get; set; }
        public string HebrewApplicationDataNo { get; set; }
        public string CorrectAnswer1 { get; set; }
        public string CorrectAnswer2 { get; set; }
        public string CorrectAnswer3 { get; set; }
        public string CorrectAnswer4 { get; set; }
        public string CorrectAnswer5 { get; set; }
        public string CorrectAnswer6 { get; set; }
        public string CorrectAnswer7 { get; set; }
        public string CorrectAnswer8 { get; set; }
        public string CorrectAnswer9 { get; set; }
        public string CorrectAnswer10 { get; set; }
        public string CorrectAnswerNo { get; set; }
        public string ImgVdofile { get; set; }
        public string Soundfile { get; set; }
        public string CorrectAnswers { get; set; }
        public string LessonId { get; set; }
        
    }
}