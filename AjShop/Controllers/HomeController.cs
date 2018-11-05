using AjShop.Context;
using AjShop.Models;
using System.Linq;
using System.Web.Mvc;

namespace AjShop.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var products = db.Products.ToList<Product>();
            ViewBag.Products = products;
            return View(db.Categories.ToList<Category>());
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