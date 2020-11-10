using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using WebBanHang.DAL;

namespace WebBanHang.Controllers
{
    
    public class ShoppingCartController : Controller
    {
        DefaultConnection _db = new DefaultConnection();
        // GET: ShoppingCart
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddToCart(int id)
        {
            var pro = _db.Products.SingleOrDefault(s => s.IDProduct == id);
            if (pro != null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }

        [Authorize]
        public ActionResult ShowToCart()
        {
            try
            {
                if (Session["Cart"] == null)
                {
                    return RedirectToAction("erorrCart", "ShoppingCart");
                }
               else
                {
                    Cart cart = Session["Cart"] as Cart;
                    return View(cart);
                }
            }
            catch
            {
                return Content("Error");
            }
      
            
           
        }
        public ActionResult Update_Quantity_Cart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["ID_Product"]);
            int quantity = int.Parse(form["Quantity"]);
            cart.Update_Quantity_Shopping(id_pro, quantity);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult RemoveCart(int id)
        {

            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public PartialViewResult BagCart()
        {
            int total_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
                total_item = cart.Total_Quantity_in_Cart();
            ViewBag.QuantityCart = total_item;
            return PartialView("BagCart");
        }
        public ActionResult Shopping_Success()
        {
            return View();
        }
        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                Order _order = new Order();
                _order.OrderDate = DateTime.Now;
                _order.Descriptions = form["AddressDelivery"];
                _order.CodeCus = int.Parse(form["CodeCustomer"]);
                _db.Orders.Add(_order);
                foreach (var item in cart.Items)
                {
                    OrderDetail _order_detail = new OrderDetail();
                    _order_detail.IDOrder = _order.IDOrder;
                    _order_detail.IDProduct = item._shopping_product.IDProduct;
                    _order_detail.UnitPriceSale = item._shopping_product.UnitPrice;
                    _order_detail.QuantitySale = item._shopping_quantity;
                    _db.OrderDetails.Add(_order_detail);
                }
                _db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("Shopping_Success", "ShoppingCart");
            }
            catch
            {
                return RedirectToAction("erorrCheck");
            }
        }
        public ActionResult erorrCheck()
        {
            return View();
        }
        public ActionResult erorrCart()
        {
            return View();
        }
    }
}