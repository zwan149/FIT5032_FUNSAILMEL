using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_FUNSAILMEL.Models;
using Microsoft.AspNet.Identity;

namespace FIT5032_FUNSAILMEL.Controllers
{
    public class BoatsController : Controller
    {
        private FUNSAILMEL_Model1Container db = new FUNSAILMEL_Model1Container();

        // GET: Boats
        [Authorize(Roles = "BoatOwner")]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var boats = db.Boats.Where(s => s.BoatOwnerId == userId).ToList();

            return View(boats);
        }

        // GET: Boats/Details/5
        [Authorize(Roles = "BoatOwner")]
        public ActionResult Details(int? id)
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
            return View(boat);
        }

        // GET: Boats/Create
        [Authorize(Roles = "BoatOwner")]
        public ActionResult Create()
        {
            ViewBag.BoatOwnerId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.PierId = new SelectList(db.Piers, "Id", "PierName");
            return View();
        }

        // POST: Boats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "BoatOwner")]
        public ActionResult Create([Bind(Include = "Id,BoatName,BoatType,Year,Colour,Capacity,PierId")] Boat boat, HttpPostedFileBase postedFile)
        {
            boat.BoatOwnerId = User.Identity.GetUserId();

            ModelState.Clear();
            var myUniqueFileName = string.Format(@"{0}", Guid.NewGuid());
            boat.Picture = myUniqueFileName;
            TryValidateModel(boat);

            if (ModelState.IsValid)
            {
                string serverPath = Server.MapPath("~/images/boats/");
                string fileExtension = Path.GetExtension(postedFile.FileName);
                string filePath = boat.Picture + fileExtension;
                boat.Picture = filePath;
                postedFile.SaveAs(serverPath + boat.Picture);

                db.Boats.Add(boat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BoatOwnerId = new SelectList(db.AspNetUsers, "Id", "Email", boat.BoatOwnerId);
            ViewBag.PierId = new SelectList(db.Piers, "Id", "PierName", boat.PierId);
            return View(boat);
        }

        // GET: Boats/Edit/5
        [Authorize(Roles = "BoatOwner")]
        public ActionResult Edit(int? id)
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
            ViewBag.BoatOwnerId = new SelectList(db.AspNetUsers, "Id", "Email", boat.BoatOwnerId);
            ViewBag.PierId = new SelectList(db.Piers, "Id", "PierName", boat.PierId);
            return View(boat);
        }

        // POST: Boats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "BoatOwner")]
        public ActionResult Edit([Bind(Include = "Id,BoatName,BoatType,Year,Colour,Capacity,PierId")] Boat boat, HttpPostedFileBase postedFile)
        {
            boat.BoatOwnerId = User.Identity.GetUserId();

            ModelState.Clear();
            var myUniqueFileName = string.Format(@"{0}", Guid.NewGuid());
            boat.Picture = myUniqueFileName;
            TryValidateModel(boat);

            if (ModelState.IsValid)
            {
                string serverPath = Server.MapPath("~/images/boats/");
                string fileExtension = Path.GetExtension(postedFile.FileName);
                string filePath = boat.Picture + fileExtension;
                boat.Picture = filePath;
                postedFile.SaveAs(serverPath + boat.Picture);

                db.Entry(boat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BoatOwnerId = new SelectList(db.AspNetUsers, "Id", "Email", boat.BoatOwnerId);
            ViewBag.PierId = new SelectList(db.Piers, "Id", "PierName", boat.PierId);
            return View(boat);
        }

        // GET: Boats/Delete/5
        [Authorize(Roles = "BoatOwner")]
        public ActionResult Delete(int? id)
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
            return View(boat);
        }

        // POST: Boats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "BoatOwner")]
        public ActionResult DeleteConfirmed(int id)
        {
            Boat boat = db.Boats.Find(id);
            db.Boats.Remove(boat);
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
