using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HebrewLanguageLearning_Admin.DBContext;
using HebrewLanguageLearning_Admin.Models;
using HebrewLanguageLearning_Admin.GenericClasses;

namespace HebrewLanguageLearning_Admin.Controllers
{
    public class MediaVideoController : Controller
    {
        private HLLDBContext db = new HLLDBContext();

        // GET: MediaVideo
        public async Task<ActionResult> Index()
        {
            return View(await db.HLL_Media_Video.ToListAsync());
        }

        // GET: MediaVideo/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Media_Video hLL_Media_Video = await db.HLL_Media_Video.FindAsync(id);
            if (hLL_Media_Video == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Media_Video);
        }

        // GET: MediaVideo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MediaVideo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VideoId,MasterTableId,Title,VideoUrl,Description,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_Media_Video hLL_Media_Video)
        {
            if (ModelState.IsValid)
            {
                db.HLL_Media_Video.Add(hLL_Media_Video);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(hLL_Media_Video);
        }

        public async void SetVideo(HLL_Media_VideoModels hLL_Media_VideoModels)
        {
            try
            {
                UploadFiles _objUploadFile = new UploadFiles();
                _objUploadFile.physicalFile = hLL_Media_VideoModels.Videofile;
                _objUploadFile.fileExtension = System.IO.Path.GetExtension(hLL_Media_VideoModels.Videofile.FileName);
                _objUploadFile.tableName = hLL_Media_VideoModels.TableRef;
                _objUploadFile.fileType = 2;
                if (ModelState.IsValid)
                {
                    hLL_Media_VideoModels.VideoId = EntityConfig.getnewid("HLL_Media_Video");
                    _objUploadFile.tableId = hLL_Media_VideoModels.VideoId;
                    hLL_Media_VideoModels.VideoUrl = await FilesUtility.UploadFiles(_objUploadFile);  
                    AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Media_VideoModels, HLL_Media_Video>(); });
                    HLL_Media_Video DataModel = AutoMapper.Mapper.Map<HLL_Media_VideoModels, HLL_Media_Video>(hLL_Media_VideoModels);
                    db.HLL_Media_Video.Add(DataModel);
                    db.SaveChanges();
                }

            }
            catch (Exception ex) { }

        }

        // GET: MediaVideo/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Media_Video hLL_Media_Video = await db.HLL_Media_Video.FindAsync(id);
            if (hLL_Media_Video == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Media_Video);
        }

        // POST: MediaVideo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VideoId,MasterTableId,Title,VideoUrl,Description,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_Media_Video hLL_Media_Video)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hLL_Media_Video).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hLL_Media_Video);
        }

        // GET: MediaVideo/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Media_Video hLL_Media_Video = await db.HLL_Media_Video.FindAsync(id);
            if (hLL_Media_Video == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Media_Video);
        }

        // POST: MediaVideo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            HLL_Media_Video hLL_Media_Video = await db.HLL_Media_Video.FindAsync(id);
            db.HLL_Media_Video.Remove(hLL_Media_Video);
            await db.SaveChangesAsync();
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
