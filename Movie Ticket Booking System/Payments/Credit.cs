﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Movie_Ticket_Booking_System.Payments
{
    public class Credit : IBank
    {
        public void withDraw(string Coupon)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("mohamed.ae1717@gmail.com");
                mail.To.Add("titopop2001@gmail.com");
                mail.Subject = "Booking Succfull";
                mail.Body = "<h1>You booked seat with Credit Card with Discount 50%</h1>";
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential("mohamed.ae1717@gmail.com", ",,,,,,,,,");
                    smtp.EnableSsl = true;
                    //smtp.Send(mail);
                    //lab
                }
            }
        }
        public void withDraw()
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("mohamed.ae1717@gmail.com");
                mail.To.Add("titopop2001@gmail.com");
                mail.Subject = "Booking Succfull";
                mail.Body = "<h1>You booked seat with Credit Card</h1>";
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential("mohamed.ae1717@gmail.com", ",,,,,,,,,");
                    smtp.EnableSsl = true;
                    //smtp.Send(mail);
                    //lab
                }
            }
        }
    }
}