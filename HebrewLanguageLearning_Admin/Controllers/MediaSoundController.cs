using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HebrewLanguageLearning_Admin.DBContext;
using HebrewLanguageLearning_Admin.Models;
using HebrewLanguageLearning_Admin.GenericClasses;
using System.IO;
using System.IO.IsolatedStorage;



namespace HebrewLanguageLearning_Admin.Controllers
{
    public class MediaSoundController : Controller
    {
        private HLLDBContext db = new HLLDBContext();

        // GET: Sound
        public ActionResult Index()
        {
            //Microphone mic = Microphone.Default;
            //if (mic == null)
            //{
            //    //return false; // No microphone is attached to the device
            //}
            return View(db.HLL_Media_Sound.ToList());
        }

        // GET: Sound/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Media_Sound HLL_Media_Sound = db.HLL_Media_Sound.Find(id);
            if (HLL_Media_Sound == null)
            {
                return HttpNotFound();
            }
            return View(HLL_Media_Sound);
        }

        // GET: Sound/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sound/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create(HLL_Media_SoundModels HLL_Media_SoundModel)
        {
            if (ModelState.IsValid)
            {
                HLL_Media_SoundModel.SoundId = EntityConfig.getnewid("HLL_Media_Sound");
                var SoundData = HLL_Media_SoundModel.Soundfile; //Request.Files["Imagefile"];
                if (string.IsNullOrEmpty(HLL_Media_SoundModel.AudioUrl)) { HLL_Media_SoundModel.AudioUrl = await FilesUtility.base64ToFile(FilesUtility.FileTobase64Convertion(SoundData), HLL_Media_SoundModel.SoundId, "Sound", HLL_Media_SoundModel.TableRef); }

                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Media_SoundModels, HLL_Media_Sound>(); });
                HLL_Media_Sound DataModel = AutoMapper.Mapper.Map<HLL_Media_SoundModels, HLL_Media_Sound>(HLL_Media_SoundModel);
                db.HLL_Media_Sound.Add(DataModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(HLL_Media_SoundModel);
        }

        public async System.Threading.Tasks.Task CreateAudio(HLL_Media_SoundModels HLL_Media_Sound)
        {
            if (ModelState.IsValid)
            {
                HLL_Media_Sound.SoundId = EntityConfig.getnewid("HLL_Media_Sound");
                var SoundData = HLL_Media_Sound.AudioUrl; //Request.Files["Imagefile"];
                HLL_Media_Sound.AudioUrl = await FilesUtility.base64ToFile(SoundData, HLL_Media_Sound.SoundId, "Sound", "temp");
            }
            AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Media_SoundModels, HLL_Media_Sound>(); });
            HLL_Media_Sound DataModel = AutoMapper.Mapper.Map<HLL_Media_SoundModels, HLL_Media_Sound>(HLL_Media_Sound);
            db.HLL_Media_Sound.Add(DataModel);
            db.SaveChanges();

        }
        public async void SetSound(HLL_Media_SoundModels hLL_Media_SoundModels)
        {

            try
            {
                UploadFiles _objUploadFile = new UploadFiles();
                _objUploadFile.physicalFile = hLL_Media_SoundModels.Soundfile;
                _objUploadFile.fileExtension = System.IO.Path.GetExtension(hLL_Media_SoundModels.Soundfile.FileName);
                _objUploadFile.tableName = hLL_Media_SoundModels.TableRef;
                _objUploadFile.fileType = 0;
                if (ModelState.IsValid)
                {
                    hLL_Media_SoundModels.SoundId = EntityConfig.getnewid("HLL_Media_Video");
                    _objUploadFile.tableId = hLL_Media_SoundModels.SoundId;
                    hLL_Media_SoundModels.AudioUrl = await FilesUtility.UploadFiles(_objUploadFile);
                    AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Media_SoundModels, HLL_Media_Sound>(); });
                    HLL_Media_Sound DataModel = AutoMapper.Mapper.Map<HLL_Media_SoundModels, HLL_Media_Sound>(hLL_Media_SoundModels);
                    db.HLL_Media_Sound.Add(DataModel);
                    db.SaveChanges();
                }

            }
            catch (Exception ex) { }

        }
        //public ActionResult Create(HLL_Media_SoundModels HLL_Media_Sound)
        //{

        //        HLL_Media_Sound.SoundId = EntityConfig.getnewid("HLL_Media_Sound");
        //        AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Media_SoundModels, HLL_Media_Sound>(); });
        //        HLL_Media_Sound DataModel = AutoMapper.Mapper.Map<HLL_Media_SoundModels, HLL_Media_Sound>(HLL_Media_Sound);
        //        db.HLL_Media_Sound.Add(DataModel);
        //        db.SaveChanges();

        //    return JsonResult(new {result:"success" });
        //}
        // GET: Sound/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Media_Sound HLL_Media_Sound = db.HLL_Media_Sound.Find(id);
            if (HLL_Media_Sound == null)
            {
                return HttpNotFound();
            }
            return View(HLL_Media_Sound);
        }

        // POST: Sound/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoundId,Title,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_Media_Sound HLL_Media_Sound)
        {
            if (ModelState.IsValid)
            {
                db.Entry(HLL_Media_Sound).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(HLL_Media_Sound);
        }

        // GET: Sound/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Media_Sound HLL_Media_Sound = db.HLL_Media_Sound.Find(id);
            if (HLL_Media_Sound == null)
            {
                return HttpNotFound();
            }
            return View(HLL_Media_Sound);
        }

        // POST: Sound/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HLL_Media_Sound HLL_Media_Sound = db.HLL_Media_Sound.Find(id);
            db.HLL_Media_Sound.Remove(HLL_Media_Sound);
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
