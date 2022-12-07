using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Movie_Ticket_Booking_System.Models;

namespace Movie_Ticket_Booking_System.Controllers
{
    public class MovieDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MovieDetails
        public ActionResult Index()
        {
            var movieDetails = db.MovieDetails.Include(m => m.Cinema);
            return View(movieDetails.ToList());
        }

        // GET: MovieDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieDetails movieDetails = db.MovieDetails.Find(id);
            if (movieDetails == null)
            {
                return HttpNotFound();
            }
            return View(movieDetails);
        }

        // GET: MovieDetails/Create
        public ActionResult Create()
        {
            ViewBag.CinemaId = new SelectList(db.Cinemas, "Id", "CinemaName");
            return View();
        }

        // POST: MovieDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieDetails movieDetails, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                upload.SaveAs(path);
                movieDetails.MoviePicture = upload.FileName;
                db.MovieDetails.Add(movieDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CinemaId = new SelectList(db.Cinemas, "Id", "CinemaName", movieDetails.CinemaId);
            return View(movieDetails);
        }

        // GET: MovieDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieDetails movieDetails = db.MovieDetails.Find(id);
            if (movieDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.CinemaId = new SelectList(db.Cinemas, "Id", "CinemaName", movieDetails.CinemaId);
            return View(movieDetails);
        }

        // POST: MovieDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MovieName,MovieDescription,Language,DateAndTime,MoviePicture,CinemaId")] MovieDetails movieDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movieDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CinemaId = new SelectList(db.Cinemas, "Id", "CinemaName", movieDetails.CinemaId);
            return View(movieDetails);
        }

        // GET: MovieDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieDetails movieDetails = db.MovieDetails.Find(id);
            if (movieDetails == null)
            {
                return HttpNotFound();
            }
            return View(movieDetails);
        }

        // POST: MovieDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MovieDetails movieDetails = db.MovieDetails.Find(id);
            db.MovieDetails.Remove(movieDetails);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
