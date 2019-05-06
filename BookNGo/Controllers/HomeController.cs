using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookNGo.Models;
using BookNGo.ViewModels;


namespace BookNGo.Controllers
{
    public class HomeController : Controller
    {

        private BookNGoContext db = new BookNGoContext();

        // GET: HouseSearch
        public ActionResult Index(DateTime? startDate, DateTime? endDate, int location = 0, int category = 0, int occupancy = 0)
        {
            ViewBag.Location = new SelectList(db.Locations, "LocationId", "LocationName");
            ViewBag.Category = new SelectList(db.Categories, "CategoryId", "CategoryName");
            //ViewBag.Availability = new SelectList(db.Availabilities, "HouseId", "StartDate", "EndDate");

            var Houses = db.Houses.ToList();
            var Reservations = db.Reservations.ToList();

            var query = db.Houses.AsQueryable();

            if (location > 0)
            {
                query = query.Where(x => x.Location.LocationId== location);
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

            if (startDate != null && endDate != null)
            {
                foreach (var house in query2.ToList())
                {
                    var reservationsHouse = Reservations.Where(b => b.HouseId == house.HouseId);

                    foreach (var reservation in reservationsHouse)
                    {
                        if (startDate <= reservation.StartDate && endDate >= reservation.EndDate)
                        {
                            query2.Remove(house);
                        }
                    }
                }
            }

            return View(query2);
        }

        // GET: Houses/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            House house = db.Houses.Find(id);
            if (house == null)
            {
                return HttpNotFound();
            }
            return View(house);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult AdminPage()
        {
           
            return View();
        }

    }
}