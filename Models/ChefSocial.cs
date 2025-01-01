using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YummyProject.Models
{
    public class ChefSocial
    {
        public int ChefSocialId { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string SocialMediaName { get; set; }
        public int ChefId { get; set; }
        public virtual Chef Chef { get; set; }

    }
}