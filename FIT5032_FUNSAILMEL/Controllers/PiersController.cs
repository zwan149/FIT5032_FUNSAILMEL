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
    public class PiersController : Controller
    {
        private FUNSAILMEL_Model1Container db = new FUNSAILMEL_Model1Container();

        // GET: Piers
        public ActionResult Index()
        {
            return View(db.Piers.ToList());
        }

        // GET: Piers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pier pier = db.Piers.Find(id);
            if (pier == null)
            {
                return HttpNotFound();
            }
            return View(pier);
        }

        // GET: Piers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Piers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PierName")] Pier pier)
        {
            if (ModelState.IsValid)
            {
                db.Piers.Add(pier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pier);
        }

        // GET: Piers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pier pier = db.Piers.Find(id);
            if (pier == null)
            {
                return HttpNotFound();
            }
            return View(pier);
        }

        // POST: Piers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PierName")] Pier pier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pier);
        }

        // GET: Piers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pier pier = db.Piers.Find(id);
            if (pier == null)
            {
                return HttpNotFound();
            }
            return View(pier);
        }

        // POST: Piers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pier pier = db.Piers.Find(id);
            db.Piers.Remove(pier);
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
