using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie_Ticket_Booking_System.Counters
{
    public class Counter
    {
        public int count = 0;
        private static Counter instance = new Counter();
        private Counter() { }
        public static Counter GetCounter()
        {
            return instance;
        }
        public void AddOne() { count++; }
        public void SubOne() { count--; }
    }
}