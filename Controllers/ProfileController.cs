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
    public class ProfileController : Controller
    {
        YummyContext context = new YummyContext();
        public ActionResult Index()
        {
            string username = Session["currentuser"].ToString();
            if (String.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Login");
            }
            var admin = context.Admins.FirstOrDefault(x => x.UserName == username);
            return View(admin);
        }

        [HttpPost]
        public ActionResult Index(Admin model)
        {
            string username = Session["currentuser"].ToString();
            var admin = context.Admins.FirstOrDefault(x => x.UserName == username);


            if (admin.Password == model.Password)
            {
                if (model.ImageFile != null)
                {
                    var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    var saveLocation = currentDirectory + "images\\";
                    var fileName = Path.Combine(saveLocation, model.ImageFile.FileName);
                    model.ImageFile.SaveAs(fileName);
                    admin.ImageUrl = "/images/" + model.ImageFile.FileName;
                }
                admin.NameSurname = model.NameSurname;

                context.SaveChanges();
                Session.Abandon();
                return RedirectToAction("SignIn", "Login");

            }

            ModelState.AddModelError("", "The password you entered is incorrect.");
            return View(model);
        }
    }
}