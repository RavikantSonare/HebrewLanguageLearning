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
    public class VocabularyController : Controller
    {
        private HLLDBContext db = new HLLDBContext();
        private List<HLL_DictionaryEntries> VocabularyInLessonList = new List<HLL_DictionaryEntries>();
        // GET: Vocabulary
        public async Task<ActionResult> Index()
        {
            return View(await db.HLL_Vocabulary.ToListAsync());
        }

        // GET: Vocabulary/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Vocabulary hLL_Vocabulary = await db.HLL_Vocabulary.FindAsync(id);
            if (hLL_Vocabulary == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Vocabulary);
        }

        // GET: Vocabulary/Create
        public ActionResult Create()
        {

            ViewBag.dic_Entry = db.HLL_DictionaryEntries.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.DictionaryEntriesId,
                                      Text = x.DicStrongNo + ", " + x.DicHebrew + ", " + x.DicEnglish
                                  }).ToList();

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "0", Text = "" });
            ViewBag.tempList = list;
            return View();

        }

        // POST: Vocabulary/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VocabularyId,LessonId,DictionaryEntriesId")] HLL_Vocabulary hLL_Vocabulary)
        {
            if (ModelState.IsValid)
            {
                db.HLL_Vocabulary.Add(hLL_Vocabulary);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(hLL_Vocabulary);
        }

        // GET: Vocabulary/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Vocabulary hLL_Vocabulary = await db.HLL_Vocabulary.FindAsync(id);
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
        public async Task<ActionResult> Edit([Bind(Include = "VocabularyId,LessonId,DictionaryEntriesId,Description,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_Vocabulary hLL_Vocabulary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hLL_Vocabulary).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hLL_Vocabulary);
        }
        public async Task<ActionResult> setDropDwonList(string LessonId)
        {
            var objVocabularyList = db.HLL_Vocabulary.Where(x => x.IsDelete == false && x.LessonId == LessonId).Select(z=>z.DictionaryEntriesId).ToList();
            VocabularyInLessonList = db.HLL_DictionaryEntries.Where(z => objVocabularyList.Contains(z.DictionaryEntriesId)).ToList();
             
            return Json(objVocabularyList, JsonRequestBehavior.AllowGet);
        }

        // GET: Vocabulary/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_Vocabulary hLL_Vocabulary = await db.HLL_Vocabulary.FindAsync(id);
            if (hLL_Vocabulary == null)
            {
                return HttpNotFound();
            }
            return View(hLL_Vocabulary);
        }

        // POST: Vocabulary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            HLL_Vocabulary hLL_Vocabulary = await db.HLL_Vocabulary.FindAsync(id);
            db.HLL_Vocabulary.Remove(hLL_Vocabulary);
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
