using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookNGo.Models;

namespace BookNGo.Controllers
{
    public class HouseSearchController : Controller
    {
        private BookNGoContext db = new BookNGoContext();
        // GET: HouseSearch
        public ActionResult Index(string search)
        {     
            List<House> listofhouses = db.Houses.ToList();
            List<Location> listoflocations = db.Locations.ToList();
            List<Category> listofcategories = db.Categories.ToList();

            return View(db.Houses.Where(x=>x.Title.Contains(search) || search==null).ToList());
        }
    }
}