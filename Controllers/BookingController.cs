using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;
using YummyProject.Models;

namespace YummyProject.Controllers
{
    public class BookingController : Controller
    {
        YummyContext context=new YummyContext();
        public ActionResult Index()
        {
            var bookings = context.Bookings.ToList();
            return View(bookings);
        }
      
        public ActionResult Approved(int id)
        {
            var booking = context.Bookings.Find(id);
            booking.IsApproved = true;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        
      
        public ActionResult DeleteBooking(int id)
        {
            var value = context.Bookings.Find(id);
            context.Bookings.Remove(value);
            context.SaveChanges();
            return RedirectToAction("ApprovedReservations");
        }

        [HttpGet]
        public ActionResult UpdateBooking(int id)
        {
            var value = context.Bookings.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateBooking(Booking booking)
        {
            var myBooking = context.Bookings.Find(booking.BookingId);

          
            myBooking.Name = booking.Name;
            myBooking.Email = booking.Email;
            myBooking.PhoneNumber = booking.PhoneNumber;
            myBooking.BookingDate = booking.BookingDate;
            myBooking.PersonCount = booking.PersonCount;
            myBooking.MessageContent = booking.MessageContent;
            myBooking.IsApproved = booking.IsApproved;
            myBooking.BookingHour = booking.BookingHour;


            context.SaveChanges();


            return RedirectToAction("Index");
        }
      
        [HttpGet]
        public ActionResult AddBooking()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddBooking(Booking newBooking)
        {
            context.Bookings.Add(newBooking);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ApprovedReservations()
        {
            var approvedBookings = context.Bookings.Where(b => b.IsApproved == true).ToList();

            return View(approvedBookings);
        }

    }
}