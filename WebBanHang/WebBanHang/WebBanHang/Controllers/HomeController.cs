using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using WebBanHang.DAL;

namespace WebBanHang.Controllers
{
    public class HomeController : Controller
    {
        DefaultConnection _db = new DefaultConnection();
        public ActionResult Index()
        {
           
           
                
            
            ViewBag.UserName = User.Identity.Name;
            return View(_db.Products.ToList());
        }
        public ActionResult Tintuc()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}