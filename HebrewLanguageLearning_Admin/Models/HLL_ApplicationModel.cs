using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HebrewLanguageLearning_Admin.Models
{
    public class HLL_ApplicationModel: HLL_CommonField
    {
        public string ApplicationId { get; set; }
        [Required(ErrorMessage = "Please Select Lesson"), Range(1, 50,ErrorMessage = "Please Select Lesson")]
        public string LessonId { get; set; }
        public string Title { get; set; }

        public string Sentence { get; set; }
        [Required(ErrorMessage = "Please Enter at least One Application.")]
        public string[] AppSentenceDynamicTextBox { get; set; }

        public string ImgUrl { get; set; }
        public string VideoUrl { get; set; }

       
        public HttpPostedFileBase Soundfile { get; set; }
        public string SoundUrl { get; set; }
    }
}