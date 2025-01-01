using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YummyProject.Models
{
    public class Testimonial
    {
        public int TestimonialId { get; set; }
        public string ImageUrl { get; set; }
        public string NameSurname { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }  
        
    }
}