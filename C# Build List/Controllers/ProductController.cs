using C__Build_List.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace C__Build_List.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            var products = new MyList<Product>();
            products.Add(new Product { Id = 1, Name = "Bút", Price = 5000 });
            products.Add(new Product { Id = 2, Name = "Vở", Price = 12000 });
            products.Insert(1, new Product { Id = 3, Name = "Thước", Price = 8000 });

            return View(products);
        }
    }
}