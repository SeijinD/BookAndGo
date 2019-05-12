using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookNGo.Models;
using Microsoft.AspNet.Identity;

namespace BookNGo.Controllers
{
    public class HousesController : Controller
    {
        private BookNGoContext db = new BookNGoContext();

        // GET: My Houses
        //[Authorize(Roles = "Owner" )]
        public ActionResult MyHouses()
        {
            var currectUser = User.Identity.GetUserId();
            var currectUserHouses = db.Houses.Where(i => i.Owner.Id == currectUser)
                                             .Include(x => x.Location)
                                             .Include(c => c.Category)
                                             .ToList();
            return View(currectUserHouses);
        }

        // GET: Houses
        //[Authorize(Roles = "Admin" )]
        public ActionResult Index()
        {
            var houseinclude = db.Houses.Include( x => x.Location).ToList();
            houseinclude = db.Houses.Include(x => x.Category).ToList();
            return View(houseinclude);
        }

        // GET: Houses/Details/5
        [Authorize]
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

        // GET: Houses/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Houses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HouseId,Title,Description,Address,MaxOccupancy,PricePerNight,LocationId,CategoryId,Qwner")] House house)
        {
            
            
            if (ModelState.IsValid)
            {

                house.OwnerId = User.Identity.GetUserId();
                var location = db.Locations.Where(x => x.LocationId == house.LocationId).FirstOrDefault();
                house.Location = location;
                var category = db.Categories.Where(x => x.CategoryId == house.CategoryId).FirstOrDefault();
                house.Category = category;
                db.Houses.Add(house);
                db.SaveChanges();

                return RedirectToAction("MyHouses","Houses");
            }
            
            return View(house);
        }

        // GET: Houses/Edit/5
        //[Authorize(Roles = "Owner" )]
        public ActionResult Edit(int? id)
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
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

        // POST: Houses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HouseId,Title,Description,Address,MaxOccupancy,PricePerNight,LocationId,CategoryId,Qwner")] House house)
        {
            if (ModelState.IsValid)
            {
                house.OwnerId = User.Identity.GetUserId();
                var location = db.Locations.Where(x => x.LocationId == house.LocationId).FirstOrDefault();
                house.Location = location;
                var category = db.Categories.Where(x => x.CategoryId == house.CategoryId).FirstOrDefault();
                house.Category = category;
                db.Entry(house).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(house);
        }

        // GET: Houses/Delete/5
        //[Authorize(Roles = "Owner" )]
        public ActionResult Delete(int? id)
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

        // POST: Houses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            House house = db.Houses.Find(id);
            db.Houses.Remove(house);
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
