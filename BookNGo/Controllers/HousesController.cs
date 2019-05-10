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
using BookNGo.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BookNGo.Controllers
{
    public class HousesController : Controller
    {
        private BookNGoContext db = new BookNGoContext();

        // GET: My Houses
        //[Authorize(Role="Owner")]
        public ActionResult MyHouses()
        {
            var currentUser = User.Identity.GetUserId();
            var currentUserHouses = db.Houses.Where(i => i.Owner.Id == currentUser)
                                             .Include(x => x.Location)
                                             .Include(c => c.Category)
                                             .ToList();
            return View(currentUserHouses);
        }

        // GET: Houses
        //[Authorize(Role="Admin")]
        public ActionResult Index()
        {
            var houseinclude = db.Houses.Include( x => x.Location).ToList();
            houseinclude = db.Houses.Include(x => x.Category).ToList();
            return View(houseinclude);
        }

        // GET: Houses/Details/5
        [AllowAnonymous]
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
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");

            return View(house);
        }

        // GET: Houses/Create
        //[Authorize]
        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            //var house = new House();
            //var AllFeatures = from t in db.Features
            //                  select t;
            //HouseModel viewModel = new HouseModel(house, AllFeatures.ToList());

            //return View(viewModel);
            return View();
        }

        // POST: Houses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HouseId,Title,Description,Address,MaxOccupancy,PricePerNight,LocationId,CategoryId,Qwner,ImageUrl")] House house/*, HouseModel houseModel*/)
        { 

            if (ModelState.IsValid)
            {
                string fileName = house.ImageUrl;
                var uploadDir = "~/Image/";
                var imagePath = Path.Combine(Server.MapPath(uploadDir), fileName);
                var imageUrl = Path.Combine(uploadDir, fileName);
                house.ImageUrl = imageUrl;

                //if (houseModel.SelectedFeatures != null)
                //{
                //    foreach (var FeatureId in houseModel.SelectedFeatures)
                //    {
                //        Feature feature = db.Features.Where(t => t.FeatureId == FeatureId).First();
                //        house.Features.Add(feature);
                //    }
                //}

                house.OwnerId = User.Identity.GetUserId();
                var location = db.Locations.Where(x => x.LocationId == house.LocationId).FirstOrDefault();
                house.Location = location;
                var category = db.Categories.Where(x => x.CategoryId == house.CategoryId).FirstOrDefault();
                house.Category = category;
                db.Houses.Add(house);
                db.SaveChanges();


                //var changeRole = new AccountController();
                //changeRole.BookNGoContext = BookNGoContext;
                //changeRole.UserManager.RemoveFromRole(User.Identity.GetUserId(), "Renter");
                //changeRole.UserManager.AddToRole(User.Identity.GetUserId(), "Owner");

                return RedirectToAction("MyHouses","Houses");
            }
            
            return View(house);
        }

        // GET: Houses/Edit/5
        //[Authorize(Role="Owner")]
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
                house.Owner = db.Users.Find(User.Identity.GetUserId());
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
        //[Authorize(Role="Owner")]
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
