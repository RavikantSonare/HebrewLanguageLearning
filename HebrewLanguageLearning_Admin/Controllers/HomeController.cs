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
                GenericClasses.XmlSetter _obj = new GenericClasses.XmlSetter();
                return Json(_obj.GetDBToXMl(), JsonRequestBehavior.AllowGet);
            }
            return null;
        }
    }
}