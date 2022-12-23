using Movie_Ticket_Booking_System.Coupon___Proxy;
using Movie_Ticket_Booking_System.Models;
using Movie_Ticket_Booking_System.Payments;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Movie_Ticket_Booking_System.Controllers
{
    [Authorize(Roles = "Front Desk Officer")]
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
        public ActionResult Book(int id)
        {
            FrontEndOfficer frontEnd = _context.frontEndOfficers.Find(id);
            BankFactory bankFactory = new BankFactory();
            frontEnd.BookingNumber = frontEnd.Id.ToString();
            var author = _context.ShowSeat.Where(a => a.seatRow == frontEnd.seatRow && a.ShowId == frontEnd.ShowId).Single();
            if (author.isReserved == false)
            {
                author.BookingNumber = frontEnd.user.Id;
                author.isReserved = frontEnd.isReserved;
                _context.Entry(author).State = EntityState.Modified;
                IBank bank = bankFactory.GetBank(frontEnd.PayWay);
                if (frontEnd.Coupon != null)
                {
                    EmailServiceProxy proxy = new EmailServiceProxy();
                    proxy.Coubon(frontEnd.Coupon);
                }  
                bank.withDraw();
               
            }
            else
            {
                return Content("This Seat is Already Booked");
            }
            _context.frontEndOfficers.Remove(_context.frontEndOfficers.Find(frontEnd.Id));
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult CancelSeat(int id)
        {
            _context.frontEndOfficers.Remove(_context.frontEndOfficers.Find(id));
            _context.SaveChanges();
            //System:
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("mohamed.ae1717@gmail.com");
                mail.To.Add("titopop2001@gmail.com");
                mail.Subject = "Your Booked is Canelld";
                mail.Body = "<h1>Your Booked is Canelld & refunded the payment</h1>";
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential("mohamed.ae1717@gmail.com", ",,,,,,,,,");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            return RedirectToAction("Index");
        }
    }
}