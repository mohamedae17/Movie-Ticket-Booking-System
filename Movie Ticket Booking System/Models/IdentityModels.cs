using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Movie_Ticket_Booking_System.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Movie_Ticket_Booking_System.Models.City> cities { get; set; }
        public System.Data.Entity.DbSet<Movie_Ticket_Booking_System.Models.Cinema> Cinemas { get; set; }

        public System.Data.Entity.DbSet<Movie_Ticket_Booking_System.Models.MovieDetails> MovieDetails { get; set; }

        public System.Data.Entity.DbSet<Movie_Ticket_Booking_System.Models.Halls> Halls { get; set; }

        public System.Data.Entity.DbSet<Movie_Ticket_Booking_System.Models.Show> Shows { get; set; }
        public System.Data.Entity.DbSet<Movie_Ticket_Booking_System.Models.ShowSeat> ShowSeat { get; set; }
        public System.Data.Entity.DbSet<Movie_Ticket_Booking_System.Models.FrontEndOfficer> frontEndOfficers { get; set; }
        public System.Data.Entity.DbSet<Movie_Ticket_Booking_System.Models.Notification> notifications { get; set; }
 
    }
}