﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HebrewLanguageLearning_Admin.Models
{
    public class HLL_PicturesModels : HLL_CommonField
    {
        public string PictureId { get; set; }
        public string DefinitionId { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase Imagefile { get; set; }
    }
}