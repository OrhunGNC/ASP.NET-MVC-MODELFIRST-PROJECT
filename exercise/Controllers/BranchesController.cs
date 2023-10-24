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
    public class BranchesController : Controller
    {
        KutuphaneEntities db = new KutuphaneEntities();
        // GET: Branches
        public ActionResult Index()
        {
            var result = db.Branches.ToList();
            return View(result);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Branch branch)
        {
            try
            {
                db.Branches.Add(branch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            var result = db.Branches.Find(id);
            return View(result);
        }
        [HttpPost]
        public ActionResult Edit(Branch branch)
        {
            try
            {
                db.Entry(branch).State = System.Data.Entity.EntityState.Modified;
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
            var result = db.Branches.Find(id);
            return View(result);
        }
        [HttpPost]
        public ActionResult Delete(Branch branch, int id)
        {
            branch = db.Branches.Find(id);
            db.Branches.Remove(branch);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Search(string x)
        {
            var search = from p in db.Branches select p;
            if (x != null)
            {
                search = search.Where(m => m.branchAdress.Contains(x));
            }
            return View(search.ToList());
        }
    }
}