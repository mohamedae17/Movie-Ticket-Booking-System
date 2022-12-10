using Movie_Ticket_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

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
        [HttpGet]
        public ActionResult Index()
        {
            return View(_context.MovieDetails.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MovieName,MovieDescription,durationMinutes,Language,DateAndTime,Country,genre,MoviePicture")] MovieDetails movieDetails)
        {
            if (ModelState.IsValid)
            {
                _context.MovieDetails.Add(movieDetails);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movieDetails);
        }

        // GET: MovieDetails/EditMovies/5
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

        // POST: MovieDetails/EditMovies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMovies([Bind(Include = "Id,MovieName,MovieDescription,durationMinutes,Language,DateAndTime,Country,genre,MoviePicture")] MovieDetails movieDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(movieDetails).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movieDetails);
        }

        // GET: MovieDetails/DeleteMovies/5
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
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult IndexShow()
        {
            var shows = _context.Shows.Include(s => s.Halls).Include(s => s.movieDetails);
            return View(shows.ToList());
        }

        [HttpGet]
        public ActionResult CreateShow(int CinemaId)
        {
            ViewBag.HallId = new SelectList(_context.Halls.Where(a=>a.CinemaId == CinemaId), "Id", "Name");
            ViewBag.MovieId = new SelectList(_context.MovieDetails, "Id", "MovieName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateShow([Bind(Include = "ID,createOn,startTime,endDate,HallId,MovieId")] Show show)
        {
            if (ModelState.IsValid)
            {
                if(_context.Shows.Any(x=>x.HallId == show.HallId && x.startTime == show.startTime))
                {
                    return RedirectToAction("IndexShow");
                }
                else
                {
                _context.Shows.Add(show);
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

        // POST: Shows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Shows/Delete/5
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

        // POST: Shows/Delete/5
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