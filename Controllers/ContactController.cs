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
    public class ContactController : Controller
    {
        YummyContext context = new YummyContext();
        public ActionResult FooterModel()
        {
            var contactinfo = context.ContactInfos.FirstOrDefault();
            return View(contactinfo);
        }
        public ActionResult Index()
        {
            var contactinfo = context.ContactInfos.ToList();
            return View(contactinfo);
        }

        public ActionResult AddContact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddContact(ContactInfo contact)
        {

            context.ContactInfos.Add(contact);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateContact(int id)
        {
            var values = context.ContactInfos.Find(id);
            return View(values);
        }

        [HttpPost]
        public ActionResult UpdateContact(ContactInfo contact)
        {
            var value = context.ContactInfos.Find(contact.ContactInfoId);
        
                value.Address = contact.Address;
            value.Email = contact.Email;
            value.PhoneNumber = contact.PhoneNumber;
            value.MapUrl = contact.MapUrl;
            value.OpenHours = contact.OpenHours;
            value.OpenDay = contact.OpenDay;
            value.OffDay = contact.OffDay;


   
            context.SaveChanges();


            return RedirectToAction("Index");
        }

        public ActionResult DeleteContact(int id)
        {
            var value = context.ContactInfos.Find(id);
            context.ContactInfos.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}