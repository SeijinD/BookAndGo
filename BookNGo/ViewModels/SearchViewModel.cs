using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BookNGo.Models;

namespace BookNGo.ViewModels
{
    public class SearchViewModel
    {
        [Required]
        [Display(Name = "Location")]
        public int SearchLocation { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int SearchCategory { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SearchStartDate { get; set; }

        [Required]
        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SearchEndDate { get; set; }

        [Required]
        [Display(Name = "Max Occupancy")]
        public int SearchMaxOccupancy { get; set; }



        public IEnumerable<House> Houses { get; set; }

        public IEnumerable<Reservation> Reservations { get; set; }

    }
}