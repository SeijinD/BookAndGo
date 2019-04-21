using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookNGo.Models
{
    public class Availability
    {
        [Key]
        public int AvailabilityId { get; set; }

        public House HouseId { get; set; } //ενα σπιτι εχει πολλες διαθεσιμες μερες(1-n)

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start Date Required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name Required")]
        public DateTime EndDate { get; set; }

        public virtual ICollection<House> Houses{ get; set; }//πολλα χαρακτηριστικα ανηκουν σε πολλα σπιτια(n-n)
    }
}