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
    public class TopicsController : Controller
    {
        private HLLDBContext db = new HLLDBContext();

        // GET: Topics
        public ActionResult Index()
        {
            return View(db.HLL_Topics.ToList());
        }

        // GET: Topics/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Topics hLL_Topics = db.HLL_Topics.Find(id);
            if (hLL_Topics == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Topics);
        }

        // GET: Topics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Topics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TopicId,Title,Description,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_Topics hLL_Topics)
        {
            if (ModelState.IsValid)
            {
                db.HLL_Topics.Add(hLL_Topics);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hLL_Topics);
        }

        // GET: Topics/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Topics hLL_Topics = db.HLL_Topics.Find(id);
            if (hLL_Topics == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Topics);
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TopicId,Title,Description,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_Topics hLL_Topics)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hLL_Topics).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hLL_Topics);
        }

        // GET: Topics/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Topics hLL_Topics = db.HLL_Topics.Find(id);
            if (hLL_Topics == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Topics);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HLL_Topics hLL_Topics = db.HLL_Topics.Find(id);
            db.HLL_Topics.Remove(hLL_Topics);
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
