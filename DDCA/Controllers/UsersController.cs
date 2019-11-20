using DDCA.Models;
using DDCA.ViewModels;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DDCA.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToRoute("Home");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin form, string returnUrl)
        {
            var user = Database.Session.Query<User>().FirstOrDefault(u => u.Username == form.Username);
            if (user == null)
                DDCA.Models.User.FakeHash();

            if (user == null || !user.CheckPassword(form.Password))
                ModelState.AddModelError("Username", "Username or Password is incorrect!");

            if (!ModelState.IsValid)
                return View(form);

            FormsAuthentication.SetAuthCookie(user.Username, true);

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);

            return RedirectToRoute("home");
        }
    }
}