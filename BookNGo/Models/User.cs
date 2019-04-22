using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookNGo.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name Required")]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name Required")]
        [StringLength(255)]
        public string LastName { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name Required")]
        [StringLength(255)]
        public string UserName { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "e-mail Required")]
        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password Required")]
        [StringLength(255)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Gender { get; set; }

        [Display(Name = "BirthDay")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Created Account")]
        public DateTime CreatedAt { get; set; }

  
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public House HouseId { get; set; } //ενας χρηστης εχει πολλα σπιτια(1-n)

        public Location Location { get; set; } //πολλοι χρηστες ανηκουν στην ιδια περιοχη(1-n)
    }
}