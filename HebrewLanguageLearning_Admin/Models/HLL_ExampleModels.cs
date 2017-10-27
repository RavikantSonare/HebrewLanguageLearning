using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HebrewLanguageLearning_Admin.Models
{
    public class HLL_ExampleModels : HLL_CommonField
    {
        public string ExampleId { get; set; }
        public string MasterTableId { get; set; }
        public string Title { get; set; }
    }
}