using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Sale
    {
        [Required(ErrorMessage = "There are no flights linked to this sale")]
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [JsonPropertyName("Id")]
        public string Id { get; set; }
        public Flight Flight { get; set; }
        [Required(ErrorMessage = "No to passengers linked to this sale")]
        public List<Passenger> Passenger { get; set; }
        public bool Reserved { get; set; } = false;
        public bool Sold { get; set; } = false;
    }

    public class SaleDTO
    {
        [Required(ErrorMessage = "Date in invalid format")]
        public DateTime DtFlight { get; set; }
        public List<string> PassengersCPFs { get; set; }
        [Required(ErrorMessage = "Inform the aircraft")]
        [MaxLength(6, ErrorMessage = "Invalid format registration")]
        public string RAB { get; set; }
        public string Destiny { get; set; }
        public bool Reserved { get; set; }
    }
}
