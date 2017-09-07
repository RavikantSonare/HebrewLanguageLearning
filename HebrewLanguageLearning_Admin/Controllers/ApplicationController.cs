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

namespace HebrewLanguageLearning_Admin.Controllers
{
    public class ApplicationController : Controller
    {
        private HLLDBContext db = new HLLDBContext();

        // GET: Application
        public async Task<ActionResult> Index()
        {
            return View(await db.HLL_Application.ToListAsync());
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetDataForm(string LessonId)
        {
            HLL_ApplicationModel DataModel = new HLL_ApplicationModel();
            try
            {
                var _ModelObj = await db.HLL_Application.Where(x => x.LessonId.Equals(LessonId)).FirstOrDefaultAsync();
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Application, HLL_ApplicationModel>(); });
                DataModel = AutoMapper.Mapper.Map<HLL_Application, HLL_ApplicationModel>(_ModelObj);
                DataModel.AppSentenceDynamicTextBox = new string[15];
                DataModel.VideoUrl = new string[15];
                DataModel.ImgUrl = new string[15];
                DataModel.SoundUrl = new string[15];
                int i = 0;
                foreach (var Item in db.HLL_HebrewApplicationData.Where(h => h.MasterTableId.Equals(_ModelObj.ApplicationId)).ToList())
                {
                    DataModel.AppSentenceDynamicTextBox[i] = Item.HebrewApplicationData;
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
            var httpPostedFile = Request.Files[0];
            if (ModelState.IsValid)
            {
                /* Add Application */

                hLL_Application.ApplicationId = EntityConfig.getnewid("HLL_Application");
                hLL_Application.LessonId = hLL_Application.LessonId;
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_ApplicationModel, HLL_Application>(); });
                HLL_Application DataModel = AutoMapper.Mapper.Map<HLL_ApplicationModel, HLL_Application>(hLL_Application);
                db.HLL_Application.Add(DataModel);
                db.SaveChanges();

                /* Add Hebrew Application Data */

                HLL_HebrewApplicationDataModel _ModelObjHAD = new HLL_HebrewApplicationDataModel();
                foreach (var Item in hLL_Application.AppSentenceDynamicTextBox)
                {
                    if (!string.IsNullOrEmpty(Item))
                    {
                        _ModelObjHAD.HebrewApplicationDataId = EntityConfig.getnewid("HLL_HebrewApplicationData");
                        _ModelObjHAD.MasterTableId = hLL_Application.ApplicationId;
                        _ModelObjHAD.HebrewApplicationData = Item;
                        AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_HebrewApplicationDataModel, HLL_HebrewApplicationData>(); });
                        HLL_HebrewApplicationData DataModelHAD = AutoMapper.Mapper.Map<HLL_HebrewApplicationDataModel, HLL_HebrewApplicationData>(_ModelObjHAD);
                        db.HLL_HebrewApplicationData.Add(DataModelHAD);
                        db.SaveChanges();
                    }
                }




                ///* Add Audio */
                //SoundController _obj = new SoundController();
                //HLL_SoundModels _ModelObj = new HLL_SoundModels();
                //_ModelObj.DicEntId = hLL_DictionaryEntries.DictionaryEntriesId;
                //_ModelObj.Title = hLL_DictionaryEntries.SoundTitle;
                //_ModelObj.AudioUrl = hLL_DictionaryEntries.SoundUrl.Substring(22);

                //_obj.CreateAudio(_ModelObj);

                ///* Add Definition */
                //DefinitionController _objDef = new DefinitionController();
                //HLL_DefinitionModel _ModelObjDef = new HLL_DefinitionModel();
                //foreach (var Item in hLL_DictionaryEntries.DicDefinitionDynamicTextBox)
                //{
                //    if (!string.IsNullOrEmpty(Item))
                //    {
                //        _ModelObjDef.DicEntId = hLL_DictionaryEntries.DictionaryEntriesId;
                //        _ModelObjDef.Title = Item;
                //        _objDef.Create(_ModelObjDef);
                //    }
                //}




                return JavaScript("window.location = '/Definition/DefinitionList'");
            }

            return Content("Please Upload Sound file in MP3 format");
            //return View(hLL_DictionaryEntries);


            //if (ModelState.IsValid)
            //{
            //    db.HLL_Application.Add(hLL_Application);
            //    await db.SaveChangesAsync();
            //    return RedirectToAction("Index");
            //}

            //  return View(hLL_Application);
        }
        public PartialViewResult GetCorrectAnswerView(string CorrectAnswerNo, string LessonId, bool isNext)
        {
            HLL_HebrewApplicationDataModel DataModel=new HLL_HebrewApplicationDataModel();
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
            HLL_Application hLL_Application = await db.HLL_Application.FindAsync(id);
            if (hLL_Application == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Application);
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
            HLL_Application hLL_Application = await db.HLL_Application.FindAsync(id);
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
            HLL_Application hLL_Application = await db.HLL_Application.FindAsync(id);
            db.HLL_Application.Remove(hLL_Application);
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
