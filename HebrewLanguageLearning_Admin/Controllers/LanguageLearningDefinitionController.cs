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
using HebrewLanguageLearning_Admin.Models;
using HebrewLanguageLearning_Admin.GenericClasses;

namespace HebrewLanguageLearning_Admin.Controllers
{
    public class LanguageLearningDefinitionController : Controller
    {
        private HLLDBContext db = new HLLDBContext();

        // GET: LanguageLearningDefinition
        public async Task<ActionResult> Index()
        {
            return View(await db.HLL_LanguageLearningDefinition.ToListAsync());
        }

        // GET: LanguageLearningDefinition/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_LanguageLearningDefinition hLL_LanguageLearningDefinition = await db.HLL_LanguageLearningDefinition.FindAsync(id);
            if (hLL_LanguageLearningDefinition == null)
            {
                return HttpNotFound();
            }
            return View(hLL_LanguageLearningDefinition);
        }

        // GET: LanguageLearningDefinition/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LanguageLearningDefinition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HLL_DefinitionModel hLL_DefinitionModel)
        {
            AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_DefinitionModel, HLL_LanguageLearningDefinition>(); });
            var _mapObj = AutoMapper.Mapper.Map<HLL_DefinitionModel, HLL_LanguageLearningDefinition>(hLL_DefinitionModel);

            if (ModelState.IsValid)
            {
                _mapObj.LanLernDefId = EntityConfig.getnewid("HLL_LanguageLearningDefinition");
                db.HLL_LanguageLearningDefinition.Add(_mapObj);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(_mapObj);
        }

        // GET: LanguageLearningDefinition/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_LanguageLearningDefinition hLL_LanguageLearningDefinition = await db.HLL_LanguageLearningDefinition.FindAsync(id);
            if (hLL_LanguageLearningDefinition == null)
            {
                return HttpNotFound();
            }
            return View(hLL_LanguageLearningDefinition);
        }

        // POST: LanguageLearningDefinition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LanLernDefId,DicEntId,Title,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_LanguageLearningDefinition hLL_LanguageLearningDefinition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hLL_LanguageLearningDefinition).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hLL_LanguageLearningDefinition);
        }

        // GET: LanguageLearningDefinition/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_LanguageLearningDefinition hLL_LanguageLearningDefinition = await db.HLL_LanguageLearningDefinition.FindAsync(id);
            if (hLL_LanguageLearningDefinition == null)
            {
                return HttpNotFound();
            }
            return View(hLL_LanguageLearningDefinition);
        }

        // POST: LanguageLearningDefinition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            HLL_LanguageLearningDefinition hLL_LanguageLearningDefinition = await db.HLL_LanguageLearningDefinition.FindAsync(id);
            db.HLL_LanguageLearningDefinition.Remove(hLL_LanguageLearningDefinition);
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
