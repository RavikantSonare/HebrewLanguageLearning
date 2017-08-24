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
    public class PicturesController : Controller
    {
        private HLLDBContext db = new HLLDBContext();

        // GET: Pictures
        public ActionResult Index()
        {
            return View(db.HLL_Pictures.ToList());
        }

        // GET: Pictures/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Pictures hLL_Pictures = db.HLL_Pictures.Find(id);
            if (hLL_Pictures == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Pictures);
        }

        // GET: Pictures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pictures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create(HLL_PicturesModels hLL_Pictures)
        {      
            if (ModelState.IsValid)
            {
                hLL_Pictures.PictureId = EntityConfig.getnewid("HLL_Pictures");
                var ImageData = Request.Files["Imagefile"];
                if (string.IsNullOrEmpty(hLL_Pictures.ImgUrl)) { hLL_Pictures.ImgUrl = await ImageUtility.base64ToImage(await ImageUtility.base64ToImageConvertion(ImageData), hLL_Pictures.PictureId, "Pictures"); }
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_PicturesModels, HLL_Pictures>(); });
                HLL_Pictures DataModel = AutoMapper.Mapper.Map<HLL_PicturesModels, HLL_Pictures>(hLL_Pictures);
                db.HLL_Pictures.Add(DataModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hLL_Pictures);
        }

        // GET: Pictures/Edit/5

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> CreateImage(HLL_PicturesModels hLL_Pictures)
        {
            if (ModelState.IsValid)
            {
                hLL_Pictures.PictureId = EntityConfig.getnewid("HLL_Pictures");
                var ImageData = hLL_Pictures.ImgUrl.Substring(22); hLL_Pictures.ImgUrl = string.Empty; 
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_PicturesModels, HLL_Pictures>(); });
                if (string.IsNullOrEmpty(hLL_Pictures.ImgUrl)) { hLL_Pictures.ImgUrl = await ImageUtility.base64ToImage(ImageData, hLL_Pictures.PictureId, "Pictures"); }
                HLL_Pictures DataModel = AutoMapper.Mapper.Map<HLL_PicturesModels, HLL_Pictures>(hLL_Pictures);
                db.HLL_Pictures.Add(DataModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hLL_Pictures);
        }
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Pictures hLL_Pictures = db.HLL_Pictures.Find(id);
            if (hLL_Pictures == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Pictures);
        }

        // POST: Pictures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PictureId,Title,ImgUrl,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_Pictures hLL_Pictures)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hLL_Pictures).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hLL_Pictures);
        }

        // GET: Pictures/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Pictures hLL_Pictures = db.HLL_Pictures.Find(id);
            if (hLL_Pictures == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Pictures);
        }

        // POST: Pictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HLL_Pictures hLL_Pictures = db.HLL_Pictures.Find(id);
            db.HLL_Pictures.Remove(hLL_Pictures);
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
