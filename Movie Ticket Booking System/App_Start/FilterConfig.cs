﻿using System.Web;
using System.Web.Mvc;

namespace Movie_Ticket_Booking_System
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
