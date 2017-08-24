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
    public class ExampleController : Controller
    {
        private HLLDBContext db = new HLLDBContext();

        // GET: Example
        public ActionResult Index()
        {
            return View(db.HLL_Example.ToList());
        }

        // GET: Example/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Example hLL_Example = db.HLL_Example.Find(id);
            if (hLL_Example == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Example);
        }

        // GET: Example/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Example/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HLL_ExampleModels hLL_Example)
        {
            if (ModelState.IsValid)
            {
                hLL_Example.ExampleId = EntityConfig.getnewid("HLL_Example");
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_ExampleModels, HLL_Example>(); });
                HLL_Example DataModel = AutoMapper.Mapper.Map<HLL_ExampleModels, HLL_Example>(hLL_Example);
                db.HLL_Example.Add(DataModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hLL_Example);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> CreateExample(HLL_ExampleModels hLL_Example)
        {
            if (ModelState.IsValid)
            {
                hLL_Example.ExampleId = EntityConfig.getnewid("HLL_Example");
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_ExampleModels, HLL_Example>(); });
                HLL_Example DataModel = AutoMapper.Mapper.Map<HLL_ExampleModels, HLL_Example>(hLL_Example);
                db.HLL_Example.Add(DataModel);
                db.SaveChanges();
                return new EmptyResult();
            }

            return new EmptyResult();
        }
        // GET: Example/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Example hLL_Example = db.HLL_Example.Find(id);
            if (hLL_Example == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Example);
        }

        // POST: Example/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExampleId,Title,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_Example hLL_Example)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hLL_Example).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hLL_Example);
        }

        // GET: Example/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Example hLL_Example = db.HLL_Example.Find(id);
            if (hLL_Example == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Example);
        }

        // POST: Example/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HLL_Example hLL_Example = db.HLL_Example.Find(id);
            db.HLL_Example.Remove(hLL_Example);
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
