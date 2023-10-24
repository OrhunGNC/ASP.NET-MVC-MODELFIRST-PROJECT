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
    public class BooksController : Controller
    {
        KutuphaneEntities db = new KutuphaneEntities();
        // GET: Books
        public ActionResult Index()
        {
            var result = db.Books.Include(x=>x.Branch).ToList();
            return View(result);
        }
        public ActionResult Create()
        {
            List<SelectListItem> veri = (from x in db.Branches
                                         select new SelectListItem
                                         {
                                             Text = x.branchAdress,
                                             Value = x.branchID.ToString()
                                         }).ToList();
            ViewBag.data = veri;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Book book)
        {
            try
            {
                db.Books.Add(book);
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
            List<SelectListItem> veri2 = (from x in db.Branches
                                          select new SelectListItem
                                          {
                                              Text = x.branchAdress,
                                              Value = x.branchID.ToString()
                                          }
                                         ).ToList();
            ViewBag.data2 = veri2;
            var result = db.Books.Find(id);
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
            var result = db.Books.Find(id);
            return View(result);
        }
        [HttpPost]
        public ActionResult Delete(int id,Book book)
        {
            book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Search(string x)
        {
            var search = from p in db.Books select p;
            if (x != null)
            {
                search = search.Where(m => m.bookName.Contains(x));
            }
            return View(search.ToList());
        }

    }
}