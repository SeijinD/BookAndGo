using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookNGo.Models
{
    public class House
    {
        [Key]
        public int HouseId { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title Required")]
        [StringLength(255)]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description Required")]
        public string Description { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address Required")]
        public string Address { get; set; }

        [Display(Name = "Maximum Occupancy")]
        [Required(ErrorMessage = "Maximum Occupance Required")]
        public int MaxOccupancy { get; set; }

        [Display(Name = "Price per Night")]
        [Required(ErrorMessage = "Price Required")]
        public Decimal PricePerNight { get; set; }

        public byte[] Picture { get; set; }


        public Category Category { get; set; } //πολλα σπιτια ανηκουν στην ιδια κατηγορια(1-n)

        public Location Location { get; set; }//πολλα σπιτια ανηκουν στην ιδια περιοχη(1-n)

        public virtual ICollection<Feature> Features { get; set; } //πολλα σπιτια εχουν πολλα χαρακτηριστικα(n-n)
    }
}