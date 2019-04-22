using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace BookNGo.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start Date Required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "End Date Required")]        
        public DateTime EndDate { get; set; }

        [Display(Name = "Occupants Number ")]
        [Required(ErrorMessage = "Occupants Number Required")]        
        public int NumberOfOccupants { get; set; }

        [Display(Name = "Date Of Booking")]
        [Required(ErrorMessage = "Date of booking Required")]
        public DateTime DateOfBooking { get; set; }

        public string Comments { get; set; }

        [Display(Name = "Price Charged")]
        [Required(ErrorMessage = "Price Charged Required")]
        public Decimal PriceCharged { get; set; }

        public User User { get; set; } //πολλα Reservation ανηκουν σε εναν χρηστη(1-n)

        public House House { get; set; } //Πολλα Reservation ανηκουν σε ενα σπιτι (1-n)

    }
}