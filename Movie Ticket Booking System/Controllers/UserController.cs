using Movie_Ticket_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Movie_Ticket_Booking_System.ViewModels;
using System.Net;

namespace Movie_Ticket_Booking_System.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext _context;
        public UserController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: User
        public ActionResult Index()
        {
            List<MoviesInCinema> list = new List<MoviesInCinema>();
            List<Cinema> cinemas = _context.Cinemas.ToList();
            foreach (var cinema in cinemas)
            {
                var item = new MoviesInCinema();
                item.cinema = cinema.CinemaName;
                List<MovieDetails> Movies = new List<MovieDetails>();
                var Halls = _context.Halls.Where(h => h.CinemaId == cinema.Id).ToList();
                List<Show> shows = new List<Show>();
                foreach (var Hall in Halls)
                {
                    var ShowList = _context.Shows.Where(s => s.HallId == Hall.Id).DefaultIfEmpty().Single();
                    shows.Add(ShowList);
                }
                foreach (var show in shows)
                {
                    if(show != null)
                    { 
                        var MovieList = _context.MovieDetails.Where(m => m.Id == show.MovieId).FirstOrDefault();
                        Movies.Add(MovieList);
                    }
                }
                item.MovieDetails = Movies;
                list.Add(item);
            }
            return View(list);
        }
        // GET: MovieDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieDetails movieDetails = _context.MovieDetails.Find(id);
            if (movieDetails == null)
            {
                return HttpNotFound();
            }
            return View(movieDetails);
        }
    }
}