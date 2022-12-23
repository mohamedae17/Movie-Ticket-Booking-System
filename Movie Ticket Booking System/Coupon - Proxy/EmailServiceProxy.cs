using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie_Ticket_Booking_System.Coupon___Proxy
{
    public class EmailServiceProxy : SendEmailService
    {
        private SendEmailService emailService;
        public void Coubon(string Coupon)
        {
            if (Coupon == "44")
            {
              withDraw();
            }
            else
            {
                NotDiscount();
            }
        }

        public override void NotDiscount()
        {
            if (emailService == null) { emailService = new SendEmail(); }
            emailService.NotDiscount();
        }

        public override void withDraw()
        {
            if (emailService == null) { emailService = new SendEmail(); }
            emailService.withDraw();
        }
    }
}