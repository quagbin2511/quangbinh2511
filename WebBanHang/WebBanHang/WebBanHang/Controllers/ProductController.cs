using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using System.Data.Entity;
using System.IO;
using System.Configuration;
using System.Data;
using WebBanHang.DAL;


namespace DoAn01Lab01_NguyenTrungKien.Controllers
{
    public class ProductController : Controller
    {
        DefaultConnection _db = new DefaultConnection();
        // GET: Category
        public ActionResult Index(string seachBy, string seach)
        {
            ViewBag.UserName = User.Identity.Name;


            if (seachBy == "NameProduct")
                return View(_db.Products.Where(s => s.NameProduct.StartsWith(seach)).ToList());
            else if (seachBy == "Available")
                return View(_db.Products.Where(s => s.Available == seach).ToList());
            else if (seachBy == "Category")
                return View(_db.Products.Where(s => s.Category.NameCategory == seach).ToList());
            else //None
                return View(_db.Products.ToList());

        }
        public ActionResult IndexUser()
        {
            var products = _db.Products.Include(m => m.IDCategory);
            return View(_db.Products.ToList());

        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            return View(_db.Products.Where(s => s.IDProduct == id).FirstOrDefault());
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            Product pro = new Product();
            return View(pro);
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product pro)
        {
            try
            {
                if (pro.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(pro.ImageUpload.FileName);
                    string extension = Path.GetExtension(pro.ImageUpload.FileName);
                    fileName = fileName + extension;
                    pro.Images = "~/Content/Images/" + fileName;
                    pro.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/"), fileName));
                }
                // TODO: Add insert logic here
                _db.Products.Add(pro);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_db.Products.Where(s => s.IDProduct == id).FirstOrDefault());
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product pro)
        {
            try
            {
                if (pro.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(pro.ImageUpload.FileName);
                    string extension = Path.GetExtension(pro.ImageUpload.FileName);
                    fileName = fileName + extension;
                    pro.Images = "~/Content/Images/" + fileName;
                    pro.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/"), fileName));
                }
                _db.Entry(pro).State = EntityState.Modified;
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
            return View(_db.Products.Where(s => s.IDProduct == id).FirstOrDefault());
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Product pro)
        {
            try
            {
                // TODO: Add delete logic here
                pro = _db.Products.Where(s => s.IDProduct == id).FirstOrDefault();
                _db.Products.Remove(pro);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This IDpro is used in the other table!!");
            }
        }
        public ActionResult ChooseCategory()
        {
            Category cate = new Category();
            cate.CateCollection = _db.Categories.ToList<Category>();
            return PartialView(cate);
        }
        public ActionResult SearGiaTien(int min, int max)
        {
            ////-- Cach 01: đổi khai báo min max từ int thành string
            //var money = from m in db.MatHangs.AsEnumerable() select m;

            //money = money.Where(s => Convert.ToDouble(s.DonGia) >= Convert.ToDouble(min))
            //    .Where(s => Convert.ToDouble(s.DonGia) <= Convert.ToDouble(max));

            //return View(money);


            ////--Cach 02:
            //List<MatHang> matHangs = db.MatHangs.ToList();
            //List<MatHang> matHangs2 = new List<MatHang>();
            //for (int i = 0; i < matHangs.Count; i++)
            //{
            //    if (matHangs[i].DonGia > min && matHangs[i].DonGia < max)
            //    {
            //        matHangs2.Add(matHangs[i]);
            //    }
            //}

            //-- Cach 03 LINQ
            List<Product> matHangs = _db.Products.ToList();
            List<Product> matHangs2 = (from s in matHangs
                                       where s.UnitPrice > min && s.UnitPrice < max
                                       select s).ToList();

            return View(matHangs2);
        }
        public ActionResult MatHangDonGiaGiamDan()
        {
            var matHangs = _db.Products.Include(m => m.Category)
                .OrderByDescending(m => m.UnitPrice);
            return View(matHangs.ToList());
        }
        public ActionResult MatHangDonGiaTangDan()
        {
            var matHangs = _db.Products.Include(m => m.Category)
                .OrderBy(m => m.UnitPrice);
            return View(matHangs.ToList());
        }
    }
}
