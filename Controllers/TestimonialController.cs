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
    public class TestimonialController : Controller
    {
        YummyContext context = new YummyContext();
        public ActionResult Index()
        {
            var values = context.Testimonials.ToList();
            return View(values);
        }

        public ActionResult AddTestimonial()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTestimonial(Testimonial testimonial)
        {
            if (testimonial.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "images\\";
                var fileName = Path.Combine(saveLocation, testimonial.ImageFile.FileName);
                testimonial.ImageFile.SaveAs(fileName);
                testimonial.ImageUrl = "/images/" + testimonial.ImageFile.FileName;
            }

            context.Testimonials.Add(testimonial);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateTestimonial(int id)
        {
            var testimonial=context.Testimonials.Find(id);
            return View(testimonial);
        }

        [HttpPost]
        public ActionResult UpdateTestimonial(Testimonial testimonial)
        {
            var value = context.Testimonials.Find(testimonial.TestimonialId);

            if (value != null)
            {
                value.NameSurname = testimonial.NameSurname;
                value.Title = testimonial.Title;
                value.Comment = testimonial.Comment;
                value.Rating = testimonial.Rating;

                if (testimonial.ImageFile != null)
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
                    var fileName = Path.Combine(saveLocation, testimonial.ImageFile.FileName);
                    testimonial.ImageFile.SaveAs(fileName);
                    value.ImageUrl = "/images/" + testimonial.ImageFile.FileName;
                }

                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteTestimonial(int id)
        {
            var value = context.Testimonials.Find(id);
            context.Testimonials.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}