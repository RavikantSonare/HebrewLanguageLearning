using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HebrewLanguageLearning_Admin.Models
{
    public class HLL_Media_SoundModels : HLL_CommonField
    {
        public string SoundId { get; set; }
        public string DicEntId { get; set; }
        [Required]
        public string Title { get; set; }
        public string AudioUrl { get; set; }
        public HttpPostedFileBase Soundfile { get; set; }
    }
}