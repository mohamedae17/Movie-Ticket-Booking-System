using Movie_Ticket_Booking_System.Models;
using Movie_Ticket_Booking_System.Payments;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movie_Ticket_Booking_System.Controllers
{
    public class FrontDeskOfficerController : Controller
    {
        private ApplicationDbContext _context;
        public FrontDeskOfficerController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View(_context.frontEndOfficers.ToList());
        }
        [HttpPost]
        public ActionResult Index(FrontEndOfficer frontEnd)
        {
            BankFactory bankFactory = new BankFactory();
            frontEnd.BookingNumber = frontEnd.Id.ToString();
            var author = _context.ShowSeat.Where(a => a.seatRow == frontEnd.seatRow && a.ShowId == frontEnd.ShowId).Single();
            if (author.isReserved == false)
            {
                author.BookingNumber = frontEnd.BookingNumber;
                author.isReserved = frontEnd.isReserved;
                _context.Entry(author).State = EntityState.Modified;
                IBank bank = bankFactory.GetBank(frontEnd.PayWay);
                if (frontEnd.Coupon != null)
                {
                    bank.withDraw(frontEnd.Coupon);
                }
                else
                {
                    bank.withDraw();
                }
            }
            else
            {
                return Content("This Seat is Already Booked");
            }
            _context.frontEndOfficers.Remove(_context.frontEndOfficers.Find(frontEnd.Id));
            _context.SaveChanges();

            return View(_context.frontEndOfficers.ToList());
        }
        public ActionResult CancelSeat(int id)
        {
            _context.frontEndOfficers.Remove(_context.frontEndOfficers.Find(id));
            _context.SaveChanges();
            return Content("Cancelled");
        }
    }
}