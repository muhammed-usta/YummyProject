using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;
using YummyProject.Models;

namespace YummyProject.Controllers
{
    public class GalleryController : Controller
    {
        YummyContext context=new   YummyContext();
        public ActionResult Index()
        {
            var values=context.PhotoGalleries.ToList();
            return View(values);
        }

        public ActionResult AddGallery()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddGallery(PhotoGallery gallery)
        {
            if (gallery.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "images\\";
                var fileName = Path.Combine(saveLocation, gallery.ImageFile.FileName);
                gallery.ImageFile.SaveAs(fileName);
                gallery.ImageUrl = "/images/" + gallery.ImageFile.FileName;
            }

            context.PhotoGalleries.Add(gallery);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateGallery(int id)
        {
            var gallery = context.PhotoGalleries.Find(id);
            return View(gallery);
        }

        [HttpPost]
        public ActionResult UpdateGallery(PhotoGallery gallery)
        {
            var value = context.PhotoGalleries.Find(gallery.PhotoGalleryID);

            if (value != null)
            {
                value.ImageUrl = gallery.ImageUrl;
               

                if (gallery.ImageFile != null)
                {
                    if (!string.IsNullOrEmpty(value.ImageUrl))
                    {
                        var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        var oldFilePath = currentDirectory + value.ImageUrl.Replace("/", "\\");
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                    var saveLocation = AppDomain.CurrentDomain.BaseDirectory + "images\\";
                    var fileName = Path.Combine(saveLocation, gallery.ImageFile.FileName);
                    gallery.ImageFile.SaveAs(fileName);
                    value.ImageUrl = "/images/" + gallery.ImageFile.FileName;
                }

                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteGallery(int id)
        {
            var value = context.PhotoGalleries.Find(id);
            context.PhotoGalleries.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}