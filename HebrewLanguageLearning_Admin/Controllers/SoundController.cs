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
    public class SoundController : Controller
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
            return View(db.HLL_Sound.ToList());
        }

        // GET: Sound/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Sound hLL_Sound = db.HLL_Sound.Find(id);
            if (hLL_Sound == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Sound);
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
        public async System.Threading.Tasks.Task<ActionResult> Create(HLL_SoundModels hLL_Sound)
        {
            if (ModelState.IsValid)
            {
                hLL_Sound.SoundId = EntityConfig.getnewid("HLL_Sound");
                var SoundData = hLL_Sound.Soundfile; //Request.Files["Imagefile"];
                if (string.IsNullOrEmpty(hLL_Sound.AudioUrl)) { hLL_Sound.AudioUrl = await ImageUtility.base64ToFile(await ImageUtility.base64ToImageConvertion(SoundData), hLL_Sound.SoundId, "Sound"); }

                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_SoundModels, HLL_Sound>(); });
                HLL_Sound DataModel = AutoMapper.Mapper.Map<HLL_SoundModels, HLL_Sound>(hLL_Sound);
                db.HLL_Sound.Add(DataModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hLL_Sound);
        }
        public async System.Threading.Tasks.Task CreateAudio(HLL_SoundModels hLL_Sound)
        {
            if (ModelState.IsValid)
            {
                hLL_Sound.SoundId = EntityConfig.getnewid("HLL_Sound");
                var SoundData = hLL_Sound.AudioUrl; //Request.Files["Imagefile"];
                hLL_Sound.AudioUrl = await ImageUtility.base64ToFile(SoundData, hLL_Sound.SoundId, "Sound");
            }
            AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_SoundModels, HLL_Sound>(); });
            HLL_Sound DataModel = AutoMapper.Mapper.Map<HLL_SoundModels, HLL_Sound>(hLL_Sound);
            db.HLL_Sound.Add(DataModel);
            db.SaveChanges();

        }



        //public ActionResult Create(HLL_SoundModels hLL_Sound)
        //{

        //        hLL_Sound.SoundId = EntityConfig.getnewid("HLL_Sound");
        //        AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_SoundModels, HLL_Sound>(); });
        //        HLL_Sound DataModel = AutoMapper.Mapper.Map<HLL_SoundModels, HLL_Sound>(hLL_Sound);
        //        db.HLL_Sound.Add(DataModel);
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
            HLL_Sound hLL_Sound = db.HLL_Sound.Find(id);
            if (hLL_Sound == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Sound);
        }

        // POST: Sound/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoundId,Title,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_Sound hLL_Sound)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hLL_Sound).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hLL_Sound);
        }

        // GET: Sound/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Sound hLL_Sound = db.HLL_Sound.Find(id);
            if (hLL_Sound == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Sound);
        }

        // POST: Sound/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HLL_Sound hLL_Sound = db.HLL_Sound.Find(id);
            db.HLL_Sound.Remove(hLL_Sound);
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
