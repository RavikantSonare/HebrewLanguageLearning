using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HebrewLanguageLearning_Admin.Models
{
    public class HLL_CommonField
    {
         public Nullable<bool> ActiveStatus { get; set; } = true;
         public Nullable<bool> IsActive { get; set; } = true;
         public Nullable<bool> IsDelete { get; set; } = false;
         public Nullable<int> CreatedBy { get; set; } = 1;
         public Nullable<System.DateTime> CreatedDate { get; set; } = DateTime.UtcNow;   
         public Nullable<int> UpdatedBy { get; set; } = 0;
         public Nullable<System.DateTime> UpdatedDate { get; set; } = DateTime.UtcNow;  
    }          
}