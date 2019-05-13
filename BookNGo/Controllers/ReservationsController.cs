using System;
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

        // GET: My House Reservation
        [Authorize]
        public ActionResult MyHouseReservations()
        {
            //var currentUser = User.Identity.GetUserId();

            //var query2 = db.Reservations.AsQueryable();
            //var query = db.Houses.AsQueryable();
            //var currectUserReservations2 = query2.Where(x => x.ReservationId == 0).ToList();
            //var currectUserReservations = query.Where(x => x.HouseId == 0).ToList();

            //foreach (House item in db.Houses.Where(x => x.OwnerId == currentUser))
            //{
            //    currectUserReservations.Add(item);

            //    foreach (Reservation item2 in currectUserReservations)
            //    {
            //        currectUserReservations2.Add(item2);
            //    }
            //}

            return View();
        }


        // GET: My Reservation
        [Authorize]
        public ActionResult MyReservations()
        {
            var currectUser = User.Identity.GetUserId();
            var currectUserReservations = db.Reservations.Where(i => i.ApplicationUserId == currectUser)
                                                         .Include(x => x.House)
                                                         .ToList();
            return View(currectUserReservations);
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
        public ActionResult BookIt([Bind(Include = "ReservationId,StartDate,EndDate,NumberOfOccupants,DateOfBooking,Comments,PriceCharged,ApplicationUserId,HouseId")] Reservation reservation)
        {
            TimeSpan priceTimeSpan = reservation.EndDate - reservation.StartDate;
            int price = priceTimeSpan.Days;

            if (ViewBag.HouseId != null)
            {
                ViewBag.HouseId = new SelectList(db.Houses, "Id", "Title", "PricePerNight", "MaxOccupancy", ViewBag.House.Id);
            }

            if (reservation.StartDate < DateTime.Now || reservation.EndDate < DateTime.Now)
            {
                ModelState.AddModelError("startDate", "StartDate or EndDate is before now.");
            }
            else if (reservation.EndDate < reservation.StartDate)
            {
                ModelState.AddModelError("endDate", "EndDate is before StartDate.");
            }

            foreach (Reservation item in db.Reservations.Where(x => x.HouseId == reservation.HouseId))
            {   
                if ((item.StartDate <= reservation.StartDate && item.EndDate >= reservation.EndDate) 
                    || (item.StartDate <= reservation.StartDate && (item.EndDate <= reservation.EndDate && item.EndDate >= reservation.StartDate)) 
                    || (item.EndDate >= reservation.EndDate && (item.StartDate >= reservation.StartDate && item.StartDate <= reservation.EndDate)) 
                    || (item.StartDate >= reservation.StartDate && item.EndDate <= reservation.EndDate))
                {
                    ModelState.AddModelError("endDate", "Date is not availability.");
                }
            }

            if (ModelState.IsValid)
            {
                ViewBag.House = TempData["House"];
                reservation.HouseId = ViewBag.House.HouseId;
                reservation.ApplicationUserId = User.Identity.GetUserId();
                reservation.DateOfBooking = DateTime.Today;
                reservation.NumberOfOccupants = ViewBag.House.MaxOccupancy;
                if (ViewBag.House.PricePerNight != null)
                {
                    reservation.PriceCharged = ViewBag.House.PricePerNight * price;
                }
                db.Reservations.Add(reservation);
                db.SaveChanges();

                return RedirectToAction("MyReservations", "Reservations", "");
            }
            return View(reservation);
        }

        // GET: Reservations
        [Authorize(Roles = "Admin" )]
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.House);
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
        [Authorize]
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
        [Authorize]
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
            return RedirectToAction("MyReservations", "Reservations");
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
