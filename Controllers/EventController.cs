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
    public class EventController : Controller
    {
        YummyContext context=new YummyContext();
        public ActionResult Index()
        {
            var valeus=context.Events.ToList();
            return View(valeus);
        }

        public ActionResult AddEvent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEvent(Event evnt)
        {
            if (evnt.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "images\\";
                var fileName = Path.Combine(saveLocation, evnt.ImageFile.FileName);
                evnt.ImageFile.SaveAs(fileName);
                evnt.ImageUrl = "/images/" + evnt.ImageFile.FileName;
            }

            context.Events.Add(evnt);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateEvent(int id)
        {
            var evnt = context.Events.Find(id);
            return View(evnt);
        }

        [HttpPost]
        public ActionResult UpdateEvent(Event evnt)
        {
            var value = context.Events.Find(evnt.EventId);

            if (value != null)
            {
                value.Title = evnt.Title;
                value.Description = evnt.Description;
                value.Price = evnt.Price;

                if (evnt.ImageFile != null)
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
                    var fileName = Path.Combine(saveLocation, evnt.ImageFile.FileName);
                    evnt.ImageFile.SaveAs(fileName);
                    value.ImageUrl = "/images/" + evnt.ImageFile.FileName;
                }

                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteEvent(int id)
        {
            var value = context.Events.Find(id);
            context.Events.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}