using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookNGo.Models;

namespace BookNGo.Controllers
{
    public class AvailabilitiesController : Controller
    {
        private BookNGoContext db = new BookNGoContext();

        // GET: Availabilities
        public ActionResult Index()
        {
            return View(db.Availabilities.ToList());
        }

        // GET: Availabilities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Availability availability = db.Availabilities.Find(id);
            if (availability == null)
            {
                return HttpNotFound();
            }
            return View(availability);
        }

        // GET: Availabilities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Availabilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AvailabilityId,StartDate,EndDate")] Availability availability)
        {
            if (ModelState.IsValid)
            {
                db.Availabilities.Add(availability);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(availability);
        }

        // GET: Availabilities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Availability availability = db.Availabilities.Find(id);
            if (availability == null)
            {
                return HttpNotFound();
            }
            return View(availability);
        }

        // POST: Availabilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AvailabilityId,StartDate,EndDate")] Availability availability)
        {
            if (ModelState.IsValid)
            {
                db.Entry(availability).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(availability);
        }

        // GET: Availabilities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Availability availability = db.Availabilities.Find(id);
            if (availability == null)
            {
                return HttpNotFound();
            }
            return View(availability);
        }

        // POST: Availabilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Availability availability = db.Availabilities.Find(id);
            db.Availabilities.Remove(availability);
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
