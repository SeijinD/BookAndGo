using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookNGo.Models
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }

        [Required(ErrorMessage = "ImageUrl Required")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        [DisplayName("Upload House Image")]

        [Display(Name = "House Name")]
        [Required(ErrorMessage = "HouseId Required")]
        public House HouseId { get; set; }


        [NotMapped]
        public HttpPostedFileBase File { get; set; }
    }
}