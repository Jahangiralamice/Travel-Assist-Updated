using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TouristPlace.Models
{
    public class TouristRegistration
    {
        [Key]
        public int TouristRegistrationId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Maximum length should be 100 characters")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNo { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }

       
    }
}