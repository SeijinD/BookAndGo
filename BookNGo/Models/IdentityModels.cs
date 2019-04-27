using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookNGo.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        //Properties
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name Required")]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name Required")]
        [StringLength(255)]
        public string LastName { get; set; }

        [StringLength(255)]
        public string Gender { get; set; }

        [Display(Name = "BirthDay")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public Location Location { get; set; } //ενα Location εχει πολλους User(1-n)

        [Display(Name = "Created Account")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "")]
        public DateTime CreatedAt { get; set; }

        public ICollection<House> Houses { get; set; } //ενας User εχει πολλα House(1-n)

        public ICollection<Reservation> Reservations { get; set; } //ενας User εχει πολλα Reservation(1-n)

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}