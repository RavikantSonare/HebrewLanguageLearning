using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HebrewLanguageLearning_Admin.Models
{
    public class HLL_GrammarModel : HLL_CommonField
    {
        public string GrammarId { get; set; }
        [Required(ErrorMessage = "Please Enter English Grammer.")]
        public string EnglishGrammar { get; set; }
        [Required(ErrorMessage = "Please Enter Hebrew Grammer.")]
        public string HebrewGrammar { get; set; }
        public string Description { get; set; }

        public string Sentence { get; set; }
        [Required(ErrorMessage = "Please Enter at least One Application.")]
        public string[] AppSentenceDynamicTextBox { get; set; }

        public HttpPostedFileBase[] ImgVdofile { get; set; }
        public HttpPostedFileBase[] Imgfile { get; set; }
        public HttpPostedFileBase[] Videofile { get; set; }


        public string ImgVdofiles { get; set; }
        public string Soundfiles { get; set; }

        public string[] ImgUrl { get; set; }
        public string[] VideoUrl { get; set; }


        public HttpPostedFileBase[] Soundfile { get; set; }
        public string[] SoundUrl { get; set; }
        public string ExercisesNumber { get; set; }
    }
}