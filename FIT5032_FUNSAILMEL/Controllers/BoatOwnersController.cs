using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_FUNSAILMEL.Models;

namespace FIT5032_FUNSAILMEL.Controllers
{
    public class BoatOwnersController : Controller
    {
        private FUNSAILMEL_Model1Container db = new FUNSAILMEL_Model1Container();

        // GET: BoatOwners
        public ActionResult Index()
        {
            return View(db.BoatOwners.ToList());
        }

        // GET: BoatOwners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoatOwner boatOwner = db.BoatOwners.Find(id);
            if (boatOwner == null)
            {
                return HttpNotFound();
            }
            return View(boatOwner);
        }

        // GET: BoatOwners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BoatOwners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,ContactPerson,ContactPhone,Password")] BoatOwner boatOwner)
        {
            if (ModelState.IsValid)
            {
                db.BoatOwners.Add(boatOwner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(boatOwner);
        }

        // GET: BoatOwners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoatOwner boatOwner = db.BoatOwners.Find(id);
            if (boatOwner == null)
            {
                return HttpNotFound();
            }
            return View(boatOwner);
        }

        // POST: BoatOwners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,ContactPerson,ContactPhone,Password")] BoatOwner boatOwner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boatOwner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(boatOwner);
        }

        // GET: BoatOwners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoatOwner boatOwner = db.BoatOwners.Find(id);
            if (boatOwner == null)
            {
                return HttpNotFound();
            }
            return View(boatOwner);
        }

        // POST: BoatOwners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BoatOwner boatOwner = db.BoatOwners.Find(id);
            db.BoatOwners.Remove(boatOwner);
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
