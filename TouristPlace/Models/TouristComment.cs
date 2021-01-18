using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TouristPlace.Models
{
    public class TouristComment
    {
        [Key]
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime CommentDate { get; set; }
        public int ToristPlaceId { get; set; }          
        public int TouristRegistrationId { get; set; }
        [NotMapped]
        public string TouristName { get; set; }


    }
}