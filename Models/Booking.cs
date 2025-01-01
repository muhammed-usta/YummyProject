using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YummyProject.Models
{
    public class Booking
    {
      
        public int BookingId { get; set; }

      
        public string Name { get; set; }

        
        public string Email { get; set; }

      
        public string PhoneNumber { get; set; }

   
        public DateTime BookingDate { get; set; }

       
        public TimeSpan BookingHour { get; set; }

       
        public int PersonCount { get; set; }

        public string MessageContent { get; set; }

        public bool IsApproved { get; set; }
    }
}