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
using System.Transactions;

namespace HebrewLanguageLearning_Admin.Controllers
{
    public class ExampleController : Controller
    {
        private HLLDBContext db = new HLLDBContext();

        // GET: Example
        public ActionResult Index()
        {
           
            AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_Example, HLL_ExampleModels>(); });
            List<HLL_ExampleModels> DataModel = AutoMapper.Mapper.Map<List<HLL_Example>, List<HLL_ExampleModels>>(db.HLL_Example.ToList());
            foreach (var Item in DataModel)
            {
                var tmpDic = getDictionary(Item.MasterTableId);
                if (tmpDic != null)
                {
                    Item.DicStrongNo = tmpDic.DicStrongNo;
                    Item.DicEnglish = tmpDic.DicEnglish;
                    Item.DicHebrew = tmpDic.DicHebrew;
                }
            }
            return View(DataModel);
        }
        private static List<string> _masterList;
        private static List<HLL_Definition> _definationList;
        private static List<HLL_LanguageLearningDefinition> _LLDefinationList;
        private HLL_DictionaryEntries getDictionary(string MasterTableId)
        {
            if (_masterList == null)
            {
                _masterList = db.HLL_Example.Select(p => p.MasterTableId).ToList();
            }
            if (_definationList == null)
            {
                _definationList = db.HLL_Definition.Where(d => (_masterList.Contains(d.DefinitionId))).ToList();
            }

            if (_LLDefinationList == null)
            {
                _LLDefinationList = db.HLL_LanguageLearningDefinition.Where(d => (_masterList.Contains(d.LanLernDefId))).ToList();
            }
            var chk = _definationList.Where(d => d.DefinitionId == MasterTableId).FirstOrDefault();
            if (chk != null)
            {
                return db.HLL_DictionaryEntries.Where(e => e.DictionaryEntriesId == chk.DicEntId).FirstOrDefault();

            }
            var chkLLD = _LLDefinationList.Where(d => d.LanLernDefId == MasterTableId).FirstOrDefault();
            if (chkLLD != null)
            {
                return db.HLL_DictionaryEntries.Where(e => e.DictionaryEntriesId == chkLLD.DicEntId).FirstOrDefault();
            }

            return null;
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

        [HttpPost]
        public bool InsertMultipleExamples(List<HLL_ExampleModels> hLL_ExampleModelslst)
        {
            bool Status = false;
            //using (TransactionScope scope_Eg = new TransactionScope())
            //{
                HLLDBContext contextdb = new HLLDBContext();
                try
                {
                    foreach (var entityToInsert in hLL_ExampleModelslst)
                    {
                        entityToInsert.ExampleId = EntityConfig.getnewid("HLL_Example");
                    }
                    AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_ExampleModels, HLL_Example>(); });
                    List<HLL_Example> DataModel = AutoMapper.Mapper.Map<List<HLL_ExampleModels>, List<HLL_Example>>(hLL_ExampleModelslst);
                    contextdb.HLL_Example.AddRange(DataModel);
                    contextdb.SaveChanges();
                }
                catch(Exception e) { }
                finally
                {
                    if (contextdb != null)
                        contextdb.Dispose();
              //  }
              //  scope_Eg.Complete();
                Status = true;
            }
            return Status;
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

        // POST: Example/Delete/5   [ValidateAntiForgeryToken]

        [HttpPost, ActionName("Delete")]
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
