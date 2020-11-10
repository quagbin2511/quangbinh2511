using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    [Authorize(Roles = "Admin")]

    public class RoleController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
    
        // GET: Role
        public ActionResult Index()
        {
            ViewBag.UserName = User.Identity.Name;
            var roles = context.Roles.AsEnumerable();
            return View(roles);
        }
        public ViewResult Create()
        {
            ViewBag.UserName = User.Identity.Name;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(IdentityRole role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Roles.Add(role);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(role);
        }

        public ViewResult Delete(String Id)
        {
            ViewBag.UserName = User.Identity.Name;
            var model = context.Roles.Find(Id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(string Id)
        {
            IdentityRole model = null;
            try
            {
                model = context.Roles.Find(Id);
                context.Roles.Remove(model);
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



