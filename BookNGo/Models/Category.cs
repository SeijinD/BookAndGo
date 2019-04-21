using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookNGo.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Display(Name = "Name Category")]
        [Required(ErrorMessage = "Category Required")]
        [StringLength(255)]
        public string CategoryName { get; set; }

    }
}