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

        public async Task<ActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetDataForm(string LessonId)
        {
            HLL_ApplicationModel DataModel = new HLL_ApplicationModel();
            try
            {
                var _ModelHLL_ApplicationObj = await db.HLL_Application.Where(x => x.LessonId.Equals(LessonId)).FirstOrDefaultAsync();
                var _ModelHLL_HebrewApplicationDataObj = await db.HLL_HebrewApplicationData.Where(x => x.MasterTableId.Equals(_ModelHLL_ApplicationObj.ApplicationId)).ToListAsync();

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
                    DataModel.SoundUrl[i] = "0";
                    DataModel.ImgUrl[i] = "1";
                    DataModel.VideoUrl[i] = "2";

                    // DataModel.AppSentenceDynamicTextBox[i] = db.HLL_HebrewApplicationData.Where(h => h.MasterTableId.Equals(_ModelObj.ApplicationId)).ToList()

                    i++;
                }

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
                        hebrewApplicationData.HebrewApplicationData = hLL_HebrewApplicationDataModel.HebrewApplicationData;
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

            if (ModelState.IsValid)
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
                        if (hLL_Application.ImgVdofile[i] != null)
                        {
                            string extension = Path.GetExtension(hLL_Application.ImgVdofile[i].FileName).ToLower();
                            string[] extImage = { ".gif", ".png", ".jpg", ".jpeg" }; string[] extVideo = { ".mp4", ".mov", ".avi" };
                            if (extVideo.Contains(extension))
                            {
                                MediaVideoController _objMediaVideo = new MediaVideoController();

                                _ModelObjMedVid.MasterTableId = _Obj_HebrewApplicationData.HebrewApplicationDataId;
                                _ModelObjMedVid.Videofile = hLL_Application.ImgVdofile[i];
                                _ModelObjMedVid.TableRef = "HebrewApplicationData";
                                _objMediaVideo.SetVideo(_ModelObjMedVid);
                            }
                            if (extImage.Contains(extension))
                            {
                                MediaPicturesController _objMediaImage = new MediaPicturesController();

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


                return JavaScript("window.location = '/Definition/DefinitionList'");
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
                DataModel = AutoMapper.Mapper.Map<HLL_HebrewApplicationData, HLL_HebrewApplicationDataModel>(CheckAndSetLessonData(CorrectAnswerNo, LessonId));
                ModelState.Clear();
            }
            return PartialView("HLL_HebrewApplicationDataCorrectAnswer_PartialView", DataModel);
        }

        private void applicationCreation(string lessonId)
        {
            HLL_ApplicationModel hLL_Application = new HLL_ApplicationModel();
            hLL_Application.ApplicationId = EntityConfig.getnewid("HLL_Application");
            hLL_Application.LessonId = lessonId;
            AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_ApplicationModel, HLL_Application>(); });
            HLL_Application DataModel = AutoMapper.Mapper.Map<HLL_ApplicationModel, HLL_Application>(hLL_Application);
            db.HLL_Application.Add(DataModel);
            db.SaveChanges();
        }
        private void applicationDataCreation(string correctAnswerNo, string lessonId)
        {
            HLL_HebrewApplicationDataModel hLL_HebrewApplicationData = new HLL_HebrewApplicationDataModel();
            hLL_HebrewApplicationData.HebrewApplicationDataId = EntityConfig.getnewid("HLL_HebrewApplicationData");
            hLL_HebrewApplicationData.MasterTableId = lessonId;
            hLL_HebrewApplicationData.HebrewApplicationDataNo = correctAnswerNo;
            AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_HebrewApplicationDataModel, HLL_HebrewApplicationData>(); });
            HLL_HebrewApplicationData DataModel = AutoMapper.Mapper.Map<HLL_HebrewApplicationDataModel, HLL_HebrewApplicationData>(hLL_HebrewApplicationData);
            db.HLL_HebrewApplicationData.Add(DataModel);
            db.SaveChanges();
        }
        private HLL_HebrewApplicationData CheckAndSetLessonData(string correctAnswerNo, string lessonId)
        {
            HLL_Application lessonIdData = getApplicationModelData(lessonId);
            HLL_HebrewApplicationData hebrewApplicationData = new HLL_HebrewApplicationData();
            if (lessonIdData != null)
            {
                hebrewApplicationData = db.HLL_HebrewApplicationData.Where(z => z.MasterTableId.Equals(lessonIdData.ApplicationId) && z.HebrewApplicationDataNo.Equals(correctAnswerNo)).FirstOrDefault();
                if (hebrewApplicationData != null)
                {
                    applicationDataCreation(correctAnswerNo, lessonIdData.ApplicationId);
                    return hebrewApplicationData;
                }
                else
                {

                    CheckAndSetLessonData(correctAnswerNo, lessonId);
                }
            }
            else
            {
                applicationCreation(lessonId);
                lessonIdData = getApplicationModelData(lessonId);
                if (lessonIdData != null)
                {
                    hebrewApplicationData = db.HLL_HebrewApplicationData.Where(z => z.MasterTableId.Equals(lessonIdData.ApplicationId) && z.HebrewApplicationDataNo.Equals(correctAnswerNo)).FirstOrDefault();
                    if (hebrewApplicationData != null)
                    {
                        return hebrewApplicationData;
                    }
                    else
                    {
                        applicationDataCreation(correctAnswerNo, lessonIdData.ApplicationId);
                        hebrewApplicationData = db.HLL_HebrewApplicationData.Where(z => z.MasterTableId.Equals(lessonIdData.ApplicationId) && z.HebrewApplicationDataNo.Equals(correctAnswerNo)).FirstOrDefault();

                        return hebrewApplicationData;
                    }
                }

            }
            return hebrewApplicationData;
        }

        private HLL_Application getApplicationModelData(string lessonId)
        {
            return db.HLL_Application.Where(z => z.LessonId.Equals(lessonId)).FirstOrDefault();
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
