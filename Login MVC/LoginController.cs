using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication.Models;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;

namespace MvcApplication.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index() {

            return View();
        }

        [HttpPost]
        public ActionResult LogInUser(login user)
        {
            studentdbEntities ctx = new studentdbEntities();

            IList<login> name = ctx.logins.SqlQuery("select * from login where username='"+ user.username+"' and password='"+user.password+"'").ToList<login>();

            if (name != null) {
                return View();
            }

            return View(); 
        }

        [HttpGet]
        public ActionResult LogIn() {
            return View();
        }



        [HttpPost]
        public ActionResult LogIn(Models.login userr) {

            if (IsValid(userr.username, userr.password))
            {
                FormsAuthentication.SetAuthCookie(userr.username, false);
                return RedirectToAction("Index", "LoginSuccess");
            }
            else {
                ModelState.AddModelError("", "Login details are wrong");
            }
            return View(userr);
        }


        public ActionResult LogOut() {
            //FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
        public ActionResult LoginSuccess() {
            return View();
        }
        private bool IsValid(string userid, string password) {

            using (studentdbEntities dbContext = new studentdbEntities())
            {
                var user = (from us in dbContext.logins
                            where string.Compare(userid, us.username, StringComparison.OrdinalIgnoreCase) == 0
                            && string.Compare(password, us.password, StringComparison.OrdinalIgnoreCase) == 0
                            select us).FirstOrDefault();

                return (user != null) ? true : false;
            }

            
            
        }

    }
}
