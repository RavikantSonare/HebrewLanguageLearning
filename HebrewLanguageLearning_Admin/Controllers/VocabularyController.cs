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
    public class VocabularyController : Controller
    {
        private HLLDBContext db = new HLLDBContext();

        // GET: Vocabulary
        public ActionResult Index()
        {
            return View(db.HLL_Vocabulary.ToList());
        }

        // GET: Vocabulary/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Vocabulary hLL_Vocabulary = db.HLL_Vocabulary.Find(id);
            if (hLL_Vocabulary == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Vocabulary);
        }

        // GET: Vocabulary/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vocabulary/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VocabularyId,Title,Description,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_Vocabulary hLL_Vocabulary)
        {
            if (ModelState.IsValid)
            {
                db.HLL_Vocabulary.Add(hLL_Vocabulary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hLL_Vocabulary);
        }

       
        // GET: Vocabulary/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Vocabulary hLL_Vocabulary = db.HLL_Vocabulary.Find(id);
            if (hLL_Vocabulary == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Vocabulary);
        }

        // POST: Vocabulary/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VocabularyId,Title,Description,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_Vocabulary hLL_Vocabulary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hLL_Vocabulary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hLL_Vocabulary);
        }

        // GET: Vocabulary/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Vocabulary hLL_Vocabulary = db.HLL_Vocabulary.Find(id);
            if (hLL_Vocabulary == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Vocabulary);
        }

        // POST: Vocabulary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HLL_Vocabulary hLL_Vocabulary = db.HLL_Vocabulary.Find(id);
            db.HLL_Vocabulary.Remove(hLL_Vocabulary);
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
