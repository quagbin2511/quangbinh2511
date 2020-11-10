using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult Index(ApplicationUser model)
        {
            ViewBag.UserName = User.Identity.Name;
            return View(context.Users.ToList());
        }
        // GET: Role

        //public ViewResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public ActionResult Create(ApplicationUser user)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            context.Users.Add(user);
        //            context.SaveChanges();
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //    }
        //    return View(user);
        //}
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        public ActionResult CreateKH()
        {
            ViewBag.UserName = User.Identity.Name;
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateKH(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Address = model.Address };


                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    UserManager.AddToRole(user.Id, "KH");
                    return RedirectToAction("Index", "User");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Index", "User");
        }
        public ActionResult CreateAD()
        {
            ViewBag.UserName = User.Identity.Name;
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAD(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Address = model.Address };


                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    UserManager.AddToRole(user.Id, "Admin");
                    return RedirectToAction("Index", "User");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Index", "User");
        }
        public ViewResult Delete(String Id)
        {
            ViewBag.UserName = User.Identity.Name;
            var model = context.Users.Find(Id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(string Id)
        {
            ApplicationUser model = null;
            try
            {
                model = context.Users.Find(Id);
                context.Users.Remove(model);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }


    }
}



