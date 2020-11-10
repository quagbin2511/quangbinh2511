using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using System.Data.Entity;
using System.Data;
using System.Net;
using WebBanHang.DAL;

namespace WebBanHang.Controllers
{
    public class CategoryController : Controller
    {
        DefaultConnection _db = new DefaultConnection();
        // GET: Category
        public ActionResult Index()
        {
            ViewBag.UserName = User.Identity.Name;
            return View(_db.Categories.ToList());
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {

            //return View(_db.Categories.Where(s => s.IDCategory == id).FirstOrDefault());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            List<Product> listItems = new List<Product>();
            foreach (var i in _db.Products.ToList())
            {
                if (i.IDCategory.Equals(category.IDCategory))
                {
                    listItems.Add(i);
                }
            }
            ViewData["LItem"] = listItems;
            return View(category);
        }


        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(Category cate)
        {
            try
            {
                // TODO: Add insert logic here
                _db.Categories.Add(cate);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_db.Categories.Where(s => s.IDCategory == id).FirstOrDefault());
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Category cate)
        {
            try
            {
                _db.Entry(cate).State = EntityState.Modified;
                _db.SaveChanges();
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_db.Categories.Where(s => s.IDCategory == id).FirstOrDefault());
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Category cate)
        {
            try
            {
                // TODO: Add delete logic here
                cate = _db.Categories.Where(s => s.IDCategory == id).FirstOrDefault();
                _db.Categories.Remove(cate);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This IDCate is used in the other table!!");
            }
        }
    }
}
