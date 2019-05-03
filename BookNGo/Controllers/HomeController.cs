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
        public ActionResult Index(string title, int? occupancy)
        {
            //ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName");
            List<House> listofhouses = db.Houses.ToList();

            var occu = occupancy.ToString();
            var result = db.Houses
                .Where(
                x =>
                (x.Title.Contains(title))
                && (x.MaxOccupancy.ToString().Contains(occu))
                )
                .ToList();

            return View(result);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
    }
}