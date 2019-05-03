using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookNGo.Models
{
    public class Location
    {     
        [Key]
        public int LocationId { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "Location Required")]
        [StringLength(255)]
        public string LocationName { get; set; }

    }
}