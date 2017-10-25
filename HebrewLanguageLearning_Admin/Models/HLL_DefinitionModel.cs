using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HebrewLanguageLearning_Admin.Models
{
    public class HLL_DefinitionModel: HLL_CommonField
    {
        public string DefinitionId { get; set; }
        public string LanLernDefId { get; set; }
        public string DicEntId { get; set; }
        public string Title { get; set; }
        //Picture Id
        public string PictureId { get; set; }
        public string PictureTitle { get; set; }
        public string ImgUrl { get; set; }
        public HttpPostedFileBase Imagefile { get; set; }

        public List<HLL_Media_PicturesModels>PictureList { get; set; }
        public List<HLL_SemanticDomainModels> SemanticDomainsList { get; set; }
        public List<HLL_ExampleModels> ExampleList { get; set; }
    }
}