using HebrewLanguageLearning_Users.GenericClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebrewLanguageLearning_Users.Models.DataModels
{
   public class VocabDecksGroupModel
    {
        public string LessonId { get; set; }
        public List<VocabularyModel> vocabularyModel { get; set; }
    }
    class BibileTextList
    {
        public string proBibleTxtHebrew { get; set; }
        public string proBibleTxtEnglish { get; set; }
        public string proMediaURl { get; set; } = EntityConfig.MediaUri;
        public string proDescriptionTxt { get; set; }
        public string proExampleTxt { get; set; }
        public string proSemanticDomainTxt { get; set; }
        public string proDictonaryReferenceTxt { get; set; }

    }
}
