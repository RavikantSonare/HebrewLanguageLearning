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
    public class VocabularyController : Controller
    {
        private HLLDBContext db = new HLLDBContext();
        private List<HLL_DictionaryEntries> VocabularyInLessonList = new List<HLL_DictionaryEntries>();
        private List<HLL_Vocabulary> _ObjHLL_Vocabulary = new List<HLL_Vocabulary>();
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
        HLL_VocabularyModel _ObjHLL_VocabularyModel = new HLL_VocabularyModel();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VocabularyId,LessonId,DictionaryEntriesId")] HLL_Vocabulary hLL_Vocabulary)
        {
            _ObjHLL_VocabularyModel = new HLL_VocabularyModel();
            if (ModelState.IsValid)
            {

                foreach (var item in VocabularyInLessonList)
                {

                    _ObjHLL_VocabularyModel.VocabularyId = EntityConfig.getnewid("HLL_Vocabulary");
                    AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_VocabularyModel, HLL_Vocabulary>(); });
                    var vocabularyTable = AutoMapper.Mapper.Map<HLL_VocabularyModel, HLL_Vocabulary>(_ObjHLL_VocabularyModel);
                    db.HLL_Vocabulary.Add(vocabularyTable);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }

            return View(hLL_Vocabulary);
        }

        public async Task<string> AddRemoveDataInLesson(int isAdd, HLL_VocabularyModel hLL_Vocabulary)
        {
            string result = "Your Data is successfully Saved";
            _ObjHLL_VocabularyModel = new HLL_VocabularyModel();
            if (isAdd == 1)
            {
                _ObjHLL_VocabularyModel.VocabularyId = EntityConfig.getnewid("HLL_Vocabulary");
                _ObjHLL_VocabularyModel.LessonId = hLL_Vocabulary.LessonId;
                _ObjHLL_VocabularyModel.DictionaryEntriesId = hLL_Vocabulary.DictionaryEntriesId;
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_VocabularyModel, HLL_Vocabulary>(); });
                var vocabularyTable = AutoMapper.Mapper.Map<HLL_VocabularyModel, HLL_Vocabulary>(_ObjHLL_VocabularyModel);
                db.HLL_Vocabulary.Add(vocabularyTable);
                await db.SaveChangesAsync();
            }
            if (isAdd == 0)
            {
                HLL_Vocabulary hLL_VocabularyLocal = await db.HLL_Vocabulary.Where(z => z.DictionaryEntriesId == hLL_Vocabulary.DictionaryEntriesId && z.LessonId == hLL_Vocabulary.LessonId).FirstOrDefaultAsync();
                if (hLL_VocabularyLocal != null)
                {
                    db.HLL_Vocabulary.Remove(hLL_VocabularyLocal);
                    await db.SaveChangesAsync();
                    result = "Your Data is successfully delete";
                }
                else { result = "Your Data is not found"; }

            }

            return result;
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
            _ObjHLL_Vocabulary = db.HLL_Vocabulary.Where(x => x.IsDelete == false && x.LessonId == LessonId).ToList();
            var objVocabularyList = _ObjHLL_Vocabulary.Select(z => z.DictionaryEntriesId).ToList();
            VocabularyInLessonList = db.HLL_DictionaryEntries.Where(z => objVocabularyList.Contains(z.DictionaryEntriesId)).ToList();

            ViewBag.InLessonList = db.HLL_DictionaryEntries.Where(z => objVocabularyList.Contains(z.DictionaryEntriesId)).Select(x =>
                                 new SelectListItem()
                                 {
                                     Value = x.DictionaryEntriesId,
                                     Text = x.DicStrongNo + ", " + x.DicHebrew + ", " + x.DicEnglish
                                 }).ToList();

            return Json(ViewBag.InLessonList, JsonRequestBehavior.AllowGet);
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
