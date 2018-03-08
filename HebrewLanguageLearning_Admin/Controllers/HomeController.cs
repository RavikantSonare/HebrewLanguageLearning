using HebrewLanguageLearning_Admin.GenericClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HebrewLanguageLearning_Admin.Controllers
{
   
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public JsonResult SyncData(string UserId)
        {
            if (!string.IsNullOrEmpty(UserId))
            {
                //SyncTable("HLL_Vocabulary");
                GenericClasses.XmlSetter _obj = new GenericClasses.XmlSetter(); //"" Need To Open
                return Json(_obj.GetDBToXMl(), JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        [AllowAnonymous]
        public JsonResult SyncTable(string _tablename)
        {
          
            if (!string.IsNullOrEmpty(_tablename))
            {
               
                GenericClasses.XmlSetter _obj = new GenericClasses.XmlSetter();              
                return Json(_obj.GetDBTable(_tablename), JsonRequestBehavior.AllowGet);

            }
            return null;
        }
    }
   
}