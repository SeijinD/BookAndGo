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

        public ICollection<Image> Images { get; set; } //ενα σπιτι εχει πολλα Image(1-n)

        public Category Category { get; set; } //ενα category εχει πολλα House(1-n)

        public Location Location { get; set; }//μια περιοχη εχει πολλα House(1-n)

        public int LocationId { get; set; }

        public int CategoryId { get; set; }

        public virtual ICollection<Feature> Features { get; set; } //πολλα House εχουν πολλα Feature(n-n)
    }
}