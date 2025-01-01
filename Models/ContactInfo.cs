using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YummyProject.Models
{
    public class ContactInfo
    {
        public int ContactInfoId { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MapUrl { get; set; }
        public string OpenHours { get; set; }

        public string OpenDay { get; set; }
        public string OffDay { get; set; }
    }
}