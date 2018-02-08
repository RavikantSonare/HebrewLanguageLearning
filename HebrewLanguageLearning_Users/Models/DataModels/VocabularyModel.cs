using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebrewLanguageLearning_Users.Models.DataModels
{
  public class VocabularyModel
    {
        //VocabDecksId
        public string VocabularyId { get; set; }
        public string LessonId { get; set; }
        public string DictionaryEntriesId { get; set; }
        public string Description { get; set; }
        public bool ActiveStatus { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
