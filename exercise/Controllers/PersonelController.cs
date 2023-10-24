using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using exercise.Models;
using System.Web.Security;

namespace exercise.Controllers
{
    [Authorize(Roles = "A")]
    public class PersonelController : Controller
    {
        KutuphaneEntities db = new KutuphaneEntities();
        // GET: Personel
        public ActionResult Index()
        {
            var result = db.Personels.Include(x=>x.Branch).ToList();
            return View(result);
        }
        public ActionResult Create()
        {
            List<SelectListItem> data = (from x in db.Branches
                                         select new SelectListItem
                                         {
                                             Text = x.branchAdress,
                                             Value = x.branchID.ToString()
                                         }).ToList();
            ViewBag.veri = data;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Personel personel)
        {
            try
            {
                db.Personels.Add(personel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Search(string x)
        {
            var search = from p in db.Personels select p;
            if (x != null)
            {
                search=search.Where(m=>m.personelName.Contains(x));
            }
            return View(search.ToList());
        }
        public ActionResult Edit(int id)
        {
            List<SelectListItem> data = (from x in db.Branches
                                         select new SelectListItem
                                         {
                                             Text = x.branchAdress,
                                             Value = x.branchID.ToString()
                                         }).ToList();
            ViewBag.veri = data;
            var result = db.Personels.Find(id);
            return View(result);
        }
        [HttpPost]
        public ActionResult Edit(Personel personel)
        {
            try
            {
                db.Entry(personel).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            var result = db.Personels.Find(id);
            return View(result);

        }
        [HttpPost]
        public ActionResult Delete(int id,Personel personel)
        {
            personel = db.Personels.Find(id);
            db.Personels.Remove(personel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}