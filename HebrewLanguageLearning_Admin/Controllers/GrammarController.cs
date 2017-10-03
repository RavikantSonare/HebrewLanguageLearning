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
using System.IO;

namespace HebrewLanguageLearning_Admin.Controllers
{
    public class GrammarController : Controller
    {
        private HLLDBContext db = new HLLDBContext();
        private List<HLL_Application> ApplicationData = new List<HLL_Application>();
        // GET: Application
        public async Task<ActionResult> Index()
        {
            List<HLL_GrammarModel> DataModelList = new List<HLL_GrammarModel>();
            try
            {
                var data = await db.HLL_Grammar.Where(x => x.IsDelete == false).ToListAsync();
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Grammar, HLL_GrammarModel>(); });
                DataModelList = AutoMapper.Mapper.Map<List<HLL_Grammar>, List<HLL_GrammarModel>>(data);
                var SoundData = db.HLL_Media_Sound.Select(c => c.MasterTableId).ToList();
                var ImageData = db.HLL_Media_Pictures.Select(c => c.MasterTableId).ToList();
                var VideoData = db.HLL_Media_Video.Select(c => c.MasterTableId).ToList();
                int correctAnswerCounter = 0;
                DataModelList.ForEach(e =>
                {
                    correctAnswerCounter = 0;
                    if (SoundData.Contains(e.GrammarId)) { e.Soundfiles = "0"; }
                    if (ImageData.Contains(e.GrammarId)) { e.ImgVdofiles = "1"; }
                    if (VideoData.Contains(e.GrammarId)) { e.ImgVdofiles = "2"; }

                    e.ExercisesNumber = correctAnswerCounter.ToString();

                });

            }
            catch (Exception ex) { }
            ModelState.Clear();
            return View(DataModelList.OrderByDescending(x => x.GrammarId));
        }

        public string ReturnLesson(string MasterTableId)
        {
            string LessonId = "0";
            try
            {
                if (ApplicationData.Count == 0)
                {
                    ApplicationData = db.HLL_Application.ToList();
                }
                var Data = ApplicationData.Where(z => z.ApplicationId.Equals(MasterTableId)).FirstOrDefault();
                if (Data != null)
                {
                    LessonId = Data.LessonId;
                }
            }
            catch (Exception ex)
            {

            }
            return LessonId;
        }
        // GET: Application/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Application hLL_Application = await db.HLL_Application.FindAsync(id);
            if (hLL_Application == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Application);
        }

        private HLL_GrammarModel set_ObjDataModelData(HLL_GrammarModel _ObjDataModel)
        {
            _ObjDataModel.AppSentenceDynamicTextBox = new string[12];
            _ObjDataModel.Imgfile = new HttpPostedFileBase[12];
            _ObjDataModel.Videofile = new HttpPostedFileBase[12];
            _ObjDataModel.Soundfile = new HttpPostedFileBase[12];
            _ObjDataModel.ImgVdofile = new HttpPostedFileBase[12];
            return _ObjDataModel;
        }

        public async Task<ActionResult> Create(string GrammarId = "0")
        {
            HLL_GrammarModel _ObjDataModel = new HLL_GrammarModel(); _ObjDataModel.ExercisesNumber = "4"; TempData["isEdit"] = false;
            _ObjDataModel = set_ObjDataModelData(_ObjDataModel);
            if (GrammarId != "0")
            {
                TempData["isEdit"] = true;
                var objGrammar = db.HLL_Grammar.Where(x => x.IsDelete == false).Where(p => p.GrammarId == GrammarId).FirstOrDefault();
                var objGrammarDataList = db.HLL_HebrewGrammarData.Where(x => x.IsDelete == false && x.MasterTableId == objGrammar.GrammarId).ToList();
                var ListData = objGrammarDataList.Select(y => y.HebrewGrammarDataId).ToList();
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Grammar, HLL_GrammarModel>(); });
                _ObjDataModel = AutoMapper.Mapper.Map<HLL_Grammar, HLL_GrammarModel>(objGrammar);
                _ObjDataModel = set_ObjDataModelData(_ObjDataModel);
                int i = 0, txtlenght = objGrammarDataList.Count() > 4 ? objGrammarDataList.Count() : 4;
                _ObjDataModel.ExercisesNumber = Convert.ToString(txtlenght);
                _ObjDataModel.VideoUrl = new string[15];
                _ObjDataModel.ImgUrl = new string[15];
                _ObjDataModel.SoundUrl = new string[15];
                foreach (var Item in objGrammarDataList)
                {

                    _ObjDataModel.AppSentenceDynamicTextBox[i] = Item.HebrewGrammarData;
                    var SoundDataList = db.HLL_Media_Sound.Where(x => x.MasterTableId == Item.HebrewGrammarDataId).FirstOrDefault();
                    if (SoundDataList != null) { _ObjDataModel.SoundUrl[i] = "0"; }
                    var ImageDataList = db.HLL_Media_Pictures.Where(x => x.MasterTableId == Item.HebrewGrammarDataId).FirstOrDefault();
                    if (ImageDataList != null) { _ObjDataModel.VideoUrl[i] = "1"; }
                    else
                    {
                        var VideoDataList = db.HLL_Media_Video.Where(x => x.MasterTableId == Item.HebrewGrammarDataId).FirstOrDefault();
                        if (VideoDataList != null) { _ObjDataModel.VideoUrl[i] = "2"; }
                    }
                    i++;
                }


                ModelState.Clear();
            }
            return View(_ObjDataModel);
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetDataForm(string GrammarId)
        {
            HLL_GrammarModel DataModel = new HLL_GrammarModel();
            try
            {
                var _ModelHLL_GrammarObj = await db.HLL_Grammar.Where(x => x.GrammarId.Equals(GrammarId)).FirstOrDefaultAsync();
                var _ModelHLL_HebrewGrammarDataObj = await db.HLL_HebrewGrammarData.Where(x => x.MasterTableId.Equals(_ModelHLL_GrammarObj.GrammarId)).ToListAsync();

                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Grammar, HLL_GrammarModel>(); });
                DataModel = AutoMapper.Mapper.Map<HLL_Grammar, HLL_GrammarModel>(_ModelHLL_GrammarObj);
                DataModel.AppSentenceDynamicTextBox = new string[15];
                DataModel.VideoUrl = new string[15];
                DataModel.ImgUrl = new string[15];
                DataModel.SoundUrl = new string[15];

                DataModel.Imgfile = new HttpPostedFileBase[15];
                DataModel.Videofile = new HttpPostedFileBase[15];
                DataModel.Soundfile = new HttpPostedFileBase[15];
                DataModel.ImgVdofile = new HttpPostedFileBase[15];
                int i = 0;
                foreach (var Item in db.HLL_HebrewGrammarData.Where(h => h.MasterTableId.Equals(_ModelHLL_GrammarObj.GrammarId) && h.IsDelete == false).ToList())
                {
                    DataModel.AppSentenceDynamicTextBox[i] = Item.HebrewGrammarData;
                    var SoundDataList = db.HLL_Media_Sound.Where(x => x.MasterTableId == Item.HebrewGrammarDataId).FirstOrDefault();
                    if (SoundDataList != null) { DataModel.SoundUrl[i] = "0"; }
                    var ImageDataList = db.HLL_Media_Pictures.Where(x => x.MasterTableId == Item.HebrewGrammarDataId).FirstOrDefault();
                    if (ImageDataList != null) { DataModel.VideoUrl[i] = "1"; }
                    else
                    {
                        var VideoDataList = db.HLL_Media_Video.Where(x => x.MasterTableId == Item.HebrewGrammarDataId).FirstOrDefault();
                        if (VideoDataList != null) { DataModel.VideoUrl[i] = "2"; }
                    }

                    i++;
                }

            }
            catch (Exception ex) { }
            ModelState.Clear();
            return PartialView("_HLL_Grammar_Data_PartialView", DataModel);
        }
        // POST: Application/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.


        public string GetLessonNext(string HebrewApplicationId)
        {

            var next = db.HLL_Definition.ToList().SkipWhile(obj => obj.DefinitionId != HebrewApplicationId).Skip(1).FirstOrDefault();
            if (next != null)
            {
                HebrewApplicationId = next.DefinitionId;
            }

            return HebrewApplicationId;

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HLL_GrammarModel _hLL_GrammarModel)
        {
            dynamic isEdit = false;
            if (TempData["isEdit"] != null)
            {
                isEdit = TempData["isEdit"];
            }
            HLL_GrammarModel _ModelObjHAD = new HLL_GrammarModel();
            HLL_Media_VideoModels _ModelObjMedVid = new HLL_Media_VideoModels();
            HLL_Media_PicturesModels _ModelObjMedPic = new HLL_Media_PicturesModels();
            HLL_Media_SoundModels _ModelObjMedSound = new HLL_Media_SoundModels();
            int i = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    /*  Add Grammer */
                    HLL_HebrewApplicationData _Obj_HebrewApplicationData = new HLL_HebrewApplicationData();
                    if (isEdit)
                    {
                        _ModelObjHAD = CheckAndSetGrammerData(_hLL_GrammarModel, 1);
                        ///  UpdateGrammarModel(_ModelObjHAD, _hLL_GrammarModel);

                    }
                    else
                    {
                        _ModelObjHAD = CheckAndSetGrammerData(_hLL_GrammarModel, 0);
                    }

                    foreach (var Item in _hLL_GrammarModel.AppSentenceDynamicTextBox)
                    {

                        if (!string.IsNullOrEmpty(Item))
                        {   /* Add Hebrew Grammar Data */
                            HLL_HebrewGrammarDataModel hLL_HebrewGrammarData = new HLL_HebrewGrammarDataModel();
                            HLL_HebrewGrammarData hLL_HebrewGrammarDataFirstModel = new HLL_HebrewGrammarData();
                            if (isEdit)
                            {
                                hLL_HebrewGrammarDataFirstModel = db.HLL_HebrewGrammarData.Where(x => x.HebrewGrammarDataNo == i.ToString() && x.MasterTableId == _ModelObjHAD.GrammarId).FirstOrDefault();
                            }
                            if (isEdit && hLL_HebrewGrammarDataFirstModel != null)
                            {
                                hLL_HebrewGrammarDataFirstModel.HebrewGrammarData = Item;
                                db.Entry(hLL_HebrewGrammarDataFirstModel).State = EntityState.Modified;
                                db.SaveChanges();
                                hLL_HebrewGrammarData.HebrewGrammarDataId = hLL_HebrewGrammarDataFirstModel.HebrewGrammarDataId;
                            }
                            else
                            {
                                hLL_HebrewGrammarData.HebrewGrammarDataId = EntityConfig.getnewid("HLL_HebrewGrammarData");
                                hLL_HebrewGrammarData.MasterTableId = _ModelObjHAD.GrammarId;
                                hLL_HebrewGrammarData.HebrewGrammarDataNo = i.ToString();
                                hLL_HebrewGrammarData.HebrewGrammarData = Item;
                                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_HebrewGrammarDataModel, HLL_HebrewGrammarData>(); });
                                HLL_HebrewGrammarData DataModel = AutoMapper.Mapper.Map<HLL_HebrewGrammarDataModel, HLL_HebrewGrammarData>(hLL_HebrewGrammarData);
                                db.HLL_HebrewGrammarData.Add(DataModel);
                                db.SaveChanges();
                            }

                            //* Add Video, Image, Sound *//
                            MediaVideoController _objMediaVideo = new MediaVideoController();
                            MediaPicturesController _objMediaImage = new MediaPicturesController();
                            if (_hLL_GrammarModel.ImgVdofile[i] != null)
                            {
                                string extension = Path.GetExtension(_hLL_GrammarModel.ImgVdofile[i].FileName).ToLower();
                                string[] extImage = { ".gif", ".png", ".jpg", ".jpeg" }; string[] extVideo = { ".mp4", ".mov", ".avi" };
                                if (extVideo.Contains(extension))
                                {

                                    _objMediaImage.checkAndDelete(hLL_HebrewGrammarData.HebrewGrammarDataId);

                                    _ModelObjMedVid.MasterTableId = hLL_HebrewGrammarData.HebrewGrammarDataId;
                                    _ModelObjMedVid.Videofile = _hLL_GrammarModel.ImgVdofile[i];
                                    _ModelObjMedVid.TableRef = "HebrewGrammarData";
                                    _objMediaVideo.SetVideo(_ModelObjMedVid);
                                }
                                if (extImage.Contains(extension))
                                {

                                    _objMediaVideo.checkAndDelete(hLL_HebrewGrammarData.HebrewGrammarDataId);

                                    _ModelObjMedPic.MasterTableId = hLL_HebrewGrammarData.HebrewGrammarDataId;
                                    _ModelObjMedPic.Imagefile = _hLL_GrammarModel.ImgVdofile[i];
                                    _ModelObjMedPic.TableRef = "HebrewGrammarData";
                                    _objMediaImage.SetPicture(_ModelObjMedPic);
                                }

                            }
                            if (_hLL_GrammarModel.Soundfile[i] != null)
                            {
                                string extension = Path.GetExtension(_hLL_GrammarModel.Soundfile[i].FileName).ToLower();
                                string[] extAudio = { ".mp3" };
                                if (extAudio.Contains(extension))
                                {
                                    MediaSoundController _objMediaSound = new MediaSoundController();
                                    _ModelObjMedSound.MasterTableId = hLL_HebrewGrammarData.HebrewGrammarDataId;
                                    _ModelObjMedSound.Soundfile = _hLL_GrammarModel.Soundfile[i];
                                    _ModelObjMedSound.TableRef = "HebrewGrammarData";
                                    _objMediaSound.SetSound(_ModelObjMedSound);
                                }

                            }
                            i++;
                        }
                    }
                    return JavaScript("window.location = '/Definition/DefinitionList'");
                }
            }
            catch (Exception ex) { }
            return Content("Please Check blank fields");

        }



        private HLL_GrammarModel CheckAndSetGrammerData(HLL_GrammarModel HLL_GrammarModel, int operationType)
        {
            HLL_GrammarModel HLL_GrammarModelData = new HLL_GrammarModel();
            try
            {

                if (operationType == 0)
                {
                    HLL_GrammarModelData.GrammarId = EntityConfig.getnewid("HLL_Grammar");
                    HLL_GrammarModelData.HebrewGrammar = HLL_GrammarModel.HebrewGrammar;
                    HLL_GrammarModelData.EnglishGrammar = HLL_GrammarModel.EnglishGrammar;
                    AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_GrammarModel, HLL_Grammar>(); });
                    HLL_Grammar DataModel = AutoMapper.Mapper.Map<HLL_GrammarModel, HLL_Grammar>(HLL_GrammarModelData);
                    db.HLL_Grammar.Add(DataModel);
                    db.SaveChanges();
                }
                if (operationType == 1)
                {
                    var CheckData = db.HLL_Grammar.Find(HLL_GrammarModel.GrammarId);
                    if (CheckData != null)
                    {
                        CheckData.EnglishGrammar = HLL_GrammarModel.EnglishGrammar;
                        CheckData.HebrewGrammar = HLL_GrammarModel.HebrewGrammar;
                        CheckData.UpdatedDate = HLL_GrammarModel.UpdatedDate;
                        db.Entry(CheckData).State = EntityState.Modified;
                        db.SaveChanges();
                        AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Grammar, HLL_GrammarModel>(); });
                        HLL_GrammarModelData = AutoMapper.Mapper.Map<HLL_Grammar, HLL_GrammarModel>(CheckData);
                    }
                }
            }
            catch (Exception ex) { }
            return HLL_GrammarModelData;

        }

        public PartialViewResult GetCorrectAnswerView(string CorrectAnswerNo, string LessonId, bool isNext)
        {
            HLL_HebrewApplicationDataModel DataModel = new HLL_HebrewApplicationDataModel();
            if (CorrectAnswerNo != null && LessonId != null)
            {
                if (isNext)
                {
                    CorrectAnswerNo = (Convert.ToInt32(CorrectAnswerNo) + 1).ToString();
                }
                var AppData = CheckAndSetLessonData(CorrectAnswerNo, LessonId);
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_HebrewApplicationData, HLL_HebrewApplicationDataModel>(); });
                DataModel = AutoMapper.Mapper.Map<HLL_HebrewApplicationData, HLL_HebrewApplicationDataModel>(CheckAndSetLessonData(CorrectAnswerNo, LessonId));
                ModelState.Clear();
            }
            return PartialView("HLL_HebrewApplicationDataCorrectAnswer_PartialView", DataModel);
        }

        private HLL_HebrewApplicationData CheckAndSetLessonData(string correctAnswerNo, string lessonId)
        {
            var lessonIdData = db.HLL_Application.Where(z => z.LessonId.Equals(lessonId)).FirstOrDefault();
            HLL_HebrewApplicationData hebrewApplicationData = new HLL_HebrewApplicationData();
            if (lessonIdData != null)
            {
                hebrewApplicationData = db.HLL_HebrewApplicationData.Where(z => z.MasterTableId.Equals(lessonIdData.ApplicationId) && z.HebrewApplicationDataNo.Equals(correctAnswerNo)).FirstOrDefault();
                if (hebrewApplicationData != null)
                {
                    return hebrewApplicationData;
                }
                else
                {
                    HLL_HebrewApplicationDataModel hLL_HebrewApplicationData = new HLL_HebrewApplicationDataModel();
                    hLL_HebrewApplicationData.HebrewApplicationDataId = EntityConfig.getnewid("HLL_HebrewApplicationData");
                    hLL_HebrewApplicationData.MasterTableId = lessonIdData.ApplicationId;
                    hLL_HebrewApplicationData.HebrewApplicationDataNo = correctAnswerNo;
                    AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_HebrewApplicationDataModel, HLL_HebrewApplicationData>(); });
                    HLL_HebrewApplicationData DataModel = AutoMapper.Mapper.Map<HLL_HebrewApplicationDataModel, HLL_HebrewApplicationData>(hLL_HebrewApplicationData);
                    db.HLL_HebrewApplicationData.Add(DataModel);
                    db.SaveChanges();
                    CheckAndSetLessonData(correctAnswerNo, lessonId);
                }
            }
            else
            {
                HLL_ApplicationModel hLL_Application = new HLL_ApplicationModel();
                hLL_Application.ApplicationId = EntityConfig.getnewid("HLL_Application");
                hLL_Application.LessonId = lessonId;
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_ApplicationModel, HLL_Application>(); });
                HLL_Application DataModel = AutoMapper.Mapper.Map<HLL_ApplicationModel, HLL_Application>(hLL_Application);
                db.HLL_Application.Add(DataModel);
                db.SaveChanges();
                CheckAndSetLessonData(correctAnswerNo, lessonId);
            }
            return hebrewApplicationData;
        }

        // GET: Application/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Create(string GrammarId = "0")
            return View("Create");
            //HLL_Application hLL_Application = await db.HLL_Application.FindAsync(id);
            //if (hLL_Application == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(hLL_Application);
        }

        // POST: Application/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ApplicationId,LessonId,Title,Sentence,ImgUrl,VideoUrl,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_Application hLL_Application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hLL_Application).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hLL_Application);
        }

        // GET: Application/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_HebrewApplicationData hLL_Application = await db.HLL_HebrewApplicationData.FindAsync(id);
            if (hLL_Application == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Application);
        }

        // POST: Application/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            HLL_CommonField obj = new HLL_CommonField();
            HLL_HebrewApplicationData _Obj_HebrewApplicationData = await db.HLL_HebrewApplicationData.FindAsync(id);
            _Obj_HebrewApplicationData.IsDelete = true;
            _Obj_HebrewApplicationData.UpdatedDate = obj.UpdatedDate;
            db.Entry(_Obj_HebrewApplicationData).State = EntityState.Modified;
            //db.HLL_Application.Remove(hLL_Application);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }




        public async Task<ActionResult> GrammarSelector()
        {
            List<SelectListItem> GrammarTopicsAvailablelist = new List<SelectListItem>();
            var GrammarList = await db.HLL_Grammar.Where(x => x.IsDelete == false).ToListAsync();
            ViewData["GrammarTopicsinLesson"] = new List<SelectListItem>();
            foreach (var Item in GrammarList)
            {
                GrammarTopicsAvailablelist.Add(new SelectListItem() { Value = Item.GrammarId, Text = Item.HebrewGrammar + " | " + Item.EnglishGrammar });
            }


            ViewData["GrammarTopicsAvailable"] = GrammarTopicsAvailablelist;
            return View();
        }

        public async Task<ActionResult> setLessonGrammarDropDwonList(string LessonId, string grammarId)
        {
            var _ObjHLL_LessonsGrammarTab = db.HLL_LessonsGrammar.Where(x => x.IsDelete == false && x.fkLessonId == LessonId && x.fkGrammarId == grammarId).Select(z => new { z.GrammarPoint1, z.GrammarPoint2, z.GrammarPoint3, z.GrammarPoint4 }).FirstOrDefault();
            if (_ObjHLL_LessonsGrammarTab == null)
            {
               var _ObjHLL_LessonsGrammarMod = new HLL_LessonsGrammarModel();
                return Json(_ObjHLL_LessonsGrammarMod, JsonRequestBehavior.AllowGet);
            }
            return Json(_ObjHLL_LessonsGrammarTab, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> setDropDwonList(string LessonId)
        {
            var _ObjHLL_LessonsGrammarTab = db.HLL_LessonsGrammar.Where(x => x.IsDelete == false && x.fkLessonId == LessonId).ToList();
            var objLessonsGrammarList = _ObjHLL_LessonsGrammarTab.Select(z => z.fkGrammarId).ToList();
            var LessonsGrammarInLessonList = db.HLL_Grammar.Where(z => objLessonsGrammarList.Contains(z.GrammarId)).ToList();

            ViewBag.InLessonList = db.HLL_Grammar.Where(z => objLessonsGrammarList.Contains(z.GrammarId)).Select(x =>
                                 new SelectListItem()
                                 {
                                     Value = x.GrammarId,
                                     Text = x.HebrewGrammar + ", " + x.EnglishGrammar
                                 }).ToList();

            return Json(ViewBag.InLessonList, JsonRequestBehavior.AllowGet);
        }

        HLL_LessonsGrammarModel _ObjHLL_LessonsGrammar = new HLL_LessonsGrammarModel();
        public async Task<string> AddRemoveDataInLesson(int isAdd, HLL_LessonsGrammarModel hLL_LessonsGrammar)
        {
            string result = "Your Data is successfully Saved";
            _ObjHLL_LessonsGrammar = new HLL_LessonsGrammarModel();
            if (isAdd == 1)
            {
                _ObjHLL_LessonsGrammar.LessonsGrammarId = EntityConfig.getnewid("HLL_LessonsGrammar");
                _ObjHLL_LessonsGrammar.fkLessonId = hLL_LessonsGrammar.fkLessonId;
                _ObjHLL_LessonsGrammar.fkGrammarId = hLL_LessonsGrammar.fkGrammarId;
                _ObjHLL_LessonsGrammar.GrammarPoint1 = hLL_LessonsGrammar.GrammarPoint1;
                _ObjHLL_LessonsGrammar.GrammarPoint2 = hLL_LessonsGrammar.GrammarPoint2;
                _ObjHLL_LessonsGrammar.GrammarPoint3 = hLL_LessonsGrammar.GrammarPoint3;
                _ObjHLL_LessonsGrammar.GrammarPoint4 = hLL_LessonsGrammar.GrammarPoint4;


                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_LessonsGrammarModel, HLL_LessonsGrammar>(); });
                var LessonsGrammarTable = AutoMapper.Mapper.Map<HLL_LessonsGrammarModel, HLL_LessonsGrammar>(_ObjHLL_LessonsGrammar);
                db.HLL_LessonsGrammar.Add(LessonsGrammarTable);
                await db.SaveChangesAsync();
            }
            if (isAdd == 0)
            {
                HLL_LessonsGrammar hLL_LessonsGrammarLocal = await db.HLL_LessonsGrammar.Where(z => z.fkGrammarId == hLL_LessonsGrammar.fkGrammarId && z.fkLessonId == hLL_LessonsGrammar.fkLessonId).FirstOrDefaultAsync();
                if (hLL_LessonsGrammarLocal != null)
                {
                    db.HLL_LessonsGrammar.Remove(hLL_LessonsGrammarLocal);
                    await db.SaveChangesAsync();
                    result = "Your Data is successfully delete";
                }
                else { result = "Your Data is not found"; }

            }

            return result;
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
