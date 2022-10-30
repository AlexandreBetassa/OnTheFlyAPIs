using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Flight
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        public Airport Destiny { get; set; }

        //[Required]
        public AirCraft Plane { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "Tamanho limite do campo Sales é de 3 caracteres")]
        public int Sales { get; set; }

        [Required]
        public DateTime Departure { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
