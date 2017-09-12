using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HebrewLanguageLearning_Admin.DBContext;
using HebrewLanguageLearning_Admin.GenericClasses;
using HebrewLanguageLearning_Admin.Models;

namespace HebrewLanguageLearning_Admin.Controllers
{
    public class StudentsInfoController : Controller
    {
        private HLLDBContext db = new HLLDBContext();

        // GET: StudentsInfo
        public ActionResult Index()
        {
            AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_StudentsInfo, HLL_StudentsInfoModel>(); });
            List<HLL_StudentsInfoModel> DataModel = AutoMapper.Mapper.Map<List<HLL_StudentsInfo>, List<HLL_StudentsInfoModel>>(db.HLL_StudentsInfo.ToList());

            return View(DataModel);
        }


        // GET: StudentsInfo/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_StudentsInfo hLL_StudentsInfo = db.HLL_StudentsInfo.Find(id);
            if (hLL_StudentsInfo == null)
            {
                return HttpNotFound();
            }
            return View(hLL_StudentsInfo);
        }

        // GET: StudentsInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentsInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create(HLL_StudentsInfoModel hLL_StudentsInfo)  /* [Bind(Include = "FName,LName,UserName,Password,City,State,Country,Zip,Address,Telephone,EmailId,ImgUrl")]*/
        {
            if (ModelState.IsValid)
            {
                hLL_StudentsInfo.StudentsId = EntityConfig.getnewid("HLL_StudentsInfo");
                var ImageData = Request.Files["Imagefile"];
                if (string.IsNullOrEmpty(hLL_StudentsInfo.ImgUrl)) { hLL_StudentsInfo.ImgUrl = await FilesUtility.base64ToImage(FilesUtility.FileTobase64Convertion(ImageData), hLL_StudentsInfo.StudentsId, "Students", "temp"); }
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_StudentsInfoModel, HLL_StudentsInfo>(); });
                HLL_StudentsInfo DataModel = AutoMapper.Mapper.Map<HLL_StudentsInfoModel, HLL_StudentsInfo>(hLL_StudentsInfo);

                db.HLL_StudentsInfo.Add(DataModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }




            return View(hLL_StudentsInfo);
        }

        // GET: StudentsInfo/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_StudentsInfo hLL_StudentsInfo = db.HLL_StudentsInfo.Find(id);
            if (hLL_StudentsInfo == null)
            {
                return HttpNotFound();
            }
            return View(hLL_StudentsInfo);
        }

        // POST: StudentsInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( HLL_StudentsInfo hLL_StudentsInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hLL_StudentsInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hLL_StudentsInfo);
        }

        // GET: StudentsInfo/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_StudentsInfo hLL_StudentsInfo = db.HLL_StudentsInfo.Find(id);
            if (hLL_StudentsInfo == null)
            {
                return HttpNotFound();
            }
            return View(hLL_StudentsInfo);
        }

        // POST: StudentsInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HLL_StudentsInfo hLL_StudentsInfo = db.HLL_StudentsInfo.Find(id);
            db.HLL_StudentsInfo.Remove(hLL_StudentsInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
       
    }
   
}
