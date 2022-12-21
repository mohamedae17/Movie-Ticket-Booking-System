using Movie_Ticket_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using System.IO;

namespace Movie_Ticket_Booking_System.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        public AdminController()
        {
            this._context = new ApplicationDbContext();
        }
        // GET: Admin
        /// <summary>
        /// List Of Movies & Create & Edit & Delete it
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexMovies()
        {
            return View(_context.MovieDetails.ToList());
        }
        public ActionResult DetailsMovies(int? id)
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
        [HttpGet]
        public ActionResult CreateMovies()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMovies( MovieDetails movieDetails,HttpPostedFileBase Uploads)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"),Uploads.FileName);
                Uploads.SaveAs(path);
                movieDetails.MoviePicture = Uploads.FileName;
                _context.MovieDetails.Add(movieDetails);
                _context.SaveChanges();
                return RedirectToAction("IndexMovies");
            }

            return View(movieDetails);
        }

        public ActionResult EditMovies(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMovies([Bind(Include = "Id,MovieName,MovieDescription,durationMinutes,Language,DateAndTime,Country,genre,MoviePicture")] MovieDetails movieDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(movieDetails).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("IndexMovies");
            }
            return View(movieDetails);
        }

        public ActionResult DeleteMovies(int? id)
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

        [HttpPost, ActionName("DeleteMovies")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MovieDetails movieDetails = _context.MovieDetails.Find(id);
            _context.MovieDetails.Remove(movieDetails);
            _context.SaveChanges();
            return RedirectToAction("IndexMovies");
        }

        /// <summary>
        /// List Of Cities & Create & Delete it
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexCity()
        {
            return View(_context.cities.ToList());
        }

        public ActionResult CreateCity()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCity([Bind(Include = "Id,Name")] City city)
        {
            if (ModelState.IsValid)
            {
                _context.cities.Add(city);
                _context.SaveChanges();
                return RedirectToAction("IndexCity");
            }

            return View(city);
        }

        public ActionResult DeleteCity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = _context.cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        [HttpPost, ActionName("DeleteCity")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedCity(int id)
        {
            City city = _context.cities.Find(id);
            _context.cities.Remove(city);
            _context.SaveChanges();
            return RedirectToAction("IndexCity");
        }

        /// <summary>
        /// List Of Cinemas and Create & addShow & Delete
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexCinema()
        {
            var cinemas = _context.Cinemas.Include(c => c.City);
            return View(cinemas.ToList());
        }

        public ActionResult CreateCinema()
        {
            ViewBag.CityId = new SelectList(_context.cities, "Id", "Name");
            return View();
        }
        /// <summary>
        /// Require 2 : Each cinema can have multiple halls 
        /// </summary>
        /// <param name="cinema"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCinema([Bind(Include = "Id,CinemaName,CityId,totalHalls")] Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                _context.Cinemas.Add(cinema);
                int g = cinema.totalHalls;
                int HallNum = 1;
                string CinemaName = cinema.CinemaName;
                for (int i = 0; i < g; i++)
                {
                    _context.Halls.Add(new Halls()
                    {
                        Name = "Hall " + (HallNum++)+"-" + CinemaName,
                        totalSeats = 10,
                        CinemaId = cinema.Id
                    });
                    _context.SaveChanges();
                }
                _context.SaveChanges();
                return RedirectToAction("IndexCinema");
            }

            ViewBag.CityId = new SelectList(_context.cities, "Id", "Name", cinema.CityId);
            return View(cinema);
        }

        public ActionResult DeleteCinema(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cinema cinema = _context.Cinemas.Find(id);
            if (cinema == null)
            {
                return HttpNotFound();
            }
            return View(cinema);
        }

        [HttpPost, ActionName("DeleteCinema")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCinemaConfirmed(int id)
        {
            Cinema cinema = _context.Cinemas.Find(id);
            _context.Cinemas.Remove(cinema);
            _context.SaveChanges();
            return RedirectToAction("IndexCinema");
        }

        /// <summary>
        /// List Of Shows & Create & Edit & Delete it
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexShow()
        {
            var shows = _context.Shows.Include(s => s.Halls).Include(s => s.movieDetails);
            return View(shows.ToList());
        }

        [HttpGet]
        public ActionResult CreateShow(int? CinemaId)
        {
            ViewBag.HallId = new SelectList(_context.Halls.Where(a => a.CinemaId == CinemaId), "Id", "Name");
            ViewBag.MovieId = new SelectList(_context.MovieDetails, "Id", "MovieName");
            return View();
        }

        /// <summary>
        ///  Require 2 : hall can run one movie show at a time. 
        ///  Require 3 : o	Each Movie will have multiple shows
        /// </summary>
        /// <param name="show"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateShow(Show show)
        {
            if (ModelState.IsValid)
            {
                if (_context.Shows.Any(x => x.HallId == show.HallId && x.startTime == show.startTime))
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Time Not available');</script>");
                }
                else
                {
                    _context.Shows.Add(show);
                    var x = _context.Halls.Where(y => y.Id == show.HallId).Single();
                    int g = x.totalSeats;

                    for (int i = 0; i < g; i++)
                    {
                        _context.ShowSeat.Add(new ShowSeat()
                        {
                            ShowId = show.ID,
                            createdOn = DateTime.Now,
                            seatRow = i
                        });
                        _context.SaveChanges();
                    }
                    _context.SaveChanges();
                    return RedirectToAction("IndexShow");
                }
            }

            ViewBag.HallId = new SelectList(_context.Halls, "Id", "Name", show.HallId);
            ViewBag.MovieId = new SelectList(_context.MovieDetails, "Id", "MovieName", show.MovieId);
            return View(show);
        }

        public ActionResult EditShow(int? id)
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
            ViewBag.HallId = new SelectList(_context.Halls, "Id", "Name", show.HallId);
            ViewBag.MovieId = new SelectList(_context.MovieDetails, "Id", "MovieName", show.MovieId);
            return View(show);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditShow([Bind(Include = "ID,createOn,startTime,endDate,HallId,MovieId")] Show show)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(show).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("IndexShow");
            }
            ViewBag.HallId = new SelectList(_context.Halls, "Id", "Name", show.HallId);
            ViewBag.MovieId = new SelectList(_context.MovieDetails, "Id", "MovieName", show.MovieId);
            return View(show);
        }

        public ActionResult DeleteShow(int? id)
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
            return View(show);
        }

        [HttpPost, ActionName("DeleteShow")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteShowConfirmed(int id)
        {
            Show show = _context.Shows.Find(id);
            _context.Shows.Remove(show);
            _context.SaveChanges();
            return RedirectToAction("IndexShow");
        }
    }
}