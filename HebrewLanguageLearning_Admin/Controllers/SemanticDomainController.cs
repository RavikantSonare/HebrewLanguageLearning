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
    public class SemanticDomainController : Controller
    {
        private HLLDBContext db = new HLLDBContext();

        // GET: SemanticDomain

       
        public ActionResult Index()
        {
            var _semanticDomainList = db.HLL_SemanticDomain.ToList();

            AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_SemanticDomain, HLL_SemanticDomainModels>(); });
            List<HLL_SemanticDomainModels> DataModel = AutoMapper.Mapper.Map<List<HLL_SemanticDomain>, List<HLL_SemanticDomainModels>>(_semanticDomainList);
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
                _masterList = db.HLL_SemanticDomain.Select(p => p.MasterTableId).ToList();
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
        // GET: SemanticDomain/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_SemanticDomain hLL_SemanticDomain = db.HLL_SemanticDomain.Find(id);
            if (hLL_SemanticDomain == null)
            {
                return HttpNotFound();
            }
            return View(hLL_SemanticDomain);
        }

        // GET: SemanticDomain/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SemanticDomain/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HLL_SemanticDomainModels hLL_SemanticDomain)
        {
            if (ModelState.IsValid)
            {
                hLL_SemanticDomain.SemanticDomainId = EntityConfig.getnewid("HLL_SemanticDomain");
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_SemanticDomainModels, HLL_SemanticDomain>(); });
                HLL_SemanticDomain DataModel = AutoMapper.Mapper.Map<HLL_SemanticDomainModels, HLL_SemanticDomain>(hLL_SemanticDomain);
                db.HLL_SemanticDomain.Add(DataModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hLL_SemanticDomain);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> CreateSemanticDomain(HLL_SemanticDomainModels hLL_SemanticDomain)
        {
            if (ModelState.IsValid)
            {
                hLL_SemanticDomain.SemanticDomainId = EntityConfig.getnewid("HLL_SemanticDomain");
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_SemanticDomainModels, HLL_SemanticDomain>(); });
                HLL_SemanticDomain DataModel = AutoMapper.Mapper.Map<HLL_SemanticDomainModels, HLL_SemanticDomain>(hLL_SemanticDomain);
                db.HLL_SemanticDomain.Add(DataModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hLL_SemanticDomain);
        }

        [HttpPost]
        public bool InsertMultipleSemanticDomain(List<HLL_SemanticDomainModels> hLL_SemanticDomainlst)
        {
            bool Status = false;
            //using (TransactionScope scope_SD = new TransactionScope())
            //{
                HLLDBContext contextdb = new HLLDBContext();
                try
                {
                    foreach (var entityToInsert in hLL_SemanticDomainlst)
                    {
                        entityToInsert.SemanticDomainId = EntityConfig.getnewid("HLL_SemanticDomain");
                    }
                    AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_SemanticDomainModels, HLL_SemanticDomain>(); });
                    List<HLL_SemanticDomain> DataModel = AutoMapper.Mapper.Map<List<HLL_SemanticDomainModels>, List<HLL_SemanticDomain>>(hLL_SemanticDomainlst);
                    contextdb.HLL_SemanticDomain.AddRange(DataModel);
                    contextdb.SaveChanges();
                }
                finally
                {
                    if (contextdb != null)
                        contextdb.Dispose();
                }
               // scope_SD.Complete();
                Status = true;
           // }
            return Status;
        }

        // GET: SemanticDomain/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_SemanticDomain hLL_SemanticDomain = db.HLL_SemanticDomain.Find(id);
            if (hLL_SemanticDomain == null)
            {
                return HttpNotFound();
            }
            return View(hLL_SemanticDomain);
        }

        // POST: SemanticDomain/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SemanticDomainId,Title,ActiveStatus,IsActive,IsDelete,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] HLL_SemanticDomain hLL_SemanticDomain)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hLL_SemanticDomain).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hLL_SemanticDomain);
        }

        // GET: SemanticDomain/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HLL_SemanticDomain hLL_SemanticDomain = db.HLL_SemanticDomain.Find(id);
            if (hLL_SemanticDomain == null)
            {
                return HttpNotFound();
            }
            return View(hLL_SemanticDomain);
        }

        //[ValidateAntiForgeryToken]
        // POST: SemanticDomain/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            HLL_SemanticDomain hLL_SemanticDomain = db.HLL_SemanticDomain.Find(id);
            db.HLL_SemanticDomain.Remove(hLL_SemanticDomain);
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
