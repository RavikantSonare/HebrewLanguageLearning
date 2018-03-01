using HebrewLanguageLearning_Admin.DBContext;
using HebrewLanguageLearning_Admin.GenericClasses;
using HebrewLanguageLearning_Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HebrewLanguageLearning_Admin.Controllers
{
    public class DictionaryReferenceController : Controller
    {
        private HLLDBContext db = new HLLDBContext();

        // GET: DictionaryReference
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public bool InsertMultipleDictionaryReference(List<HLL_DictionaryReferenceModels> hLL_DictionaryReferencelst)
        {
            bool Status = false;
            //using (TransactionScope scope_Eg = new TransactionScope())
            //{
            HLLDBContext contextdb = new HLLDBContext();
            try
            {
                foreach (var entityToInsert in hLL_DictionaryReferencelst)
                {
                    entityToInsert.DictionaryReferenceId = EntityConfig.getnewid("HLL_DictionaryReference");
                }
                AutoMapper.Mapper.Initialize(c => { c.CreateMap<HLL_DictionaryReferenceModels, HLL_DictionaryReference>(); });
                List<HLL_DictionaryReference> DataModel = AutoMapper.Mapper.Map<List<HLL_DictionaryReferenceModels>, List<HLL_DictionaryReference>>(hLL_DictionaryReferencelst);
                contextdb.HLL_DictionaryReference.AddRange(DataModel);
                contextdb.SaveChanges();
            }
            catch (Exception e) { }
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
        // GET: DictionaryReference/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DictionaryReference/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DictionaryReference/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DictionaryReference/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DictionaryReference/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DictionaryReference/Delete/5
        public ActionResult Delete(int id)
        {
            HLL_DictionaryReference hLL_DictionaryReference = db.HLL_DictionaryReference.Find(id);
            db.HLL_DictionaryReference.Remove(hLL_DictionaryReference);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: DictionaryReference/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
