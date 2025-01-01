using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YummyProject.Models
{
    public class About
    {
        public int AboutId { get; set; }
        public string ImageUrl { get; set; }
        public string ImageUrl2 { get; set; }
        public string Item1 { get; set; }
        public string Item2 { get; set; }
        public string Item3 { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public string PhoneNumber { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile2 { get; set; }   
    }
}