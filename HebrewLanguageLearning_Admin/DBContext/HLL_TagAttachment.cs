//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HebrewLanguageLearning_Admin.DBContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class HLL_TagAttachment
    {
        public string TagAttachmentId { get; set; }
        public string DictionaryEntriesfk { get; set; }
        public string AttachTag { get; set; }
        public string Tagfk { get; set; }
        public Nullable<bool> ActiveStatus { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
