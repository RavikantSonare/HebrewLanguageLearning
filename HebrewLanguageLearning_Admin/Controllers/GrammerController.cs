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
    public class GrammerController : Controller
    {
        private HLLDBContext db = new HLLDBContext();

        // GET: Grammer
        public ActionResult Index()
        {
            return View(db.HLL_Grammer.ToList());
        }

        // GET: Grammer/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Grammer hLL_Grammer = db.HLL_Grammer.Find(id);
            if (hLL_Grammer == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Grammer);
        }

        // GET: Grammer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Grammer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GrammerId,Title,Description,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_Grammer hLL_Grammer)
        {
            if (ModelState.IsValid)
            {
                db.HLL_Grammer.Add(hLL_Grammer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hLL_Grammer);
        }

        // GET: Grammer/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Grammer hLL_Grammer = db.HLL_Grammer.Find(id);
            if (hLL_Grammer == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Grammer);
        }

        // POST: Grammer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GrammerId,Title,Description,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_Grammer hLL_Grammer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hLL_Grammer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hLL_Grammer);
        }

        // GET: Grammer/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Grammer hLL_Grammer = db.HLL_Grammer.Find(id);
            if (hLL_Grammer == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Grammer);
        }

        // POST: Grammer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HLL_Grammer hLL_Grammer = db.HLL_Grammer.Find(id);
            db.HLL_Grammer.Remove(hLL_Grammer);
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
