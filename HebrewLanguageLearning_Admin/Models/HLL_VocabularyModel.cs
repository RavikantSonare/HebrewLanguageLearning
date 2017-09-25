using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HebrewLanguageLearning_Admin.Models
{
    public class HLL_VocabularyModel : HLL_CommonField
    {
        public string VocabularyId { get; set; }
        public string LessonId { get; set; }
        public string DictionaryEntriesId { get; set; }
        public string Description { get; set; }
        public string[] VocabularyInLesson { get; set; }
    }
}