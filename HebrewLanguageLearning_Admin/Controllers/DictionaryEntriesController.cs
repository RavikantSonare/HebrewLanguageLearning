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
using System.Threading.Tasks;

namespace HebrewLanguageLearning_Admin.Controllers
{
    public class DictionaryEntriesController : Controller
    {
        private HLLDBContext db = new HLLDBContext();

        // GET: DictionaryEntries
        HLL_DictionaryModel DataModelDictionary;
        public ActionResult Index()
        {
            var DictionaryObj = db.HLL_DictionaryEntries.ToList();
            List<HLL_DictionaryModel> DataModelDictionary = new List<HLL_DictionaryModel>();

            try
            {
                if (DictionaryObj != null)
                {
                    AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_DictionaryEntries, HLL_DictionaryModel>(); });
                    DataModelDictionary = AutoMapper.Mapper.Map<List<HLL_DictionaryEntries>, List<HLL_DictionaryModel>>(DictionaryObj);
                }
               
                foreach (var Item in DataModelDictionary)
                {

                    var _currentDef = db.HLL_Definition.Where(x => x.DicEntId.Equals(Item.DictionaryEntriesId)).ToList();
                    var _currentLLD = db.HLL_LanguageLearningDefinition.Where(x => x.DicEntId.Equals(Item.DictionaryEntriesId)).ToList();

                    Item.Count_Sound = db.HLL_Media_Sound.Where(x => x.MasterTableId.Equals(Item.DictionaryEntriesId)).Count();
                    Item.Count_LLD = _currentLLD.Count();
                    Item.Count_Definition = _currentDef.Count();

                    if (_currentDef != null)
                    {
                        var _curDefLst = _currentDef.Select(z => z.DefinitionId).ToList();
                        Item.Count_SemanticDomain = db.HLL_SemanticDomain.Where(x => _curDefLst.Contains(x.MasterTableId)).Count();
                        Item.Count_Pictures = db.HLL_Media_Pictures.Where(p => _curDefLst.Contains(p.MasterTableId)).Count();
                    }
                    if (_currentLLD != null)
                    {
                        var _curLLDLst = _currentLLD.Select(l => l.LanLernDefId).ToList();
                        Item.Count_Example = db.HLL_Example.Where(x => _curLLDLst.Contains(x.MasterTableId)).Count();
                        Item.Count_Pictures = Item.Count_Pictures + db.HLL_Media_Pictures.Where(p => _curLLDLst.Contains(p.MasterTableId)).Count();
                    }
                }
             
            }
            catch
            {
                return View(DataModelDictionary);
            }
            return View(DataModelDictionary);

        }

        // GET: DictionaryEntries/Details/5
        public ActionResult Details(string id)
        {
            DataModelDictionary = new HLL_DictionaryModel();
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var DictionaryObj = db.HLL_DictionaryEntries.Find(id);
                var LLDictionaryObj = db.HLL_LanguageLearningDefinition.Where(t => t.DicEntId == id).ToList();
                if (DictionaryObj != null)
                {
                    AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_DictionaryEntries, HLL_DictionaryModel>(); });
                    DataModelDictionary = AutoMapper.Mapper.Map<HLL_DictionaryEntries, HLL_DictionaryModel>(DictionaryObj);

                    AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Definition, HLL_DefinitionModel>(); });
                    var DefinitionList = AutoMapper.Mapper.Map<List<HLL_Definition>, List<HLL_DefinitionModel>>(db.HLL_Definition.Where(z => z.DicEntId == DataModelDictionary.DictionaryEntriesId).ToList());

                    foreach (var Item in DefinitionList)
                    {
                        AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Media_Pictures, HLL_Media_PicturesModels>(); });
                        Item.PictureList = AutoMapper.Mapper.Map<List<HLL_Media_Pictures>, List<HLL_Media_PicturesModels>>(db.HLL_Media_Pictures.Where(x => x.MasterTableId.Equals(Item.DefinitionId)).ToList());

                        AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_SemanticDomain, HLL_SemanticDomainModels>(); });
                        Item.SemanticDomainsList = AutoMapper.Mapper.Map<List<HLL_SemanticDomain>, List<HLL_SemanticDomainModels>>(db.HLL_SemanticDomain.Where(x => x.MasterTableId.Equals(Item.DefinitionId)).ToList());

                        AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Example, HLL_ExampleModels>(); });
                        Item.ExampleList = AutoMapper.Mapper.Map<List<HLL_Example>, List<HLL_ExampleModels>>(db.HLL_Example.Where(x => x.MasterTableId.Equals(Item.DefinitionId)).ToList());

                    }
                    if (LLDictionaryObj != null)
                    {
                        //AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_LanguageLearningDefinition, HLL_DictionaryModel>(); });
                        //DataModelDictionary = AutoMapper.Mapper.Map<HLL_LanguageLearningDefinition, HLL_DictionaryModel>(LLDictionaryObj);

                        AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_LanguageLearningDefinition, HLL_DefinitionModel>(); });
                        var LLDefinitionList = AutoMapper.Mapper.Map<List<HLL_LanguageLearningDefinition>, List<HLL_DefinitionModel>>(db.HLL_LanguageLearningDefinition.Where(z => z.DicEntId == DataModelDictionary.DictionaryEntriesId).ToList());

                        foreach (var Item in LLDefinitionList)
                        {
                            AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Media_Pictures, HLL_Media_PicturesModels>(); });
                            Item.PictureList = AutoMapper.Mapper.Map<List<HLL_Media_Pictures>, List<HLL_Media_PicturesModels>>(db.HLL_Media_Pictures.Where(x => x.MasterTableId.Equals(Item.LanLernDefId)).ToList());

                            AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_SemanticDomain, HLL_SemanticDomainModels>(); });
                            Item.SemanticDomainsList = AutoMapper.Mapper.Map<List<HLL_SemanticDomain>, List<HLL_SemanticDomainModels>>(db.HLL_SemanticDomain.Where(x => x.MasterTableId.Equals(Item.LanLernDefId)).ToList());

                            AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Example, HLL_ExampleModels>(); });
                            Item.ExampleList = AutoMapper.Mapper.Map<List<HLL_Example>, List<HLL_ExampleModels>>(db.HLL_Example.Where(x => x.MasterTableId.Equals(Item.LanLernDefId)).ToList());

                        }
                        DataModelDictionary.LngLrngDefinitionList = LLDefinitionList;
                    }
                    DataModelDictionary.DefinitionList = DefinitionList;
                    if (DataModelDictionary == null)
                    {
                        return HttpNotFound();
                    }
                    return View(DataModelDictionary);
                }
            }
            catch
            {
                return View(DataModelDictionary);
            }
            return View(DataModelDictionary);
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

                if (!string.IsNullOrEmpty(hLL_DictionaryEntries.SoundUrl))
                {
                    /* Add Audio */
                    MediaSoundController _obj = new MediaSoundController();
                    HLL_Media_SoundModels _ModelObj = new HLL_Media_SoundModels();
                    _ModelObj.MasterTableId = hLL_DictionaryEntries.DictionaryEntriesId;
                    _ModelObj.Title = String.IsNullOrEmpty(hLL_DictionaryEntries.SoundTitle) ? hLL_DictionaryEntries.DicHebrew + "_SoundFile" : hLL_DictionaryEntries.SoundTitle;
                    _ModelObj.AudioUrl = hLL_DictionaryEntries.SoundUrl.Substring(22);
                    _obj.CreateAudio(_ModelObj);
                }
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
                /* Add Definition */
                LanguageLearningDefinitionController _objLLDef = new LanguageLearningDefinitionController();
                HLL_DefinitionModel _ModelObjLLDef = new HLL_DefinitionModel();
                foreach (var Item in hLL_DictionaryEntries.DicLanguageLearningDefinitionDynamicTextBox)
                {
                    if (!string.IsNullOrEmpty(Item))
                    {
                        _ModelObjLLDef.DicEntId = hLL_DictionaryEntries.DictionaryEntriesId;
                        _ModelObjLLDef.Title = Item;
                        _objLLDef.Create(_ModelObjLLDef);
                    }
                }
                db.HLL_DictionaryEntries.Add(DataModel);
                db.SaveChanges();
                // RedirectToAction("DefinitionList", "Definition"); return JavaScript("window.location = '/Definition/DefinitionList'");
                return JavaScript("window.location = '/DictionaryEntries/Details/" + hLL_DictionaryEntries.DictionaryEntriesId + "'");
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

            // HLL_StudentsInfo hLL_StudentsInfo = db.HLL_StudentsInfo.Find(id);

            AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_DictionaryEntries, HLL_DictionaryModel>(); });
            HLL_DictionaryModel DataModel = AutoMapper.Mapper.Map<HLL_DictionaryEntries, HLL_DictionaryModel>(db.HLL_DictionaryEntries.Find(id));
            var tmpListDicDefinition = db.HLL_Definition.Where(h => h.DicEntId.Equals(DataModel.DictionaryEntriesId) && h.IsDelete == false).ToList();
            DataModel.DicDefinitionDynamicTextBox = new string[tmpListDicDefinition.Count()];
            int i = 0;
            foreach (var Item in tmpListDicDefinition)
            {
                DataModel.DicDefinitionDynamicTextBox[i] = Item.Title;
                i++;
            }

            var tmpLLDList = db.HLL_LanguageLearningDefinition.Where(h => h.DicEntId.Equals(DataModel.DictionaryEntriesId) && h.IsDelete == false).ToList();
            DataModel.DicLanguageLearningDefinitionDynamicTextBox = new string[tmpLLDList.Count()];
            i = 0;
            foreach (var Item in tmpLLDList)
            {
                DataModel.DicLanguageLearningDefinitionDynamicTextBox[i] = Item.Title;
                i++;
            }

            var picData = db.HLL_Media_Sound.Where(p => p.MasterTableId.Equals(id)).FirstOrDefault(); if (picData != null) { DataModel.SoundTitle = picData.Title; }
            if (DataModel == null)
            {
                return HttpNotFound();
            }
            return View(DataModel);
        }

        // POST: DictionaryEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(HLL_DictionaryModel hLL_DictionaryModel)
        {
            if (ModelState.IsValid)
            {
                /* Add Update Sound */
                MediaSoundController _objMediaSound = new MediaSoundController();
                HLL_Media_SoundModels _ModelObjSound = new HLL_Media_SoundModels();

                /* Add Update Definition */
                DefinitionController _objDef = new DefinitionController();
                HLL_DefinitionModel _ModelObjDef = new HLL_DefinitionModel();
                if (hLL_DictionaryModel.Soundfile != null || hLL_DictionaryModel.SoundUrl != null)
                {

                    _ModelObjSound.MasterTableId = hLL_DictionaryModel.DictionaryEntriesId;
                    _ModelObjSound.Soundfile = hLL_DictionaryModel.Soundfile;
                    _ModelObjSound.AudioUrl = hLL_DictionaryModel.SoundUrl;
                    _ModelObjSound.TableRef = "DictionaryEntries";
                    _ModelObjSound.Title = hLL_DictionaryModel.SoundTitle;
                    _objMediaSound.SetSound(_ModelObjSound);
                }
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_DictionaryModel, HLL_DictionaryEntries>(); });
                HLL_DictionaryEntries DataModel = AutoMapper.Mapper.Map<HLL_DictionaryModel, HLL_DictionaryEntries>(hLL_DictionaryModel);
                db.Entry(DataModel).State = EntityState.Modified;
                db.SaveChanges();
                HLL_Definition hLL_Definition = new HLL_Definition();

                var tmpList = db.HLL_Definition.Where(h => h.DicEntId.Equals(DataModel.DictionaryEntriesId) && h.IsDelete == false).Select(z => z.DefinitionId).ToList();
                int i = 0;
                string DefinitionId = string.Empty;
                foreach (var Item in hLL_DictionaryModel.DicDefinitionDynamicTextBox)
                {
                    if (!string.IsNullOrEmpty(Item))
                    {
                        DefinitionId = string.Empty;
                        if (tmpList.Count() > i)
                        {
                            DefinitionId = tmpList[i];
                        }
                        var dicData = db.HLL_Definition.Where(p => p.DicEntId == hLL_DictionaryModel.DictionaryEntriesId && p.DefinitionId == DefinitionId).FirstOrDefault();

                        if (dicData != null)
                        {
                            dicData.Title = Item;
                            _objDef.Edit(dicData);
                        }
                        else
                        {
                            _ModelObjDef.DicEntId = hLL_DictionaryModel.DictionaryEntriesId;
                            _ModelObjDef.Title = Item;
                            _objDef.Create(_ModelObjDef);
                        }
                    }
                    i++;
                }
                /* Add Update LanguageLearningDefinition */
                LanguageLearningDefinitionController _objLLDef = new LanguageLearningDefinitionController();
                var tmpLLDList = db.HLL_LanguageLearningDefinition.Where(h => h.DicEntId.Equals(DataModel.DictionaryEntriesId) && h.IsDelete == false).Select(z => z.LanLernDefId).ToList();
                i = 0;
                string LanLernDefId = string.Empty;
                foreach (var Item in hLL_DictionaryModel.DicLanguageLearningDefinitionDynamicTextBox)
                {
                    if (!string.IsNullOrEmpty(Item))
                    {
                        LanLernDefId = string.Empty;
                        if (tmpLLDList.Count() > i)
                        {
                            LanLernDefId = tmpLLDList[i];
                        }
                        var dicData = db.HLL_LanguageLearningDefinition.Where(p => p.DicEntId == hLL_DictionaryModel.DictionaryEntriesId && p.LanLernDefId == LanLernDefId).FirstOrDefault();

                        if (dicData != null)
                        {
                            dicData.Title = Item;
                            _objLLDef.Edit(dicData);
                        }
                        else
                        {
                            _ModelObjDef.DicEntId = hLL_DictionaryModel.DictionaryEntriesId;
                            _ModelObjDef.Title = Item;
                            _objLLDef.Create(_ModelObjDef);
                        }
                    }
                    i++;
                }
                return JavaScript("window.location = '/DictionaryEntries/Details/" + hLL_DictionaryModel.DictionaryEntriesId + "'");
                //return RedirectToAction("Index");
            }
            return View(hLL_DictionaryModel);
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
