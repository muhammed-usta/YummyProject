using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;
using YummyProject.Models;

namespace YummyProject.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
     YummyContext context=new YummyContext();

     

        public ActionResult Index()
        {

            ViewBag.totalProductCount = context.Products.Count();
            ViewBag.totalCaetgoriesCount = context.Categories.Count();
            ViewBag.totalEventsCount = context.Events.Count();
            ViewBag.totalChefCount = context.Chefs.Count();

            return View();
        }

        public PartialViewResult DefaultFeature()
        {
            var values = context.Features.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultAbout()
        {
            var values = context.Abouts.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultProduct()
        {
            var values = context.Categories.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultService()
        {
            var values = context.Services.ToList();

            return PartialView(values);
        }

        public PartialViewResult DefaultStats()
        {
            ViewBag.totalProductCount = context.Products.Count();
            ViewBag.totalCategoriesCount = context.Categories
                                       .Select(c => c.CategoryName)
                                       .Distinct()
                                       .Count();


            ViewBag.totalEventsCount = context.Events.Count();
            ViewBag.totalChefCount = context.Chefs.Count();

           
            return PartialView();
        }

        public PartialViewResult DefaultTestimonial()

        {
            var values = context.Testimonials.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultEvent()
        {
            var values = context.Events.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultChef()
        {
            var values = context.Chefs.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultGallery()
        {
            var values = context.PhotoGalleries.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultContact()
        {
            var values = context.ContactInfos.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultFooter()
        {
            var values = context.ContactInfos.FirstOrDefault();
            return PartialView(values);
        }


     
        public PartialViewResult DefaultAddBooking()
        {
            return PartialView();
        }

        [HttpPost]
        public string DefaultAddBooking(Booking booking)
        {
            
            context.Bookings.Add(booking);
            context.SaveChanges();
            return "Rezervasyon talebiniz oluşturuldu.";

        }



        public PartialViewResult DefaultChefSocial(int chefId)
        {
            
            var values = context.ChefSocials
                                .Where(cs => cs.ChefId == chefId)  
                                .ToList();

            return PartialView(values);
        }


        public PartialViewResult DefaultSendMessage()
        {
            return PartialView();
        }

        [HttpPost]
        public string DefaultSendMessage(Message message)
        {
            context.Messages.Add(message);
            context.SaveChanges();
            return "Başarıyla tamamlandı";
        }


        public PartialViewResult DefaultSocialMedia()
        {
            var socialMedias = context.SocialMedias.ToList();
            return PartialView(socialMedias);
        }


    }
    }



