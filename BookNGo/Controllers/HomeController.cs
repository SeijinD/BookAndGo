using BookNGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BookNGo.Controllers
{
    public class HomeController : Controller
    {

        private BookNGoContext db = new BookNGoContext();

        // GET: HouseSearch
        public ActionResult Index(string title, int occupancy = 0,int category = 0, int location = 0)
        {
            ViewBag.Location = new SelectList(db.Locations, "LocationId", "LocationName");
            ViewBag.Category = new SelectList(db.Categories, "CategoryId", "CategoryName");

            var query = db.Houses.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(x => x.Title.Contains(title));
            }
            if(occupancy>0)
            {
                query = query.Where(x => x.MaxOccupancy==occupancy);
            }
            if (category > 0)
            {
                query = query.Where(x => x.Category.CategoryId == category);
            }
            if (location > 0)
            {
                query = query.Where(x => x.Location.LocationId == location);
            }



            return View(query.ToList());
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
        
    }
}