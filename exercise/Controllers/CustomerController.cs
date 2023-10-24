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
    public class CustomerController : Controller
    {
        KutuphaneEntities db = new KutuphaneEntities();
        // GET: Customer
        public ActionResult Index()
        {
            var result = db.Customers.Include(x => x.Book).Include(m => m.Branch).ToList();
            return View(result);
        }
        public ActionResult Create()
        {
            List<SelectListItem> data = (from x in db.Books.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.bookName,
                                             Value = x.bookID.ToString()
                                         }).ToList();
            ViewBag.data = data;

            List<SelectListItem> data2 = (from x in db.Branches.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.branchAdress,
                                              Value = x.branchID.ToString()
                                          }).ToList();
            ViewBag.data2 = data2;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                db.Customers.Add(customer);
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
            List<SelectListItem> data = (from x in db.Books
                                         select new SelectListItem
                                         {
                                             Text = x.bookName,
                                             Value = x.bookID.ToString()
                                         }).ToList();
            ViewBag.veri = data;
            List<SelectListItem> data2 = (from x in db.Branches
                                          select new SelectListItem
                                          {
                                              Text = x.branchAdress,
                                              Value = x.branchID.ToString()
                                          }).ToList();
            ViewBag.veri2 = data2;
            var result = db.Customers.Find(id);
            return View(result);
        }
        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            try
            {
                db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
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
            var result = db.Customers.Find(id);
            return View(result);
        }
        [HttpPost]
        public ActionResult Delete(Customer customer,int id)
        {
            customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Search(string x)
        {
            var search = from p in db.Customers select p;
            if (x != null)
            {
                search=search.Where(m=>m.customerName.Contains(x)); 
            }
            return View(search.ToList());
        }
    }
}