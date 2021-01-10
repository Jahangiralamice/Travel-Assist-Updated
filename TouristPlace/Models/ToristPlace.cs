using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace TouristPlace.Models
{
    public class ToristPlace
    {
        [Key]
        public int ToristPlaceId { get; set; }
        [Required]
        public string Title { get; set; }
      
        public string ImageUrl { get; set; }
        [Required]
        public string Description { get; set; }
        [NotMapped]
        [Required]
        //[Display(Name ="Image")]
        public HttpPostedFileBase PlaceImage { get; set; }
        public int TouristRegistrationId { get; set; }
        [ForeignKey("TouristRegistrationId")]
        public virtual TouristRegistration TouristRegistration { get; set; }       
      

    }
}