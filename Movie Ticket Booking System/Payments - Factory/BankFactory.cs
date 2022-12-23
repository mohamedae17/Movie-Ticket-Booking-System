using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie_Ticket_Booking_System.Payments
{
    public class BankFactory : IBankFactory
    {
        public IBank GetBank(string PayWay)
        {
            switch (PayWay)
            {
                case "1": return new Credit();
                case "2": return new Cash();
            }
            return null;
        }
    }
}