using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using YummyProject.Context;
using YummyProject.Models;

namespace YummyProject.Controllers
{
    public class DashboardController : Controller
    {
        YummyContext context = new YummyContext();
        public ActionResult Index()
        {

            ViewBag.soupCount = context.Products.Count(x => x.Category.CategoryName == "Çorbalar");
            ViewBag.mostExpensive = context.Products.OrderByDescending(x => x.Price).Select(x => x.ProductName).FirstOrDefault();
            ViewBag.avgPrice = context.Products.Average(x => x.Price);
            ViewBag.cheapestPrice = context.Products.OrderBy(x =>x.Price).Select(x=>x.ProductName).FirstOrDefault();

            ViewBag.totalProductCount = context.Products.Count();
            ViewBag.totalCategoriesCount = context.Categories
                                       .Select(c => c.CategoryName)
                                       .Distinct()
                                       .Count();


            ViewBag.totalEventsCount = context.Events.Count();
            ViewBag.totalChefCount = context.Chefs.Count();



            var value = context.Products.OrderByDescending(x => x.ProductId).ToList();
            return View(value);
         
        }

     


    }




}
