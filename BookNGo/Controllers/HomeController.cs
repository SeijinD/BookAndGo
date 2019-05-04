using BookNGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookNGo.Controllers
{
    public class HomeController : Controller
    {

        private BookNGoContext db = new BookNGoContext();

        // GET: HouseSearch
        public ActionResult Index(string title, int occupancy = 0)
        {
            var query = db.Houses.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(x => x.Title.Contains(title));
            }
            if(occupancy>0)
            {
                query = query.Where(x => x.MaxOccupancy==occupancy);
            }
            ViewBag.Location = new SelectList(db.Locations, "LocationId", "LocationName");
            

            return View(query.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
    }
}