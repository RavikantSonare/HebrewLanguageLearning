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
using System.Threading.Tasks;

namespace HebrewLanguageLearning_Admin.Controllers
{
    public class StudentsInfoController : Controller
    {
        private HLLDBContext db = new HLLDBContext();

        // GET: StudentsInfo
        public ActionResult Index()
        {
            // Just For The Server Testing
            var S = Session.Timeout;// = 60;
            ViewBag.SessionTO = S.ToString();   //XmlSetter.LogError(S.ToString());

            AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_StudentsInfo, HLL_StudentsInfoModel>(); });
            List<HLL_StudentsInfoModel> DataModel = AutoMapper.Mapper.Map<List<HLL_StudentsInfo>, List<HLL_StudentsInfoModel>>(db.HLL_StudentsInfo.ToList());
            var StudentIdList = DataModel.Select(p => p.StudentsId).ToList();
            var ImageList = db.HLL_Media_Pictures.Where(x => StudentIdList.Contains(x.MasterTableId)).ToList();
            DataModel.ForEach(z => z.ImgUrl = ImageList.Where(p => p.MasterTableId == z.StudentsId).FirstOrDefault() == null ? "" : ImageList.Where(p => p.MasterTableId == z.StudentsId).FirstOrDefault().ImgUrl);
            return View(DataModel);
        }

        // GET: StudentsInfo/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_StudentsInfo hLL_StudentsInfo = db.HLL_StudentsInfo.Find(id);
            if (hLL_StudentsInfo == null)
            {
                return HttpNotFound();
            }
            return View(hLL_StudentsInfo);
        }

        // GET: StudentsInfo/Create
        public ActionResult Create()
        {
            return View();
        }
        MediaPicturesController _objMediaImage = new MediaPicturesController();
        HLL_Media_PicturesModels _ModelObjMedPic = new HLL_Media_PicturesModels();
        // POST: StudentsInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create(HLL_StudentsInfoModel hLL_StudentsInfo)  /* [Bind(Include = "FName,LName,UserName,Password,City,State,Country,Zip,Address,Telephone,EmailId,ImgUrl")]*/
        {
            if (ModelState.IsValid)
            {
                hLL_StudentsInfo.StudentsId = EntityConfig.getnewid("HLL_StudentsInfo");
                if (hLL_StudentsInfo.Imagefile != null)
                {
                    _ModelObjMedPic.MasterTableId = hLL_StudentsInfo.StudentsId;
                    _ModelObjMedPic.Imagefile = hLL_StudentsInfo.Imagefile;
                    _ModelObjMedPic.TableRef = "StudentsInfo";
                    _objMediaImage.SetPicture(_ModelObjMedPic);
                }
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_StudentsInfoModel, HLL_StudentsInfo>(); });
                HLL_StudentsInfo DataModel = AutoMapper.Mapper.Map<HLL_StudentsInfoModel, HLL_StudentsInfo>(hLL_StudentsInfo);

                db.HLL_StudentsInfo.Add(DataModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(hLL_StudentsInfo);
        }

        // GET: StudentsInfo/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_StudentsInfo hLL_StudentsInfo = db.HLL_StudentsInfo.Find(id);

            AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_StudentsInfo, HLL_StudentsInfoModel>(); });
            HLL_StudentsInfoModel DataModel = AutoMapper.Mapper.Map<HLL_StudentsInfo, HLL_StudentsInfoModel>(db.HLL_StudentsInfo.Find(id));

            if (DataModel == null)
            {
                return HttpNotFound();
            }
            return View(DataModel);
        }

        // POST: StudentsInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(HLL_StudentsInfoModel hLL_StudentsInfo)
        {
           
            if (ModelState.IsValid)
            {

                if (hLL_StudentsInfo.Imagefile != null)
                {
                    _ModelObjMedPic.MasterTableId = hLL_StudentsInfo.StudentsId;
                    _ModelObjMedPic.Imagefile = hLL_StudentsInfo.Imagefile;
                    _ModelObjMedPic.TableRef = "StudentsInfo";
                    _objMediaImage.SetPicture(_ModelObjMedPic);
                }
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_StudentsInfoModel, HLL_StudentsInfo>(); });
                HLL_StudentsInfo DataModel = AutoMapper.Mapper.Map<HLL_StudentsInfoModel, HLL_StudentsInfo>(hLL_StudentsInfo);
                db.Entry(DataModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hLL_StudentsInfo);
        }

        // GET: StudentsInfo/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_StudentsInfo hLL_StudentsInfo = db.HLL_StudentsInfo.Find(id);
            if (hLL_StudentsInfo == null)
            {
                return HttpNotFound();
            }
            return View(hLL_StudentsInfo);
        }

        // POST: StudentsInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HLL_StudentsInfo hLL_StudentsInfo = db.HLL_StudentsInfo.Find(id);
            db.HLL_StudentsInfo.Remove(hLL_StudentsInfo);
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
