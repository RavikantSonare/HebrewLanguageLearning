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
using HebrewLanguageLearning_Admin.GenericClasses;
using HebrewLanguageLearning_Admin.Models;
using System.IO;

namespace HebrewLanguageLearning_Admin.Controllers
{
    public class ApplicationController : Controller
    {
        private HLLDBContext db = new HLLDBContext();
        private List<HLL_Application> ApplicationData = new List<HLL_Application>();
        // GET: Application
        public async Task<ActionResult> Index()
        {
            List<HLL_HebrewApplicationDataModel> DataModelList = new List<HLL_HebrewApplicationDataModel>();
            try
            {
               
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_HebrewApplicationData, HLL_HebrewApplicationDataModel>(); });
                DataModelList = AutoMapper.Mapper.Map<List<HLL_HebrewApplicationData>, List<HLL_HebrewApplicationDataModel>>(await db.HLL_HebrewApplicationData.Where(x => x.IsDelete == false).ToListAsync());

                var SoundData = db.HLL_Media_Sound.Select(c => c.MasterTableId).ToList();
                var ImageData = db.HLL_Media_Pictures.Select(c => c.MasterTableId).ToList();
                var VideoData = db.HLL_Media_Video.Select(c => c.MasterTableId).ToList();
                int correctAnswerCounter = 0;
                DataModelList.ForEach(e =>
                {
                    correctAnswerCounter = 0;
                    e.LessonId = ReturnLesson(e.MasterTableId);
                    if (SoundData.Contains(e.HebrewApplicationDataId)) { e.Soundfile = "0"; }
                    if (ImageData.Contains(e.HebrewApplicationDataId)) { e.ImgVdofile = "1"; }
                    if (VideoData.Contains(e.HebrewApplicationDataId)) { e.ImgVdofile = "2"; }
                    if (e.CorrectAnswer1 != null) { correctAnswerCounter++; }
                    if (e.CorrectAnswer2 != null) { correctAnswerCounter++; }
                    if (e.CorrectAnswer3 != null) { correctAnswerCounter++; }
                    if (e.CorrectAnswer4 != null) { correctAnswerCounter++; }
                    if (e.CorrectAnswer5 != null) { correctAnswerCounter++; }
                    if (e.CorrectAnswer6 != null) { correctAnswerCounter++; }
                    if (e.CorrectAnswer7 != null) { correctAnswerCounter++; }
                    if (e.CorrectAnswer8 != null) { correctAnswerCounter++; }
                    if (e.CorrectAnswer9 != null) { correctAnswerCounter++; }
                    if (e.CorrectAnswer10 != null) { correctAnswerCounter++; }
                    e.CorrectAnswers = correctAnswerCounter.ToString();

                });

            }
            catch (Exception ex) { }
            ModelState.Clear();
            return View(DataModelList.OrderByDescending(x => x.HebrewApplicationDataId));
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

        private HLL_ApplicationModel set_ObjDataModelData(HLL_ApplicationModel _ObjDataModel)
        {
            _ObjDataModel.AppSentenceDynamicTextBox = new string[15];
            _ObjDataModel.Imgfile = new HttpPostedFileBase[15];
            _ObjDataModel.Videofile = new HttpPostedFileBase[15];
            _ObjDataModel.Soundfile = new HttpPostedFileBase[15];
            _ObjDataModel.ImgVdofile = new HttpPostedFileBase[15];
            return _ObjDataModel;
        }

        public async Task<ActionResult> Create(string Id = "0")
        {
            var LessonId = Id; TempData["isEdit"] = false;
            HLL_ApplicationModel _ObjDataModel = new HLL_ApplicationModel(); _ObjDataModel.DataCounter = 4;
            _ObjDataModel = set_ObjDataModelData(_ObjDataModel);
            if (LessonId != "0")
            {
                var ApplicationId = getAndSetApplication(LessonId).ApplicationId;
                TempData["isEdit"] = true;
                var objApplication = db.HLL_Application.Where(x => x.IsDelete == false).Where(p => p.ApplicationId == ApplicationId).FirstOrDefault();
                var objApplicationDataList = db.HLL_HebrewApplicationData.Where(x => x.IsDelete == false && x.MasterTableId == objApplication.ApplicationId).ToList();
                var ListData = objApplicationDataList.Select(y => y.HebrewApplicationDataId).ToList();
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Application, HLL_ApplicationModel>(); });
                _ObjDataModel = AutoMapper.Mapper.Map<HLL_Application, HLL_ApplicationModel>(objApplication);
                _ObjDataModel = set_ObjDataModelData(_ObjDataModel);
                var SoundDataList = db.HLL_Media_Sound.Where(x => (ListData.Contains(x.MasterTableId))).Select(c => c.MasterTableId).ToList();
                var ImageDataList = db.HLL_Media_Pictures.Where(x => (ListData.Contains(x.MasterTableId))).Select(c => c.MasterTableId).ToList();
                var VideoDataList = db.HLL_Media_Video.Where(x => (ListData.Contains(x.MasterTableId))).Select(c => c.MasterTableId).ToList();
                int i = 0, txtlenght = objApplicationDataList.Count() > 4 ? objApplicationDataList.Count() : 4;
                _ObjDataModel.DataCounter = txtlenght;
                foreach (var Item in objApplicationDataList)
                {

                    _ObjDataModel.AppSentenceDynamicTextBox[i] = Item.HebrewApplicationData;
                    i++;
                }


                ModelState.Clear();
            }
            return View(_ObjDataModel);
        }


        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetDataForm(string LessonId)
        {
            HLL_ApplicationModel DataModel = new HLL_ApplicationModel();
            try
            {
                var _ModelHLL_ApplicationObj = await db.HLL_Application.Where(x => x.LessonId.Equals(LessonId)).FirstOrDefaultAsync();
                List<HLL_HebrewApplicationData> _ModelHLL_HebrewApplicationDataObj = new List<HLL_HebrewApplicationData>();
                if (_ModelHLL_ApplicationObj != null)
                {
                    _ModelHLL_HebrewApplicationDataObj = await db.HLL_HebrewApplicationData.Where(x => x.MasterTableId.Equals(_ModelHLL_ApplicationObj.ApplicationId) && x.IsDelete == false).ToListAsync();
                }else
                {
                    _ModelHLL_ApplicationObj = new HLL_Application();
                }
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Application, HLL_ApplicationModel>(); });
                DataModel = AutoMapper.Mapper.Map<HLL_Application, HLL_ApplicationModel>(_ModelHLL_ApplicationObj);
                DataModel.AppSentenceDynamicTextBox = new string[15];
                DataModel.VideoUrl = new string[15];
                DataModel.ImgUrl = new string[15];
                DataModel.SoundUrl = new string[15];

                DataModel.Imgfile = new HttpPostedFileBase[15];
                DataModel.Videofile = new HttpPostedFileBase[15];
                DataModel.Soundfile = new HttpPostedFileBase[15];
                DataModel.ImgVdofile = new HttpPostedFileBase[15];
                int i = 0;


                foreach (var Item in db.HLL_HebrewApplicationData.Where(h => h.MasterTableId.Equals(_ModelHLL_ApplicationObj.ApplicationId) && h.IsDelete == false).ToList())
                {
                    DataModel.AppSentenceDynamicTextBox[i] = Item.HebrewApplicationData;
                    var SoundDataList = db.HLL_Media_Sound.Where(x => x.MasterTableId == Item.HebrewApplicationDataId).FirstOrDefault();
                    if (SoundDataList != null) { DataModel.SoundUrl[i] = "0"; }
                   
                    var ImageDataList = db.HLL_Media_Pictures.Where(x => x.MasterTableId == Item.HebrewApplicationDataId).FirstOrDefault();
                    if (ImageDataList != null) { DataModel.VideoUrl[i] = "1"; }
                    else
                    {
                        var VideoDataList = db.HLL_Media_Video.Where(x => x.MasterTableId == Item.HebrewApplicationDataId).FirstOrDefault();
                        if (VideoDataList != null) { DataModel.VideoUrl[i] = "2"; }
                    }
                    
                    i++;
                }
                DataModel.DataCounter = i > 4 ? i : 4;

            }
            catch (Exception ex) { }
            ModelState.Clear();
            return PartialView("_HLL_Application_Data_PartialView", DataModel);
        }
        // POST: Application/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateHebrewAppData(HLL_HebrewApplicationDataModel hLL_HebrewApplicationDataModel)
        {

            if (ModelState.IsValid)
            {
                /* Add Correct Answer */
                try
                {
                    var hebrewApplicationData = db.HLL_HebrewApplicationData.Where(z => z.HebrewApplicationDataId.Equals(hLL_HebrewApplicationDataModel.HebrewApplicationDataId)).FirstOrDefault();
                    if (hebrewApplicationData != null)
                    {
                       if(!string.IsNullOrEmpty(hLL_HebrewApplicationDataModel.HebrewApplicationData)) hebrewApplicationData.HebrewApplicationData = hLL_HebrewApplicationDataModel.HebrewApplicationData;
                        hebrewApplicationData.CorrectAnswer1 = hLL_HebrewApplicationDataModel.CorrectAnswer1;
                        hebrewApplicationData.CorrectAnswer2 = hLL_HebrewApplicationDataModel.CorrectAnswer2;
                        hebrewApplicationData.CorrectAnswer3 = hLL_HebrewApplicationDataModel.CorrectAnswer3;
                        hebrewApplicationData.CorrectAnswer4 = hLL_HebrewApplicationDataModel.CorrectAnswer4;
                        hebrewApplicationData.CorrectAnswer5 = hLL_HebrewApplicationDataModel.CorrectAnswer5;
                        hebrewApplicationData.CorrectAnswer6 = hLL_HebrewApplicationDataModel.CorrectAnswer6;
                        hebrewApplicationData.CorrectAnswer7 = hLL_HebrewApplicationDataModel.CorrectAnswer7;
                        hebrewApplicationData.CorrectAnswer8 = hLL_HebrewApplicationDataModel.CorrectAnswer8;
                        hebrewApplicationData.CorrectAnswer9 = hLL_HebrewApplicationDataModel.CorrectAnswer9;
                        hebrewApplicationData.CorrectAnswer10 = hLL_HebrewApplicationDataModel.CorrectAnswer10;
                        hebrewApplicationData.UpdatedDate = hLL_HebrewApplicationDataModel.UpdatedDate;
                        db.Entry(hebrewApplicationData).State = EntityState.Modified;
                        db.SaveChanges();

                        // return JavaScript("window.location = '/Application/Create'");
                        return Content("Success");
                    }
                }
                catch (Exception ex) { }
            }
            return Content("Sever Not responding");

        }
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
        public async Task<ActionResult> Create(HLL_ApplicationModel hLL_Application)
        {

            HLL_Media_VideoModels _ModelObjMedVid = new HLL_Media_VideoModels();
            HLL_Media_PicturesModels _ModelObjMedPic = new HLL_Media_PicturesModels();
            HLL_Media_SoundModels _ModelObjMedSound = new HLL_Media_SoundModels();
            int i = 0;

            dynamic isEdit = false;
            if (TempData["isEdit"] != null)
            {
                isEdit = TempData["isEdit"];
            }

            if (ModelState.IsValid || isEdit)
            {
                foreach (var Item in hLL_Application.AppSentenceDynamicTextBox)
                {

                    if (!string.IsNullOrEmpty(Item))
                    { /* Add Application */  /* Add Hebrew Application Data */
                        HLL_HebrewApplicationData _Obj_HebrewApplicationData = new HLL_HebrewApplicationData();
                        _Obj_HebrewApplicationData = CheckAndSetLessonData(Convert.ToString(i + 1), hLL_Application.LessonId);
                        hLL_Application.ApplicationId = _Obj_HebrewApplicationData.HebrewApplicationDataId;

                        _Obj_HebrewApplicationData.HebrewApplicationData = Item;
                        _Obj_HebrewApplicationData.UpdatedDate = _Obj_HebrewApplicationData.UpdatedDate;
                        db.Entry(_Obj_HebrewApplicationData).State = EntityState.Modified;
                        db.SaveChanges();

                        //* Add Video, Image, Sound *//
                        MediaVideoController _objMediaVideo = new MediaVideoController();
                        MediaPicturesController _objMediaImage = new MediaPicturesController();
                        if (hLL_Application.ImgVdofile[i] != null)
                        {
                            string extension = Path.GetExtension(hLL_Application.ImgVdofile[i].FileName).ToLower();
                            string[] extImage = { ".gif", ".png", ".jpg", ".jpeg" }; string[] extVideo = { ".mp4", ".mov", ".avi" };
                            if (extVideo.Contains(extension))
                            {

                                _objMediaImage.checkAndDelete(_Obj_HebrewApplicationData.HebrewApplicationDataId);
                                _ModelObjMedVid.MasterTableId = _Obj_HebrewApplicationData.HebrewApplicationDataId;
                                _ModelObjMedVid.Videofile = hLL_Application.ImgVdofile[i];
                                _ModelObjMedVid.TableRef = "HebrewApplicationData";
                                _objMediaVideo.SetVideo(_ModelObjMedVid);
                            }
                            if (extImage.Contains(extension))
                            {

                                _objMediaImage.checkAndDelete(_Obj_HebrewApplicationData.HebrewApplicationDataId);
                                _ModelObjMedPic.MasterTableId = _Obj_HebrewApplicationData.HebrewApplicationDataId;
                                _ModelObjMedPic.Imagefile = hLL_Application.ImgVdofile[i];
                                _ModelObjMedPic.TableRef = "HebrewApplicationData";
                                _objMediaImage.SetPicture(_ModelObjMedPic);
                            }

                        }
                        if (hLL_Application.Soundfile[i] != null)
                        {
                            string extension = Path.GetExtension(hLL_Application.Soundfile[i].FileName).ToLower();
                            string[] extAudio = { ".mp3" };
                            if (extAudio.Contains(extension))
                            {
                                MediaSoundController _objMediaSound = new MediaSoundController();

                                _ModelObjMedSound.MasterTableId = _Obj_HebrewApplicationData.HebrewApplicationDataId;
                                _ModelObjMedSound.Soundfile = hLL_Application.Soundfile[i];
                                _ModelObjMedSound.TableRef = "HebrewApplicationData";
                                _objMediaSound.SetSound(_ModelObjMedSound);
                            }

                        }
                        i++;
                    }
                }


                return JavaScript("  $('#frmloading').show(); window.location = '/Definition/DefinitionList'; $('#frmloading').show();");
            }

            return Content("Please Check blank fields");

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
                DataModel = AutoMapper.Mapper.Map<HLL_HebrewApplicationData, HLL_HebrewApplicationDataModel>(AppData);
                DataModel.CorrectAnswerNo = CorrectAnswerNo;
                ModelState.Clear();
            }
            return PartialView("HLL_HebrewApplicationDataCorrectAnswer_PartialView", DataModel);
        }



        private HLL_HebrewApplicationData CheckAndSetLessonData(string correctAnswerNo, string lessonId)
        {
            HLL_Application lessonIdData = getAndSetApplication(lessonId);
            HLL_HebrewApplicationData hebrewApplicationData = new HLL_HebrewApplicationData();
            if (lessonIdData != null)
            {
                hebrewApplicationData = getAndSetHebrewApplicationData(lessonIdData.ApplicationId, correctAnswerNo);
            }
            return hebrewApplicationData;
        }

        private HLL_Application getAndSetApplication(string lessonId)
        {
            HLL_Application applicationTable = new HLL_Application();
            HLL_ApplicationModel applicationModel = new HLL_ApplicationModel();
            applicationTable = db.HLL_Application.Where(z => z.LessonId.Equals(lessonId)).FirstOrDefault();
            if (applicationTable == null)
            {
                applicationModel.ApplicationId = EntityConfig.getnewid("HLL_Application");
                applicationModel.LessonId = lessonId;
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_ApplicationModel, HLL_Application>(); });
                applicationTable = AutoMapper.Mapper.Map<HLL_ApplicationModel, HLL_Application>(applicationModel);
                db.HLL_Application.Add(applicationTable);
                db.SaveChanges();
            }
            return applicationTable;
        }
        private HLL_HebrewApplicationData getAndSetHebrewApplicationData(string ApplicationId, string correctAnswerNo)
        {
            HLL_HebrewApplicationData hebrewApplicationDataTable = new HLL_HebrewApplicationData();
            HLL_HebrewApplicationDataModel hebrewApplicationDataModel = new HLL_HebrewApplicationDataModel();
            hebrewApplicationDataTable = db.HLL_HebrewApplicationData.Where(z => z.MasterTableId.Equals(ApplicationId) && z.IsDelete == false && z.HebrewApplicationDataNo == correctAnswerNo).FirstOrDefault();
            if (hebrewApplicationDataTable == null)
            {
                hebrewApplicationDataModel.HebrewApplicationDataId = EntityConfig.getnewid("HLL_HebrewApplicationData");
                hebrewApplicationDataModel.MasterTableId = ApplicationId;
                hebrewApplicationDataModel.HebrewApplicationDataNo = correctAnswerNo;
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_HebrewApplicationDataModel, HLL_HebrewApplicationData>(); });
                hebrewApplicationDataTable = AutoMapper.Mapper.Map<HLL_HebrewApplicationDataModel, HLL_HebrewApplicationData>(hebrewApplicationDataModel);
                db.HLL_HebrewApplicationData.Add(hebrewApplicationDataTable);
                db.SaveChanges();
            }
            return hebrewApplicationDataTable;
        }
        // GET: Application/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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
