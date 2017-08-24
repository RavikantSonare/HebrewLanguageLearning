using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HebrewLanguageLearning_Admin.DBContext;

namespace HebrewLanguageLearning_Admin.Controllers
{
    public class ApplicationController : Controller
    {
        private HLLDBContext db = new HLLDBContext();

        // GET: Application
        public ActionResult Index()
        {
            return View(db.HLL_Application.ToList());
        }

        // GET: Application/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Application hLL_Application = db.HLL_Application.Find(id);
            if (hLL_Application == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Application);
        }

        // GET: Application/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Application/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationId,Title,Sentence,ImgUrl,VideoUrl,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_Application hLL_Application)
        {
            if (ModelState.IsValid)
            {
                db.HLL_Application.Add(hLL_Application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hLL_Application);
        }

        // GET: Application/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Application hLL_Application = db.HLL_Application.Find(id);
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
        public ActionResult Edit([Bind(Include = "ApplicationId,Title,Sentence,ImgUrl,VideoUrl,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_Application hLL_Application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hLL_Application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hLL_Application);
        }

        // GET: Application/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Application hLL_Application = db.HLL_Application.Find(id);
            if (hLL_Application == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Application);
        }

        // POST: Application/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HLL_Application hLL_Application = db.HLL_Application.Find(id);
            db.HLL_Application.Remove(hLL_Application);
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
