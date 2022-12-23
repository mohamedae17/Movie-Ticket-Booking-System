using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie_Ticket_Booking_System.Coupon___Proxy
{
    public class EmailServiceProxy
    {
        private SendEmailService emailService;
        public void Coubon(string Coupon)
        {
            if(emailService == null) { emailService = new SendEmail(); }
            if (Coupon == "44")
            {
              emailService.withDraw();
            }
            else
            {
                emailService.NotDiscount();
            }
        }
    }
}