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
    public class ServiceController : Controller
    {
        YummyContext context =new YummyContext();
        public ActionResult Index()
        {
            var values=context.Services.ToList();
            return View(values);
        }

        public ActionResult AddService()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddService(Service service)
        {

         
            context.Services.Add(service);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteService(int id)
        {
            var value = context.Services.Find(id);
            context.Services.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UpdateService(int id)
        {
            var service = context.Services.Find(id);
            return View(service);

        }

        [HttpPost]
        public ActionResult UpdateService(Service service)
        {
            var value = context.Services.Find(service.ServiceId);

                value.Title = service.Title;
                value.Description = service.Description;
                value.Icon = service.Icon;
    
                context.SaveChanges();
            

            return RedirectToAction("Index");
        }

        


    }
    



}
