using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TouristPlace.Models
{
    public class TouristContext : DbContext
    {
        public TouristContext() : base("TouristContextDB") { }
        public virtual DbSet<TouristComment> TouristComments { get; set; }
        public virtual DbSet<ToristPlace> ToristPlaces { get; set; }

        public virtual DbSet<TouristRegistration> TouristRegistrations { get; set; }

    }
}