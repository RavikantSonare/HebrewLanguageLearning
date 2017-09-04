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

namespace HebrewLanguageLearning_Admin.Controllers
{
    public class LessonsController : Controller
    {
        private HLLDBContext db = new HLLDBContext();

        // GET: Lessons
        public async Task<ActionResult> Index()
        {
            return View(await db.HLL_Example.ToListAsync());
        }

        // GET: Lessons/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Example hLL_Example = await db.HLL_Example.FindAsync(id);
            if (hLL_Example == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Example);
        }

        // GET: Lessons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ExampleId,DefinitionId,Title,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_Example hLL_Example)
        {
            if (ModelState.IsValid)
            {
                db.HLL_Example.Add(hLL_Example);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(hLL_Example);
        }

        // GET: Lessons/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Example hLL_Example = await db.HLL_Example.FindAsync(id);
            if (hLL_Example == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Example);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ExampleId,DefinitionId,Title,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_Example hLL_Example)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hLL_Example).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hLL_Example);
        }

        // GET: Lessons/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Example hLL_Example = await db.HLL_Example.FindAsync(id);
            if (hLL_Example == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Example);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            HLL_Example hLL_Example = await db.HLL_Example.FindAsync(id);
            db.HLL_Example.Remove(hLL_Example);
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
