using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Flight
    {
        //[BsonId]
        //[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

        public Airport Destiny { get; set; }
        public AirCraft Plane { get; set; }
        public int Sales { get; set; }
        public DateTime Departure { get; set; }
        public bool Status { get; set; }
    }
}
