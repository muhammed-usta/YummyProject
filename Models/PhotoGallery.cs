using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YummyProject.Models
{
    public class PhotoGallery
    {
        public int PhotoGalleryID { get; set; }
        public string ImageUrl { get; set; }

        [NotMapped] 
        public HttpPostedFileBase ImageFile { get; set; }
    }
}