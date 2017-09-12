using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HebrewLanguageLearning_Admin.Models
{
    public class HLL_Media_VideoModels : HLL_CommonField
    {
        public string VideoId { get; set; }
        public string MasterTableId { get; set; }
        [Required]
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public HttpPostedFileBase Videofile { get; set; }
        public string TableRef { get; set; }
    }
}