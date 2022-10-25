using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Flight
    {
        //[BsonId]
        //[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

        [Required]
        public Airport Destiny { get; set; }

        [Required]
        public AirCraft Plane { get; set; }

        [Required]
        public int Sales { get; set; }

        [Required]
        public DateTime Departure { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
