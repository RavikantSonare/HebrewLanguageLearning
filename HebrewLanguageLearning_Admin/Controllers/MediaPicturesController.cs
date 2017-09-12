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
    public class MediaPicturesController : Controller
    {
        private HLLDBContext db = new HLLDBContext();

        // GET: Pictures
        public ActionResult Index()
        {
            return View(db.HLL_Media_Pictures.ToList());
        }

        // GET: Pictures/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Media_Pictures hLL_Media_Pictures = db.HLL_Media_Pictures.Find(id);
            if (hLL_Media_Pictures == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Media_Pictures);
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
        public async System.Threading.Tasks.Task<ActionResult> Create(HLL_Media_PicturesModels hLL_Media_Pictures)
        {      
            if (ModelState.IsValid)
            {
                hLL_Media_Pictures.PictureId = EntityConfig.getnewid("HLL_Media_Pictures");
                var ImageData = Request.Files["Imagefile"];
                if (string.IsNullOrEmpty(hLL_Media_Pictures.ImgUrl)) { hLL_Media_Pictures.ImgUrl = await FilesUtility.base64ToImage(FilesUtility.FileTobase64Convertion(ImageData), hLL_Media_Pictures.PictureId, "Pictures"); }
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Media_PicturesModels, HLL_Media_Pictures>(); });
                HLL_Media_Pictures DataModel = AutoMapper.Mapper.Map<HLL_Media_PicturesModels, HLL_Media_Pictures>(hLL_Media_Pictures);
                db.HLL_Media_Pictures.Add(DataModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hLL_Media_Pictures);
        }

        // GET: Pictures/Edit/5

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> CreateImage(HLL_Media_PicturesModels hLL_Media_Pictures)
        {
            if (ModelState.IsValid)
            {
                hLL_Media_Pictures.PictureId = EntityConfig.getnewid("HLL_Media_Pictures");
                var ImageData = hLL_Media_Pictures.ImgUrl.Substring(22); hLL_Media_Pictures.ImgUrl = string.Empty; 
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Media_PicturesModels, HLL_Media_Pictures>(); });
                if (string.IsNullOrEmpty(hLL_Media_Pictures.ImgUrl)) { hLL_Media_Pictures.ImgUrl = await FilesUtility.base64ToImage(ImageData, hLL_Media_Pictures.PictureId, "Pictures"); }
                HLL_Media_Pictures DataModel = AutoMapper.Mapper.Map<HLL_Media_PicturesModels, HLL_Media_Pictures>(hLL_Media_Pictures);
                db.HLL_Media_Pictures.Add(DataModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hLL_Media_Pictures);
        }

        public async void CreatePicture(HLL_Media_PicturesModels hLL_Media_Pictures)
        {
            if (ModelState.IsValid)
            {
                hLL_Media_Pictures.PictureId = EntityConfig.getnewid("HLL_Media_Pictures");
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Media_PicturesModels, HLL_Media_Pictures>(); });
                if (string.IsNullOrEmpty(hLL_Media_Pictures.ImgUrl)) { hLL_Media_Pictures.ImgUrl = await FilesUtility.base64ToImage(FilesUtility.FileTobase64Convertion(hLL_Media_Pictures.Imagefile), hLL_Media_Pictures.PictureId, "Pictures", hLL_Media_Pictures.TableRef); }
                HLL_Media_Pictures DataModel = AutoMapper.Mapper.Map<HLL_Media_PicturesModels, HLL_Media_Pictures>(hLL_Media_Pictures);
                db.HLL_Media_Pictures.Add(DataModel);
                db.SaveChanges();

            }
            
        }
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Media_Pictures hLL_Media_Pictures = db.HLL_Media_Pictures.Find(id);
            if (hLL_Media_Pictures == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Media_Pictures);
        }

        // POST: Pictures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PictureId,Title,ImgUrl,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_Media_Pictures hLL_Media_Pictures)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hLL_Media_Pictures).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hLL_Media_Pictures);
        }

        // GET: Pictures/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Media_Pictures hLL_Media_Pictures = db.HLL_Media_Pictures.Find(id);
            if (hLL_Media_Pictures == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Media_Pictures);
        }

        // POST: Pictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HLL_Media_Pictures hLL_Media_Pictures = db.HLL_Media_Pictures.Find(id);
            db.HLL_Media_Pictures.Remove(hLL_Media_Pictures);
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
