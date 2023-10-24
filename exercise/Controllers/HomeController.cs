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

    
    public class HomeController : Controller
    {
        KutuphaneEntities db = new KutuphaneEntities();
        // GET: Home
        [Authorize(Roles = "A,U")]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();  
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Admin admin)
        {
            var userlogin = db.Admins.FirstOrDefault(x => x.Username == admin.Username && x.Password == admin.Password);
            if (userlogin != null)
            {
                FormsAuthentication.SetAuthCookie(userlogin.Username, false);
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.hata = "Wrong Username or Password Try Again!";
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}