using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Movie_Ticket_Booking_System.Coupon___Proxy
{
    public abstract class SendEmailService
    {
        public abstract void withDraw();
        public abstract void NotDiscount();
    }
}