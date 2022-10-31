using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Flight
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        [Required]
        [JsonPropertyName("Destiny")]
        public Airport Destiny { get; set; }

        //[Required]
        [JsonPropertyName("Plane")]
        public AirCraft Plane { get; set; }

        [Required]
        //[StringLength(3, ErrorMessage = "Tamanho limite do campo Sales é de 3 caracteres")]
        [JsonPropertyName("Sales")]
        public int Sales { get; set; }

        [Required]
        [JsonPropertyName("Departure")]
        public DateTime Departure { get; set; }

        [Required]
        [JsonPropertyName("Status")]
        public bool Status { get; set; }
    }
}
