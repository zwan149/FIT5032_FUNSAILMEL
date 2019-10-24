using Rotativa;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_FUNSAILMEL.Models;
using Microsoft.AspNet.Identity;

namespace FIT5032_FUNSAILMEL.Controllers
{
    public class BookingsController : Controller
    {
        private FUNSAILMEL_Model1Container db = new FUNSAILMEL_Model1Container();

        // GET: Bookings
        [Authorize(Roles = "Customer")]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var bookings = db.Bookings.Where(s => s.CustomerId == userId).ToList();

            return View(bookings);
        }

        // GET: Bookings/Details/5
        [Authorize(Roles = "Customer")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/ViewBoats
        [Authorize(Roles = "Customer")]
        public ActionResult ViewBoats()
        {
            var boats = db.Boats.ToList();

            return View(boats);
        }

        // GET: Bookings/Create
        [Authorize(Roles = "Customer")]
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boat boat = db.Boats.Find(id);
            if (boat == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.BoatId = new SelectList(db.Boats, "Id", "BoatName", id);
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public ActionResult Create([Bind(Include = "Id,Date,BoatId,Review,Complete")] Booking booking)
        {
            try
            {

                booking.CustomerId = User.Identity.GetUserId();

                ModelState.Clear();
                TryValidateModel(booking);

                if (ModelState.IsValid)
                {
                    db.Bookings.Add(booking);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.CustomerId = new SelectList(db.AspNetUsers, "Id", "Email", booking.CustomerId);
                ViewBag.BoatId = new SelectList(db.Boats, "Id", "BoatName", booking.BoatId);
                return View(booking);
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                return View(booking);
            }
        }

        // GET: Bookings/Edit/5
        [Authorize(Roles = "Customer")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.AspNetUsers, "Id", "Email", booking.CustomerId);
            ViewBag.BoatId = new SelectList(db.Boats, "Id", "BoatName", booking.BoatId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public ActionResult Edit([Bind(Include = "Id,Date,BoatId,Review,Complete")] Booking booking)
        {
            booking.CustomerId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.AspNetUsers, "Id", "Email", booking.CustomerId);
            ViewBag.BoatId = new SelectList(db.Boats, "Id", "BoatName", booking.BoatId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        [Authorize(Roles = "Customer")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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

        //Bookings/DownloadAsPdf
        [Authorize(Roles = "Customer")]
        public ActionResult DownloadAsPdf(int id)
        {
            using (FUNSAILMEL_Model1Container db = new FUNSAILMEL_Model1Container())
            {
                Booking booking = db.Bookings.FirstOrDefault(c => c.Id == id);

                var report = new PartialViewAsPdf("~/Views/Shared/DetailBooking.cshtml", booking);
                return report;
            }

        }
    }
}
