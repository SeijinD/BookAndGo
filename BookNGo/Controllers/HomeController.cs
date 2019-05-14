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
    public class HomeController : Controller
    {

        private BookNGoContext db = new BookNGoContext();

        // GET: HouseSearch
        [AllowAnonymous]
        public ActionResult Index(DateTime? startDate, DateTime? endDate, int location = 0, int category = 0, int occupancy = 0)
        {
            ViewBag.Location = new SelectList(db.Locations, "LocationId", "LocationName");
            ViewBag.Category = new SelectList(db.Categories, "CategoryId", "CategoryName");

            if (startDate == null && endDate == null && location == 0 && category == 0 && occupancy == 0)
            {
                var queryTop = db.Houses.Include(h => h.Images).OrderByDescending(t => t.PricePerNight).Take(3);
                return View(queryTop.ToList());
            }

            //var Houses = db.Houses.ToList();
            var Reservations = db.Reservations.ToList();

            var query = db.Houses.AsQueryable();

            var queryEmpty = query.Where(x => x.HouseId == 0);
            query = db.Houses.Include(h => h.Images);

            if (location > 0)
            {
                query = query.Where(x => x.Location.LocationId == location);
            }
            if (category > 0)
            {
                query = query.Where(x => x.Category.CategoryId == category);
            }
            if (occupancy > 0)
            {
                query = query.Where(x => x.MaxOccupancy == occupancy);
            }

            var query2 = query.ToList();

            if (startDate < DateTime.Now || endDate < DateTime.Now)
            {
                ModelState.AddModelError("startDate", "StartDate or EndDate is before now.");
            }
            else if (endDate < startDate)
            {
                ModelState.AddModelError("endDate", "EndDate is before StartDate.");
            }
            if (ModelState.IsValid)
            {
                foreach (var house in query2.ToList())
                {
                    if(User.Identity.GetUserId() == house.OwnerId)
                    {
                        query2.Remove(house);
                    }
                }

                if (startDate != null && endDate != null)
                {
                    foreach (var house in query2.ToList())
                    {
                        var reservationsHouse = Reservations.Where(b => b.HouseId == house.HouseId);

                        foreach (var reservation in reservationsHouse)
                        {

                            if ((startDate <= reservation.StartDate && endDate >= reservation.EndDate)
                                || (startDate <= reservation.StartDate && (endDate <= reservation.EndDate && endDate >= reservation.StartDate)) 
                                || (endDate >= reservation.EndDate && (startDate >= reservation.StartDate && startDate <= reservation.EndDate)) 
                                || (startDate >= reservation.StartDate && endDate <= reservation.EndDate))
                            {
                                query2.Remove(house);
                            }
                        }
                    }
                }

                return View(query2);
            }
            return View(queryEmpty);
        }

        // GET: Houses/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            House house = db.Houses.Find(id);
            var location = db.Locations.Where(x => x.LocationId == house.LocationId).FirstOrDefault();
            house.Location = location;
            var category = db.Categories.Where(x => x.CategoryId == house.CategoryId).FirstOrDefault();
            house.Category = category;
            if (house == null)
            {
                return HttpNotFound();
            }
            return View(house);
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize(Roles="Admin")]
        public ActionResult AdminPage()
        {
            return View();
        }

    }
}