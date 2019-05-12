using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookNGo.Models
{
    public class BookNGoContext : IdentityDbContext<ApplicationUser>
    {
        public BookNGoContext() : base("DefaultConnection", throwIfV1Schema: false){}

        public static BookNGoContext Create()
        {
            return new BookNGoContext();
        }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<House> Houses { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Feature> Features { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Image> Images { get; set; }

    }
}