using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HebrewLanguageLearning_Admin.DBContext;
using System.ComponentModel.DataAnnotations;

namespace HebrewLanguageLearning_Admin.Models
{
    public class HLL_DictionaryModel : HLL_CommonField
    {
        public string DictionaryEntriesId { get; set; }
        [Required(ErrorMessage = "Please Enter Strong No.")]
        public string DicStrongNo { get; set; }
        [Required(ErrorMessage = "Please Fill English Text")]
        public string DicEnglish { get; set; }
        [Required(ErrorMessage = "Please Fill Hebrew Text")]
        public string DicHebrew { get; set; }
        //[Required(ErrorMessage = "Please Enter Language Learning Definition.")]
        public string DicLanguageLearningDefinition { get; set; }
        public string[] DicLanguageLearningDefinitionDynamicTextBox { get; set; }
        [Required(ErrorMessage = "Please Enter at least One definition.")]
        public string[] DicDefinitionDynamicTextBox { get; set; }
        public int Count_TagPictures { get; set; }
        public int Count_TagVerbalDefinition { get; set; }
        public int Count_TagExample { get; set; }
        public int Count_TagSemanticDomain { get; set; }
        public int Count_TagDictionaries { get; set; }
        public int Count_TagSound { get; set; }
        public string SoundTitle { get; set; }
        // [Required(ErrorMessage = "Please Upload Sound file in MP3 format ")]
        public HttpPostedFileBase Soundfile { get; set; }
        public string SoundUrl { get; set; }
        public List<HLL_DefinitionModel> DefinitionList { get; set; }
        public List<HLL_DefinitionModel> LngLrngDefinitionList { get; set; }
    }

}