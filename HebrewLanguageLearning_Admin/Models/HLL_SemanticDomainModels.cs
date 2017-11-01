using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HebrewLanguageLearning_Admin.Models
{
    public class HLL_SemanticDomainModels : HLL_CommonField
    {
        public string SemanticDomainId { get; set; }
        public string MasterTableId { get; set; }
        public string Title { get; set; }
        public string DicStrongNo { get; set; }
        public string DicEnglish { get; set; }
        public string DicHebrew { get; set; }
    }
}