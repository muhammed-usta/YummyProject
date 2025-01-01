using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;
using YummyProject.Models;
using System.Data.Entity;


namespace YummyProject.Controllers
{
    public class ChefController : Controller
    {
        YummyContext context = new YummyContext();
        public ActionResult Index()
        {
            var values = context.Chefs
                .Include("ChefSocials")
                .ToList();

            return View(values);
        }


        public ActionResult AddChef()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddChef(Chef chef, List<string> SocialMediaName, List<string> SocialMediaUrl, List<string> SocialMediaIcon)
        {
            if (ModelState.IsValid)
            {

                if (chef.ImageFile != null)
                {
                    var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    var saveLocation = currentDirectory + "images\\";
                    var fileName = Path.Combine(saveLocation, chef.ImageFile.FileName);
                    chef.ImageFile.SaveAs(fileName);
                    chef.ImageUrl = "/images/" + chef.ImageFile.FileName;
                }


                context.Chefs.Add(chef);
                context.SaveChanges();


                for (int i = 0; i < SocialMediaName.Count; i++)
                {
                    var chefSocial = new ChefSocial
                    {
                        SocialMediaName = SocialMediaName[i],
                        Url = SocialMediaUrl[i],
                        Icon = SocialMediaIcon != null && SocialMediaIcon.Count > i ? SocialMediaIcon[i] : null,
                        ChefId = chef.ChefId
                    };

                    context.ChefSocials.Add(chefSocial);
                }

                context.SaveChanges();


                return RedirectToAction("Index");
            }


            return View(chef);
        }
        public ActionResult UpdateChef(int id)
        {
            var chef = context.Chefs
                              .Include(c => c.ChefSocials)
                              .FirstOrDefault(c => c.ChefId == id);

            if (chef == null)
            {
                return HttpNotFound();
            }

            return View(chef);
        }

        [HttpPost]
        public ActionResult UpdateChef(Chef chef, List<string> SocialMediaName, List<string> SocialMediaUrl, List<string> SocialMediaIcon)
        {
            // Şefi buluyoruz
            var value = context.Chefs
                               .Include(c => c.ChefSocials)
                               .FirstOrDefault(c => c.ChefId == chef.ChefId);

            if (value != null)
            {
                // Chef bilgilerini güncelleme
                value.Name = chef.Name;
                value.Title = chef.Title;
                value.Description = chef.Description;

                // Yeni resim varsa güncelleme
                if (chef.ImageFile != null)
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
                    var fileName = Path.Combine(saveLocation, chef.ImageFile.FileName);
                    chef.ImageFile.SaveAs(fileName);
                    value.ImageUrl = "/images/" + chef.ImageFile.FileName;
                }

                // Mevcut sosyal medya bilgilerini güncelleme
                for (int i = 0; i < SocialMediaName.Count; i++)
                {
                    var socialMedia = value.ChefSocials.ElementAtOrDefault(i);
                    if (socialMedia != null)
                    {
                        // Mevcut öğe varsa, bilgilerini güncelle
                        socialMedia.SocialMediaName = SocialMediaName[i];
                        socialMedia.Url = SocialMediaUrl[i];
                        socialMedia.Icon = SocialMediaIcon != null && SocialMediaIcon.Count > i ? SocialMediaIcon[i] : null;
                    }
                    else
                    {
                        // Yeni öğe ekle
                        value.ChefSocials.Add(new ChefSocial
                        {
                            SocialMediaName = SocialMediaName[i],
                            Url = SocialMediaUrl[i],
                            Icon = SocialMediaIcon != null && SocialMediaIcon.Count > i ? SocialMediaIcon[i] : null,
                            ChefId = value.ChefId
                        });
                    }
                }

                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        public ActionResult DeleteSocialMedia()
        {
            // Şeflerin sosyal medya hesaplarını ve şefin adını alıyoruz
            var values = context.ChefSocials.Include(c => c.Chef).ToList();
            return View(values);  // Şefin adı ve sosyal medya bilgilerini view'a gönderiyoruz
        }

        [HttpPost]
        public ActionResult DeleteSocialMedia(int id)
        {
            // Silinecek sosyal medya kaydını buluyoruz
            var value = context.ChefSocials.Find(id);
            if (value != null)
            {
                // Silinen sosyal medya ile ilişkilendirilmiş şef bilgisi de dahil
                context.ChefSocials.Remove(value);
                context.SaveChanges();
            }

            return RedirectToAction("DeleteSocialMedia");
        }


        public ActionResult AddSocialMedia()
        {
            // Şefleri ViewBag üzerinden view'e gönderiyoruz
            var chefs = context.Chefs.ToList();
            ViewBag.Chefs = new SelectList(chefs, "ChefId", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult AddSocialMedia(int chefId, string socialMediaName, string socialMediaUrl, string socialMediaIcon)
        {
            // Şef bulunuyor
            var chef = context.Chefs.FirstOrDefault(c => c.ChefId == chefId);

            if (chef != null)
            {
                // Yeni bir sosyal medya ekleniyor
                var newSocialMedia = new ChefSocial
                {
                    SocialMediaName = socialMediaName,
                    Url = socialMediaUrl,
                    Icon = socialMediaIcon,
                    ChefId = chefId
                };

                context.ChefSocials.Add(newSocialMedia);
                context.SaveChanges();

                TempData["SuccessMessage"] = "Social Media added successfully!";
                return RedirectToAction("AddSocialMedia");
            }

            TempData["ErrorMessage"] = "Error while adding Social Media!";
            return View();
        }



        public ActionResult DeleteChef(int id)
        {
            var value = context.Chefs.Find(id);
            context.Chefs.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

     
    }

}



