using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BookNGo.Models
{
    public class BookNGoContext : DbContext
    {
        const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BookNGo;Integrated Security=True";

        public BookNGoContext() : base(connectionString) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<House> Houses { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Feature> Features { get; set; }

        public DbSet<UserType> UserTypes { get; set; }

        public DbSet<Location> Locations { get; set; }

    }
}