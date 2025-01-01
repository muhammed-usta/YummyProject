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
    public class FeatureController : Controller
    {
        YummyContext context = new YummyContext();
        public ActionResult Index()
        {

            var values=context.Features.ToList();
            return View(values);
        }

        public ActionResult AddFeature()
        {
            return View();
        }

        [HttpPost]

        public ActionResult AddFeature(Feature feature)
        {

            if (!ModelState.IsValid)
            {
                return View(feature);
            }


          

            if (feature.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "images\\";
                var fileName = Path.Combine(saveLocation, feature.ImageFile.FileName);
                feature.ImageFile.SaveAs(fileName);
                feature.ImageUrl = "/images/" + feature.ImageFile.FileName;
            }

            context.Features.Add(feature);
           int result= context.SaveChanges();

            if (result == 0)
            {
                ViewBag.error = "Değerler kaydedilirken bir hata ile karşılaşıldı";
                return View(feature);
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteFeature(int id)
        {
            var value = context.Features.Find(id);
            context.Features.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateFeature(int id)
        {
            var feature = context.Features.Find(id);
            return View(feature);
        }

        [HttpPost]

        public ActionResult UpdateFeature(Feature feature)
        {
            var value = context.Features.Find(feature.FeatureId);

            if (value != null)
            {
                value.Title = feature.Title;
                value.Title = feature.Description;
                value.VideoUrl = feature.VideoUrl;

                if (feature.ImageFile != null)
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
                    var fileName = Path.Combine(saveLocation, feature.ImageFile.FileName);
                    feature.ImageFile.SaveAs(fileName);
                    value.ImageUrl = "/images/" + feature.ImageFile.FileName;
                }

                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }


    }
}