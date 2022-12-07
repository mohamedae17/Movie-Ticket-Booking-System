using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Movie_Ticket_Booking_System.Startup))]
namespace Movie_Ticket_Booking_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
