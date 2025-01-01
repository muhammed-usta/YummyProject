using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Compilation;

namespace YummyProject.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}