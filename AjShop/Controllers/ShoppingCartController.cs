using AjShop.Context;
using AjShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using Microsoft.AspNet.Identity;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace AjShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        public class OrderRequest
        {
            public string ProductId { get; set; }
            public string Quantity { get; set; }
        }
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpPost]
        public JsonResult Add(OrderRequest orderRequest)
        {
            long prodId = Convert.ToInt64(orderRequest.ProductId);
            int quantity = Convert.ToInt32(orderRequest.Quantity);
            int itemsCount = 1;
            if (Session["ShoppingCart"] == null)
            {
                Dictionary<long, int> orderList = new Dictionary<long, int>();
                orderList.Add(prodId, quantity);
                Session["ShoppingCart"] = orderList;
            }
            else
            {
                Dictionary<long, int> orderList = (Dictionary<long, int>)Session["ShoppingCart"];
                if (orderList.ContainsKey(prodId))
                {
                    orderList[prodId] += quantity;
                }
                else
                {
                    orderList.Add(prodId, quantity);
                }
                Session["ShoppingCart"] = orderList;
                itemsCount = orderList.Count;
            }
            return Json(itemsCount, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Get()
        {
            if (Session["ShoppingCart"] == null)
            {
                return View("ViewCart");
            }

            Dictionary<long, int> cartItems = (Dictionary<long, int>)Session["ShoppingCart"];
            decimal total = 0;
            List<Product> products = new List<Product>();
            foreach (long productId in cartItems.Keys)
            {
                Product p = db.Products.Find(productId);
                total += p.Price * (decimal)cartItems[productId];
                products.Add(p);
            }

            ViewBag.Products = products;
            ViewBag.CartItems = cartItems;
            ViewBag.Total = total;

            return View("ViewCart");
        }
        public ActionResult Purchase()
        {
            if (System.Web.HttpContext.Current.User == null || !System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account"); 
            }

            string customerId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(customerId);

            if (Session["ShoppingCart"] == null)
            {
                return View();
            }
            bool isNew = false;
            Order order = db.Orders.Include(o => o.OrderDetails).Where(p => p.Status.Equals("PENDING") && p.CustomerId == customerId).FirstOrDefault();
            if (order == null)
            {
                order = new Order();
                order.CustomerId = customerId;
                order.OrderDate = DateTime.Today;
                order.Status = "PENDING";
                order.OrderDetails = new List<OrderDetail>();
                isNew = true;
            }
            else
            {
                db.OrderDetails.RemoveRange(order.OrderDetails);
            }
            
            Dictionary<long, int> orderDetailList = (Dictionary<long, int>)Session["ShoppingCart"];
            foreach (long productId in orderDetailList.Keys)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.ProductId = productId;
                order.OrderDetails.Add(new OrderDetail { ProductId = productId, Quantity = orderDetailList[productId] });
            }
            if (isNew)
            {
                db.Orders.Add(order);
            }
            foreach (OrderDetail orderDetail in order.OrderDetails)
            {
                db.OrderDetails.Add(orderDetail);
            }
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                string s = ex.ToString();
            }
            return RedirectToAction("ViewPayment", "Payment");
        }
    }
}