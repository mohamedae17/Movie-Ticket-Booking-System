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

namespace Movie_Ticket_Booking_System.Controllers
{
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
        public ActionResult Index(string option, string search)
        {
            List<CinemasInCities> list = new List<CinemasInCities>();
            if (option == "Title")
            {
                var item = new CinemasInCities();
                item.city = "Searh Result";
                item.cinema = null;
                //item.cinema = _context.Cinemas.Where(x => x.CinemaName.StartsWith(search) || search == null).ToList();
                item.movieDetails = _context.MovieDetails.Where(x => x.MovieName.StartsWith(search) || search == null).ToList();
                list.Add(item);
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(list);
            }
            else if (option == "Genere")
            {
                var item = new CinemasInCities();
                item.city = "Searh Result";
                item.cinema = null;
                //item.cinema = _context.Cinemas.Where(x => x.CinemaName.StartsWith(search) || search == null).ToList();
                item.movieDetails = _context.MovieDetails.Where(x => x.genre.StartsWith(search) || search == null).ToList();
                list.Add(item);
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(list);
            }
            else if (option == "Language")
            {
                var item = new CinemasInCities();
                item.city = "Searh Result";
                item.cinema = null;
                //item.cinema = _context.Cinemas.Where(x => x.CinemaName.StartsWith(search) || search == null).ToList();
                item.movieDetails = _context.MovieDetails.Where(x => x.Language.StartsWith(search) || search == null).ToList();
                list.Add(item);
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(list);
            }
            else if (option == "ReleaseDate")
            {
                var item = new CinemasInCities();
                item.city = "Searh Result";
                item.cinema = null;
                //item.cinema = _context.Cinemas.Where(x => x.CinemaName.StartsWith(search) || search == null).ToList();
                item.movieDetails = _context.MovieDetails.Where(x => x.RleaseDate.StartsWith(search) || search == null).ToList();
                list.Add(item);
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(list);
            }
            else if (option == "CityName")
            {
                
                List<Cinema> Cin = new List<Cinema>();
                int K = _context.cities.Where(x => x.Name.StartsWith(search) || search == null).Single().Id;
                Cin = _context.Cinemas.Where(x => x.CityId == K || search == null).ToList();
                //item.cinema = _context.Cinemas.Where(x => x.CinemaName.StartsWith(search) || search == null).ToList();
                foreach (var cinema in Cin)
                {
                    var item = new CinemasInCities();
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
                        if (show != null)
                        {
                            var MovieList = _context.MovieDetails.Where(m => m.Id == show.MovieId).FirstOrDefault();
                            Movies.Add(MovieList);
                        }
                    }
                    item.movieDetails = Movies;
                    list.Add(item);
                }
                //list.Add(item);
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(list);
            }
            else
            {
                List<City> cities = _context.cities.ToList();
               // List<Cinema> cinemas = _context.Cinemas.ToList();
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
                    int i = 0;
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
                            var ShowList = _context.Shows.Where(s => s.HallId == Hall.Id).DefaultIfEmpty().Single();
                            shows.Add(ShowList);
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
                        // i++;
                        //item.MovieDetails = Movies;
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

        public ActionResult CheckBookSeat()
        {
            var getBookTable = _context.ShowSeat.ToList();
            return View(getBookTable);

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
            //int x = show.Halls.totalSeats - _context.ShowSeat.Where(a => a.isReserved == true && a.ShowId == show.ID).Count();
            //bookNow.chairs = x;
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
          //  bookNow.showSeatofMovie = null;
            return View(bookNow);
        }
        [HttpPost]
        public ActionResult DetailsShow(BookNowView bookNow,string option)
        {

            foreach (var item in bookNow.showSeatofMovie)
            {
                if (item.isReserved && item.BookingNumber == null)
                {
                    _context.frontEndOfficers.Add(new FrontEndOfficer()
                    {
                        Id = item.Id,
                        isReserved = item.isReserved,
                        seatRow = item.seatRow,
                        PayWay = option,
                        ShowId = item.ShowId,
                        BookingNumber = item.BookingNumber
                    });
                    _context.SaveChanges();
                }
            }
            return View(bookNow);
        
        }
        [HttpGet]
        public ActionResult FrontEndOffice()
        {
            return View(_context.frontEndOfficers.ToList());
        }
        [HttpPost]
        public ActionResult FrontEndOffice(FrontEndOfficer frontEnd)
        {
            BankFactory bankFactory = new BankFactory();
            IBank bank = bankFactory.GetBank(frontEnd.PayWay);
            bank.withDraw();
            frontEnd.BookingNumber = frontEnd.Id.ToString();
            var author = _context.ShowSeat.Where(a => a.seatRow == frontEnd.seatRow && a.ShowId == frontEnd.ShowId).Single();
            author.BookingNumber = frontEnd.BookingNumber;
            author.isReserved = frontEnd.isReserved;
            _context.Entry(author).State = EntityState.Modified;
            _context.frontEndOfficers.Remove(_context.frontEndOfficers.Find(frontEnd.Id));
            _context.SaveChanges();
            
            return View(_context.frontEndOfficers.ToList());
        }

    }
}