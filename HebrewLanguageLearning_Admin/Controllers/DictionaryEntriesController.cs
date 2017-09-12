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
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

namespace HebrewLanguageLearning_Admin.Controllers
{
    public class DictionaryEntriesController : Controller
    {
        private HLLDBContext db = new HLLDBContext();

        public object Mapper { get; private set; }

        // GET: DictionaryEntries
        public ActionResult Index()
        {

            return View(db.HLL_DictionaryEntries.ToList());
        }

        // GET: DictionaryEntries/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_DictionaryEntries hLL_DictionaryEntries = db.HLL_DictionaryEntries.Find(id);
            if (hLL_DictionaryEntries == null)
            {
                return HttpNotFound();
            }
            return View(hLL_DictionaryEntries);
        }

        // GET: DictionaryEntries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DictionaryEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //&& hLL_DictionaryEntries.Soundfile.ContentLength > 0 && hLL_DictionaryEntries.Soundfile.ContentType == "audio/mp3"
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HLL_DictionaryModel hLL_DictionaryEntries)
        {
            HttpPostedFileBase files = Request.Files["Soundfile"];
            if (ModelState.IsValid)
            {
                /* Add Dictionary */

                hLL_DictionaryEntries.DictionaryEntriesId = EntityConfig.getnewid("HLL_DictionaryEntries");
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_DictionaryModel, HLL_DictionaryEntries>(); });
                HLL_DictionaryEntries DataModel = AutoMapper.Mapper.Map<HLL_DictionaryModel, HLL_DictionaryEntries>(hLL_DictionaryEntries);

                
                /* Add Audio */
                MediaSoundController _obj = new MediaSoundController();
                HLL_Media_SoundModels _ModelObj = new HLL_Media_SoundModels();
                _ModelObj.MasterTableId = hLL_DictionaryEntries.DictionaryEntriesId;
                _ModelObj.Title = hLL_DictionaryEntries.SoundTitle;
                _ModelObj.AudioUrl = hLL_DictionaryEntries.SoundUrl.Substring(22);

                _obj.CreateAudio(_ModelObj);

                /* Add Definition */
                DefinitionController _objDef = new DefinitionController();
                HLL_DefinitionModel _ModelObjDef = new HLL_DefinitionModel();
                foreach (var Item in hLL_DictionaryEntries.DicDefinitionDynamicTextBox)
                {
                    if (!string.IsNullOrEmpty(Item))
                    {
                        _ModelObjDef.DicEntId = hLL_DictionaryEntries.DictionaryEntriesId;
                        _ModelObjDef.Title = Item;
                        _objDef.Create(_ModelObjDef);
                    }
                }

               db.HLL_DictionaryEntries.Add(DataModel);
               db.SaveChanges();

              
                return JavaScript("window.location = '/Definition/DefinitionList'");
            }

            return Content("Please Upload Sound file in MP3 format");
            //return View(hLL_DictionaryEntries);
        }

        // GET: DictionaryEntries/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_DictionaryEntries hLL_DictionaryEntries = db.HLL_DictionaryEntries.Find(id);
            if (hLL_DictionaryEntries == null)
            {
                return HttpNotFound();
            }
            return View(hLL_DictionaryEntries);
        }

        // POST: DictionaryEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DictionaryEntriesId,DicEnglish,DicHebrew,fk_TagPictures,fk_TagVerbalDefinition,fk_TagExample,fk_TagSemanticDomain,fk_TagDictionaries,fk_TagSound,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_DictionaryEntries hLL_DictionaryEntries)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hLL_DictionaryEntries).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hLL_DictionaryEntries);
        }

        // GET: DictionaryEntries/Delete/5
        public ActionResult Delete(string id)
        {



            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_DictionaryEntries hLL_DictionaryEntries = db.HLL_DictionaryEntries.Find(id);
            if (hLL_DictionaryEntries == null)
            {
                return HttpNotFound();
            }
            return View(hLL_DictionaryEntries);
        }

        // POST: DictionaryEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HLL_DictionaryEntries hLL_DictionaryEntries = db.HLL_DictionaryEntries.Find(id);
            db.HLL_DictionaryEntries.Remove(hLL_DictionaryEntries);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public ActionResult AddControl()
        //{
        //    TextBox txtDic = new TextBox();
        //    txtDic.Text = "</label><div class='col-sm-8'>";
        //    ctrlPlaceholderTextBox.Controls.Add(lblop);
        //    return new EmptyResult();
        //}
        // POST: Home
        [HttpPost]
        public ActionResult SaveData(string[] DynamicTextBox)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ViewBag.Values = serializer.Serialize(DynamicTextBox);

            string message = "";
            foreach (string textboxValue in DynamicTextBox)
            {
                message += textboxValue + "\\n";
            }
            ViewBag.Message = message;

            return View();
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
