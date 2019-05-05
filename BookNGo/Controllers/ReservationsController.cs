﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookNGo.Models;
using Microsoft.AspNet.Identity;

namespace BookNGo.Controllers
{
    public class ReservationsController : Controller
    {
        private BookNGoContext db = new BookNGoContext();

        // GET: My Houses
        public ActionResult MyReservations()
        {
            var currentUser = User.Identity.GetUserId();
            var ccurrentUserReservations = db.Reservations.Where(i => i.ApplicationUserId == currentUser).ToList();
            ccurrentUserReservations = db.Reservations.Include(x => x.House).ToList();
            return View(ccurrentUserReservations);
        }

        // GET: Reservations/Book
        [Authorize]
        public ActionResult BookIt(int houseId)
        {
            ViewBag.House = db.Houses.Find(houseId);
            return View();
        }

        // POST: Reservations/BookIt
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookIt([Bind(Include = "ReservationId,StartDate,EndDate,NumberOfOccupants,DateOfBooking,PriceCharged,ApplicationUserId,HouseId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                ViewBag.House = TempData["House"];
                reservation.HouseId = ViewBag.House.HouseId;
                reservation.ApplicationUserId = User.Identity.GetUserId();
                reservation.DateOfBooking = DateTime.Today;
                if (ViewBag.House.PricePerNight != null)
                {
                    reservation.PriceCharged = ViewBag.House.PricePerNight;
                }
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HouseId = new SelectList(db.Houses, "Id", "Title" , "PricePerNight", ViewBag.House.Id);
            return View(reservation);
        }

        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.House);
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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
