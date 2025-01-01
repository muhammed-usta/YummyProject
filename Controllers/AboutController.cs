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
    public class AboutController : Controller
    {
        YummyContext context=new YummyContext();
        public ActionResult Index()
        {
            var values = context.Abouts.ToList();
            return View(values);
        }


        public ActionResult AddAbout()
        {
            return View();
        }

        [HttpPost]

        public ActionResult AddAbout(About about)
        {
            if (about.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "images\\";
                var fileName = Path.Combine(saveLocation, about.ImageFile.FileName);
                about.ImageFile.SaveAs(fileName);
                about.ImageUrl = "/images/" + about.ImageFile.FileName;
            }

            if (about.ImageFile2 != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "images\\";
                var fileName2 = Path.Combine(saveLocation, about.ImageFile2.FileName);
                about.ImageFile2.SaveAs(fileName2);
                about.ImageUrl2 = "/images/" + about.ImageFile2.FileName;
            }


            context.Abouts.Add(about);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAbout(int id)
        {
            var value = context.Abouts.Find(id);
            context.Abouts.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateAbout(int id)
        {
            var about = context.Abouts.Find(id);
            return View(about);
        }

        [HttpPost]
        public ActionResult UpdateAbout(About about)
        {
            var value = context.Abouts.Find(about.AboutId);

            if (value != null)
            {
                value.Item1 = about.Item1;
                value.Item2 = about.Item2;
                value.Item3 = about.Item3;
                value.Description = about.Description;
                value.VideoUrl = about.VideoUrl;

                if (about.ImageFile != null)
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
                    var fileName = Path.Combine(saveLocation, about.ImageFile.FileName);
                    about.ImageFile.SaveAs(fileName);
                    value.ImageUrl = "/images/" + about.ImageFile.FileName;

                 
                }
                if (about.ImageFile2 != null)
                {
                    if (!string.IsNullOrEmpty(value.ImageUrl2))
                    {
                        var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        var oldFilePath = currentDirectory + value.ImageUrl.Replace("/", "\\");
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                    var saveLocation = AppDomain.CurrentDomain.BaseDirectory + "images\\";
                    var fileName2 = Path.Combine(saveLocation, about.ImageFile2.FileName);
                    about.ImageFile2.SaveAs(fileName2);
                    value.ImageUrl2 = "/images/" + about.ImageFile2.FileName;
                }

                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

      
    }
}