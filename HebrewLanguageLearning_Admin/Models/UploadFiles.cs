using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HebrewLanguageLearning_Admin.Models
{
    public class UploadFiles 
    {
        public string tableId { get; set; }
        public string tableName { get; set; }
        public string base64File { get; set; }
        public HttpPostedFileBase physicalFile { get; set; }
        public string fileName { get; set; }
        public string fileExtension { get; set; }
        public int fileType { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; } = DateTime.UtcNow;

    }
}