using AjShop.Context;
using AjShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace AjShop.Controllers
{
    public class PaymentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Payment
        public ActionResult ViewPayment()
        {
            if (System.Web.HttpContext.Current.User == null || !System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            string customerId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            Subscription subscription = db.Subscriptions.Where(s => s.Status.Equals("A")).FirstOrDefault();
            if (subscription == null)
            {
                ViewBag.ERROR_MESSAGE = "Please configure subscription";
                return View("Pay");
            }

            List<Card> cards = db.Cards.Where(c => c.CustomerId == customerId).ToList();
            if (cards == null || cards.Count == 0)
            {
                ViewBag.ERROR_MESSAGE = "Add new card";
            }
            else
            {
                ViewBag.Cards = cards;
            }
            Order order = db.Orders.Include(o => o.OrderDetails.Select(od=>od.Product)).Where(o => o.CustomerId == customerId && o.Status.Equals("PENDING")).FirstOrDefault();
            if (order == null)
            {
                ViewBag.ERROR_MESSAGE = "No order is found.";
            }
            if (order.OrderDetails == null)
            {
                ViewBag.ERROR_MESSAGE = "No order details are found.";
            }
            decimal sum = 0;
            foreach (OrderDetail od in order.OrderDetails)
            {
                sum = sum + od.Quantity * od.Product.Price;
            }
            ViewBag.Total = sum;
            ViewBag.TaxAmount = sum * (decimal)subscription.TaxPercentage / 100;
            ViewBag.TotalAmount = sum + ViewBag.TaxAmount;
            return View("Pay", order);
        }
    }
}