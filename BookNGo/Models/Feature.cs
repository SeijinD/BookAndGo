using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookNGo.Models
{
    public class Feature
    {
        [Key]
        public int FeatureId { get; set; }

        [Display(Name = "Name Feature")]
        [Required(ErrorMessage = "Feature Required")]
        [StringLength(255)]
        public string FeatureName { get; set; }

        public virtual ICollection<House> Houses{ get; set; }//πολλα Feature ανηκουν σε πολλα House(n-n)

    }
}