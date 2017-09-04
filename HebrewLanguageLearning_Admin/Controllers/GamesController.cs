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
    public class GamesController : Controller
    {
        private HLLDBContext db = new HLLDBContext();

        // GET: Games
        public async Task<ActionResult> Index()
        {
            return View(await db.HLL_Definition.ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Definition hLL_Definition = await db.HLL_Definition.FindAsync(id);
            if (hLL_Definition == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Definition);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DefinitionId,DicEntId,Title,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_Definition hLL_Definition)
        {
            if (ModelState.IsValid)
            {
                db.HLL_Definition.Add(hLL_Definition);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(hLL_Definition);
        }

        // GET: Games/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Definition hLL_Definition = await db.HLL_Definition.FindAsync(id);
            if (hLL_Definition == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Definition);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DefinitionId,DicEntId,Title,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_Definition hLL_Definition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hLL_Definition).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hLL_Definition);
        }

        // GET: Games/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Definition hLL_Definition = await db.HLL_Definition.FindAsync(id);
            if (hLL_Definition == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Definition);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            HLL_Definition hLL_Definition = await db.HLL_Definition.FindAsync(id);
            db.HLL_Definition.Remove(hLL_Definition);
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
