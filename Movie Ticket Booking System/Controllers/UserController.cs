using Movie_Ticket_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Movie_Ticket_Booking_System.ViewModels;
using System.Net;
using System.Net.Mail;
using Movie_Ticket_Booking_System.Payments;
using Microsoft.AspNet.Identity;
using Movie_Ticket_Booking_System.SeatsStatueCopy___ProtoType;

namespace Movie_Ticket_Booking_System.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationDbContext _context;
        public UserController()
        {
            _context = new ApplicationDbContext();
        }
        /// <summary>
        /// Require 1 : It should be able to list the cities where affiliate cinemas are located.
        /// Require 4 : Customers should be able to search movies by their title, language, genre, release date, and city name
        /// </summary>
        /// <param name="option">title, language, genre, release date, and city name</param>
        /// <param name="search">String in Serach text box</param>
        /// <returns></returns>
        // GET: User
        [AllowAnonymous]
        public ActionResult Index(string option, string search)
        {
            List<CinemasInCities> list = new List<CinemasInCities>();
            if (option == "Title")
            {
                var item = new CinemasInCities();
                item.city = "Searh Result";
                item.cinema = null;
                item.movieDetails = _context.MovieDetails.Where(x => x.MovieName.StartsWith(search) || search == null).ToList();
                list.Add(item);
                return View(list);
            }
            else if (option == "Genere")
            {
                var item = new CinemasInCities();
                item.city = "Searh Result";
                item.cinema = null;
                item.movieDetails = _context.MovieDetails.Where(x => x.genre.StartsWith(search) || search == null).ToList();
                list.Add(item);
                return View(list);
            }
            else if (option == "Language")
            {
                var item = new CinemasInCities();
                item.city = "Searh Result";
                item.cinema = null;
                item.movieDetails = _context.MovieDetails.Where(x => x.Language.StartsWith(search) || search == null).ToList();
                list.Add(item);
                return View(list);
            }
            else if (option == "ReleaseDate")
            {
                var item = new CinemasInCities();
                item.city = "Searh Result";
                item.cinema = null;
                item.movieDetails = _context.MovieDetails.Where(x => x.RleaseDate.StartsWith(search) || search == null).ToList();
                list.Add(item);
                return View(list);
            }
            else if (option == "CityName")
            {
                
                List<Cinema> Cin = new List<Cinema>();
                int K = _context.cities.Where(x => x.Name.StartsWith(search) || search == null).Single().Id;
                Cin = _context.Cinemas.Where(x => x.CityId == K || search == null).ToList();
                foreach (var cinema in Cin)
                {
                    var item = new CinemasInCities();
                    List<MovieDetails> Movies = new List<MovieDetails>();
                    var Halls = _context.Halls.Where(h => h.CinemaId == cinema.Id).ToList();
                    List<Show> shows = new List<Show>();
                    foreach (var Hall in Halls)
                    {
                        var ShowList = _context.Shows.Where(s => s.HallId == Hall.Id).ToList();
                        foreach (var s in ShowList)
                        {
                            shows.Add(s);
                        }
                    }
                    foreach (var show in shows)
                    {
                        if (show != null)
                        {
                            var MovieList = _context.MovieDetails.Where(m => m.Id == show.MovieId).FirstOrDefault();
                            Movies.Add(MovieList);
                        }
                    }
                    item.movieDetails = Movies;
                    list.Add(item);
                }
                return View(list);
            }
            else
            {
                List<City> cities = _context.cities.ToList();
                foreach(var city in cities)
                {
                    var Cinemas = _context.Cinemas.Where(c => c.CityId == city.Id).ToList();
                    List<Cinema> Cin = new List<Cinema>();
                    foreach (var c in Cinemas)
                    {
                        var CinemaList = _context.Cinemas.Where(s => s.Id == c.Id).DefaultIfEmpty().Single();
                        if(CinemaList != null)
                             Cin.Add(CinemaList);
                    }
                    //item.cinema = Cin;
                   // int i = 0;
                    foreach (var cinema in Cin)
                    {
                        var item = new CinemasInCities();
                        item.city = city.Name;
                        item.cinema = cinema;
                        item.movieDetails = new List<MovieDetails>();
                        List<MovieDetails> Movies = new List<MovieDetails>();
                        var Halls = _context.Halls.Where(h => h.CinemaId == cinema.Id).ToList();
                        List<Show> shows = new List<Show>();
                        foreach (var Hall in Halls)
                        {
                            var ShowList = _context.Shows.Where(s => s.HallId == Hall.Id).ToList();
                            foreach(var s in ShowList)
                            {
                            shows.Add(s);
                            }
                        }
                        foreach (var show in shows)
                        {
                            if (show != null)
                            {
                                var MovieList = _context.MovieDetails.Where(m => m.Id == show.MovieId).FirstOrDefault();
                                Movies.Add(MovieList);
                            }
                        }
                        item.movieDetails=Movies;
                        list.Add(item);
                    }
                    
                }
              
            }
           
            return View(list);
        }
        // GET: MovieDetails/Details/5

        public ActionResult Details(int? id)
        {
            MovieWithShows movieWithShows = new MovieWithShows();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieDetails movieDetails = _context.MovieDetails.Find(id);
            if (movieDetails == null)
            {
                return HttpNotFound();
            }
            movieWithShows.movieDetails = movieDetails;
            movieWithShows.shows = _context.Shows.Where(s => s.MovieId == movieDetails.Id).ToList();

            return View(movieWithShows);
        }


        [HttpGet]
        public ActionResult DetailsShow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Show show = _context.Shows.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }
            BookNowView bookNow = new BookNowView();
            bookNow.show = _context.Shows.Find(id);
            bookNow.number = _context.ShowSeat.Where(a => a.ShowId == show.ID).ToList().Count;
            if (bookNow.showSeatofMovie == null)
            {
                bookNow.showSeatofMovie = new List<ShowSeat>();
            }
            for(int i = 0; i < bookNow.number; i++)
            {
                ShowSeat g = _context.ShowSeat.Where(s => s.ShowId == show.ID && s.seatRow == i).DefaultIfEmpty().Single();
                bookNow.showSeatofMovie.Add(g);
            }
            return View(bookNow);
        }

        [HttpPost]
        public ActionResult DetailsShow(BookNowView bookNow,string option,string Coupon)
        {

            foreach (var item in bookNow.showSeatofMovie)
            {
                Seat seat = new ReSeat
                {
                    Id = item.Id,
                    isReserved = item.isReserved,
                    seatRow = item.seatRow,
                    UserId = User.Identity.GetUserId(),
                    PayWay = option,
                    ShowId = item.ShowId,
                    Coupon = Coupon,
                    createdOn = DateTime.Now,
                    BookingNumber = item.BookingNumber
                };
                Seat seatCopy = seat.ShallowCopy(); 
                if (item.isReserved && item.BookingNumber == null)
                {
                    _context.frontEndOfficers.Add(new FrontEndOfficer()
                    {
                        Id = item.Id,
                        isReserved = item.isReserved,
                        seatRow = item.seatRow,
                        UserId = User.Identity.GetUserId(),
                        PayWay = option,
                        ShowId = item.ShowId,
                        Coupon = Coupon,
                        createdOn = DateTime.Now,
                        BookingNumber = item.BookingNumber
                    });
                    _context.SaveChanges();
                }
            }
            return Content("Check Your Mail");
        
        }
        
    }
}