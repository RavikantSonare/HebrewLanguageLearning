using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebrewLanguageLearning_Users.Models.DataModels
{
    class DictionaryModel
    {
        public string DictionaryEntriesId { get; set; }
        public string DicStrongNo { get; set; }
        public string DicEnglish { get; set; }
        public string DicHebrew { get; set; }
        public string DicLanguageLearningDefinition { get; set; }
        public int Count_Pictures { get; set; }
        public int Count_Definition { get; set; }
        public int Count_Example { get; set; }
        public int Count_SemanticDomain { get; set; }
        public int Count_Dictionaries { get; set; }
        public int Count_LLD { get; set; }
        public int Count_Sound { get; set; }
        public string SoundTitle { get; set; }
        public string SoundUrl { get; set; }
        public List<string> ListOfDefinition { get; set; }
        public List<string> ListOfPictures { get; set; }
        public List<string> ListOfSemanticDomain { get; set; }
        public List<string> ListOfExample { get; set; }
        public List<string> ListOfDictionaryReference { get; set; }
    }
}
