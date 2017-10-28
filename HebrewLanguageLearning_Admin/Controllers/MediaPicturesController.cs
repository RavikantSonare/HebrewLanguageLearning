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
using System.Transactions;
using System.Threading.Tasks;

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
                if (string.IsNullOrEmpty(hLL_Media_Pictures.ImgUrl)) { hLL_Media_Pictures.ImgUrl = await FilesUtility.base64ToImage(FilesUtility.FileTobase64Convertion(ImageData), hLL_Media_Pictures.PictureId, "Pictures", "temp"); }
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Media_PicturesModels, HLL_Media_Pictures>(); });
                HLL_Media_Pictures DataModel = AutoMapper.Mapper.Map<HLL_Media_PicturesModels, HLL_Media_Pictures>(hLL_Media_Pictures);
                db.HLL_Media_Pictures.Add(DataModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hLL_Media_Pictures);
        }

        // GET: Pictures/Edit/5


        //[ValidateAntiForgeryToken]
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> CreateImage(HLL_Media_PicturesModels hLL_Media_Pictures)
        {
            if (ModelState.IsValid)
            {
                hLL_Media_Pictures.PictureId = EntityConfig.getnewid("HLL_Media_Pictures");
                var ImageData = hLL_Media_Pictures.ImgUrl.Substring(22); hLL_Media_Pictures.ImgUrl = string.Empty;
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Media_PicturesModels, HLL_Media_Pictures>(); });
                if (string.IsNullOrEmpty(hLL_Media_Pictures.ImgUrl)) { hLL_Media_Pictures.ImgUrl = await FilesUtility.base64ToImage(ImageData, hLL_Media_Pictures.PictureId, "Pictures", "temp"); }
                HLL_Media_Pictures DataModel = AutoMapper.Mapper.Map<HLL_Media_PicturesModels, HLL_Media_Pictures>(hLL_Media_Pictures);
                db.HLL_Media_Pictures.Add(DataModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hLL_Media_Pictures);
        }

        public async void SetPicture(HLL_Media_PicturesModels hLL_Media_PicturesModels)
        {

            try
            {
                UploadFiles _objUploadFile = new UploadFiles();
                checkAndDelete(hLL_Media_PicturesModels.MasterTableId);
                _objUploadFile.physicalFile = hLL_Media_PicturesModels.Imagefile;
                _objUploadFile.fileExtension = System.IO.Path.GetExtension(hLL_Media_PicturesModels.Imagefile.FileName);
                _objUploadFile.tableName = hLL_Media_PicturesModels.TableRef;
                _objUploadFile.fileType = 1;
                if (ModelState.IsValid)
                {
                    hLL_Media_PicturesModels.PictureId = EntityConfig.getnewid("HLL_Media_Pictures");
                    _objUploadFile.tableId = hLL_Media_PicturesModels.PictureId;
                    hLL_Media_PicturesModels.ImgUrl = await FilesUtility.UploadFiles(_objUploadFile);
                    AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Media_PicturesModels, HLL_Media_Pictures>(); });
                    HLL_Media_Pictures DataModel = AutoMapper.Mapper.Map<HLL_Media_PicturesModels, HLL_Media_Pictures>(hLL_Media_PicturesModels);
                    db.HLL_Media_Pictures.Add(DataModel);
                    db.SaveChanges();
                }

            }
            catch (Exception ex) { }

        }


        [HttpPost]
        public bool InsertMultiplePicture(List<HLL_Media_PicturesModels> hLL_PicturesModelslst)
        {
            bool Status = false;
            using (TransactionScope scopeImg = new TransactionScope())
            {
                HLLDBContext contextdb = new HLLDBContext();
                try
                {
                   
                    foreach (var entityToInsert in hLL_PicturesModelslst)
                    {
                        entityToInsert.PictureId = EntityConfig.getnewid("HLL_Media_Pictures");
                        if (!string.IsNullOrEmpty(entityToInsert.ImgUrl)) {
                            entityToInsert.ImgUrl = Task.Run(async () => await FilesUtility.base64ToImage(entityToInsert.ImgUrl, entityToInsert.PictureId, "Pictures", entityToInsert.TableRef)).Result;
                        }

                    }
                    AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Media_PicturesModels, HLL_Media_Pictures>(); });
                    List<HLL_Media_Pictures> DataModel = AutoMapper.Mapper.Map<List<HLL_Media_PicturesModels>, List<HLL_Media_Pictures>>(hLL_PicturesModelslst);
                    contextdb.HLL_Media_Pictures.AddRange(DataModel);
                    contextdb.SaveChanges();
                }
                catch(Exception ex) { }
                finally
                {
                    if (contextdb != null)
                        contextdb.Dispose();
                }
                scopeImg.Complete();
                Status = true;
            }
            return Status;
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

        internal void checkAndDelete(string MasterId)
        {
            HLL_Media_Pictures hLL_Media_Pictures = db.HLL_Media_Pictures.Where(x => x.MasterTableId == MasterId).FirstOrDefault();
            if (hLL_Media_Pictures != null)
            {
                db.HLL_Media_Pictures.Remove(hLL_Media_Pictures);
                db.SaveChanges();
            }
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
