using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;
using YummyProject.Models;
using System.Data.Entity;


namespace YummyProject.Controllers
{
    public class ProductController : Controller
    {
        YummyContext context=new YummyContext();
        public ActionResult Index(int? categoryId, int page = 1)
        {
            const int pageSize = 6;  
            int skip = (page - 1) * pageSize;  

            
            ViewBag.Categories = context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                })
                .ToList();

         
            var productsQuery = context.Products.Include("Category").AsQueryable();

        
            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId.Value);
            }

        
            var totalProducts = productsQuery.Count(); 
            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize); 

            var products = productsQuery
                .OrderBy(p => p.ProductId) 
                .Skip(skip)  
                .Take(pageSize)  
                .ToList();

           
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.CategoryId = categoryId;  

            return View(products);  
        }


        private void CategoryDropDown()
        {
            var categoryList = context.Categories.ToList();

            List<SelectListItem> categories = (from x in categoryList
                                               select new SelectListItem
                                               { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();

            ViewBag.categories = categories;
        }

        public ActionResult AddProduct()
        {
            CategoryDropDown();
            var categoryList = context.Categories.ToList();
            List<SelectListItem> categories = (from x in categoryList
                                               select new SelectListItem
                                               { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();

            ViewBag.categories = categories;
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            CategoryDropDown();
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            if (product.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "images\\";

                if (!Directory.Exists(saveLocation))
                {
                    Directory.CreateDirectory(saveLocation);
                }

                var fileName = Path.Combine(saveLocation, product.ImageFile.FileName);

                product.ImageFile.SaveAs(fileName);

                product.ImageUrl = "/images/" + product.ImageFile.FileName;
            }

            context.Products.Add(product);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateProduct(int id)
        {
            var product = context.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound(); 
            }

          
            CategoryDropDown();

            return View(product); 
        }
        [HttpPost]
        public ActionResult UpdateProduct(Product product)
        {
            CategoryDropDown();

            if (!ModelState.IsValid)
            {
                return View(product); 
            }

            var existingProduct = context.Products.Find(product.ProductId);

            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.Ingredients = product.Ingredients;
                existingProduct.Price = product.Price;
                existingProduct.CategoryId = product.CategoryId;

             
                if (product.ImageFile != null)
                {
                    if (!string.IsNullOrEmpty(existingProduct.ImageUrl))
                    {
                       
                        var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        var oldFilePath = currentDirectory + existingProduct.ImageUrl.Replace("/", "\\");
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // Save new image
                    var saveLocation = AppDomain.CurrentDomain.BaseDirectory + "images\\";
                    var fileName = Path.Combine(saveLocation, product.ImageFile.FileName);
                    product.ImageFile.SaveAs(fileName);
                    existingProduct.ImageUrl = "/images/" + product.ImageFile.FileName; 
                }

                
                context.SaveChanges();
            }

            return RedirectToAction("Index"); 




          
        }
        public ActionResult DeleteProduct(int id)
        {
            var value = context.Products.Find(id);
            context.Products.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult LastProduct()
        {
            // Son eklenen 10 ürünü almak için ID'yi tersten sıralıyoruz ve Category'yi de dahil ediyoruz
            var last10Products = context.Products
                                         .Include(p => p.Category)  // Category'yi dahil et
                                         .OrderByDescending(p => p.ProductId)  // ProductId'ye göre tersten sıralama
                                         .Take(1)  // Son 10 ürünü al
                                         .ToList();

            // Veriyi ViewBag'e aktarıyoruz
            ViewBag.Last10Product = last10Products;

            return View();
        }



    }




}