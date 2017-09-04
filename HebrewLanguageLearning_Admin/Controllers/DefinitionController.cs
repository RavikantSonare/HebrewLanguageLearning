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

namespace HebrewLanguageLearning_Admin.Controllers
{
    public class definitionData
    {
        private static HLLDBContext db = new HLLDBContext();
        internal static List<HLL_PicturesModels> Get_HLL_PictureList(string definitionId)
        {
            try
            {
                var HLL_Picture = db.HLL_Pictures.Where(x => x.DefinitionId.Equals(definitionId)).ToList();
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Pictures, HLL_PicturesModels>(); });
                return AutoMapper.Mapper.Map<List<HLL_Pictures>, List<HLL_PicturesModels>>(HLL_Picture);
            }
            catch { }
            return null;
        }

        internal static List<HLL_SemanticDomainModels> Get_HLL_SemanticDomainsList(string definitionId)
        {
            try
            {
                var HLL_SemanticDomain = db.HLL_SemanticDomain.Where(x => x.DefinitionId.Equals(definitionId)).ToList();
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_SemanticDomain, HLL_SemanticDomainModels>(); });
                return AutoMapper.Mapper.Map<List<HLL_SemanticDomain>, List<HLL_SemanticDomainModels>>(HLL_SemanticDomain);
            }
            catch { }
            return null;
        }

        internal static List<HLL_ExampleModels> Get_HLL_ExampleList(string definitionId)
        {
            try
            {
                var HLL_Example = db.HLL_Example.Where(x => x.DefinitionId.Equals(definitionId)).ToList();
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Example, HLL_ExampleModels>(); });
                return AutoMapper.Mapper.Map<List<HLL_Example>, List<HLL_ExampleModels>>(HLL_Example);
            }
            catch { }
            return null;
        }
    }
    public class DefinitionController : Controller
    {
        private HLLDBContext db = new HLLDBContext();

        // GET: Definition
        public ActionResult Index()
        {
            return View(db.HLL_Definition.ToList());
        }

        // GET: Definition/Details/5

        public ActionResult DefinitionList()
        {
            List<HLL_DefinitionModel> Def_Obj = new List<HLL_DefinitionModel>();
            var hLL_Definition = db.HLL_Definition.ToList();

            foreach (var item in hLL_Definition)
            {
                Def_Obj.Add(new HLL_DefinitionModel
                {
                    DefinitionId = item.DefinitionId,
                    Title = item.Title,
                    PictureList = definitionData.Get_HLL_PictureList(item.DefinitionId),
                    SemanticDomainsList = definitionData.Get_HLL_SemanticDomainsList(item.DefinitionId),
                    ExampleList = definitionData.Get_HLL_ExampleList(item.DefinitionId),
                });

            }

            return View(Def_Obj);
        }

        //[ChildActionOnly]
        public PartialViewResult GetDefinitionView(string DefinitionId,string Next)
        {
            if (Next == "1") { DefinitionId = GetDefinitionViewNext(DefinitionId); }
            HLL_DefinitionModel Def_Obj = new HLL_DefinitionModel();
            var hLL_Definition = db.HLL_Definition.Where(x => x.DefinitionId.Equals(DefinitionId)).FirstOrDefault();

            AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Definition, HLL_DefinitionModel>(); });
            Def_Obj = AutoMapper.Mapper.Map<HLL_Definition, HLL_DefinitionModel>(hLL_Definition);

            ////new List<HLL_PicturesModels>() { new HLL_PicturesModels { Title = "Pic_1" }, new HLL_PicturesModels { Title = "Pic_2" }, new HLL_PicturesModels { Title = "Pic_3" }, new HLL_PicturesModels { Title = "Pic_4" } };
            //List< HLL_SemanticDomainModels > SemanticDomainsList = new List<HLL_SemanticDomainModels>() { new HLL_SemanticDomainModels { Title = "Semantic_Domain_1" }, new HLL_SemanticDomainModels { Title = "Semantic_Domain_2" }, new HLL_SemanticDomainModels { Title = "Semantic_Domain_3" } };
            //List<HLL_ExampleModels> ExampleList = new List<HLL_ExampleModels>() { new HLL_ExampleModels { Title = "Example_1" }, new HLL_ExampleModels { Title = "Example_2" } };

            Def_Obj.PictureList = definitionData.Get_HLL_PictureList(DefinitionId);
            Def_Obj.SemanticDomainsList = definitionData.Get_HLL_SemanticDomainsList(DefinitionId);
            Def_Obj.ExampleList = definitionData.Get_HLL_ExampleList(DefinitionId);
            ModelState.Clear();
            return PartialView("_HLL_Definition_PartialView", Def_Obj);
        }

        public string GetDefinitionViewNext(string DefinitionId)
        {

            var next = db.HLL_Definition.ToList().SkipWhile(obj => obj.DefinitionId != DefinitionId).Skip(1).FirstOrDefault();
            if (next != null)
            {
                DefinitionId = next.DefinitionId;
            }

            return DefinitionId;

        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Definition hLL_Definition = db.HLL_Definition.Find(id);
            if (hLL_Definition == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Definition);
        }

        // GET: Definition/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Definition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HLL_DefinitionModel hLL_Definition)
        {
            if (ModelState.IsValid)
            {
                hLL_Definition.DefinitionId = EntityConfig.getnewid("HLL_Definition");
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_DefinitionModel, HLL_Definition>(); });
                HLL_Definition DataModel = AutoMapper.Mapper.Map<HLL_DefinitionModel, HLL_Definition>(hLL_Definition);
                db.HLL_Definition.Add(DataModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hLL_Definition);
        }

        // GET: Definition/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Definition hLL_Definition = db.HLL_Definition.Find(id);
            if (hLL_Definition == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Definition);
        }

        // POST: Definition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DefinitionId,Title,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_Definition hLL_Definition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hLL_Definition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hLL_Definition);
        }

        // GET: Definition/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Definition hLL_Definition = db.HLL_Definition.Find(id);
            if (hLL_Definition == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Definition);
        }

        // POST: Definition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HLL_Definition hLL_Definition = db.HLL_Definition.Find(id);
            db.HLL_Definition.Remove(hLL_Definition);
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
