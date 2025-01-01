using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YummyProject.Models
{
    public class Admin
    {
        public int AdminId { get; set; }

        public string NameSurname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ImageUrl { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}