using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HebrewLanguageLearning_Admin.Models
{
    public class HLL_StudentsInfoModel: HLL_CommonField
    {
        public string StudentsId { get; set; }
        [Required(ErrorMessage = "Please Enter First Name")]
        public string FName { get; set; }
        [Required(ErrorMessage = "Please Enter Last Name")]
        public string LName { get; set; }
        [Required(ErrorMessage = "Please Enter User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please Enter Password ")]
        public string Password { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int fk_PurposeforLearningHebrew { get; set; }
        public string PreviousHebrewStudies { get; set; }
        [Required(ErrorMessage = "Please Enter Email Address "),EmailAddress(ErrorMessage = "Please Enter Currect email Address ")]
        public string EmailId { get; set; }
        public string ImgUrl { get; set; }
        public HttpPostedFileBase Imagefile { get; set; }

    }
}