using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;
using YummyProject.Models;

namespace YummyProject.Controllers
{
    public class MessageController : Controller
    {
        YummyContext context = new YummyContext();
        public ActionResult Index()
        {
            var values = context.Messages.Where(m => m.IsRead == false).ToList();
            return View(values);
        }

       
        public ActionResult ShowAllMessages()
        {
            var values = context.Messages.ToList();
            return View(values);
        }
        public ActionResult DeleteMessage(int id)
        {
            var value = context.Messages.Find(id);
            context.Messages.Remove(value);
            context.SaveChanges();
            return RedirectToAction("ShowAllMessages", "Message");

        }
        public ActionResult MessageDetail(int id)
        {

            var value = context.Messages.Find(id);
            value.IsRead = true;
            context.SaveChanges();
            return View(value);
        }

        [HttpPost]
        public ActionResult MakeMessageRead(int id)
        {
            var value = context.Messages.Find(id);
            value.IsRead = true;
            context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}