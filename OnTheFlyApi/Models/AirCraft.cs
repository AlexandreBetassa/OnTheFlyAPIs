using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class AirCraft
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [JsonPropertyName("Id")]

        public string Id { get; set; }
        [Required]
        [StringLength(6, ErrorMessage = "Invalid RAB Code. Maximum restriction of 6 characters.")]
        [JsonPropertyName("RAB")]
        public string RAB { get; set; }

        [Required]
        [Range(1, 999, ErrorMessage = "Aircraft Capacity must have a numeric value Integer between 1 to 999.")]
        [JsonPropertyName("Capacity")]
        public int Capacity { get; set; }

        [JsonPropertyName("DtRegistry")]
        public DateTime DtRegistry { get; set; }

        [JsonPropertyName("DtLastFlight")]
        public DateTime? DtLastFlight { get; set; }

        [Required]
        [JsonPropertyName("Company")]
        public Company Company { get; set; }
    }

    public class AirCraftDTO
    {
        [Required]
        [StringLength(6, ErrorMessage = "Invalid RAB Code. Maximum restriction of 6 characters.")]
        public string RAB { get; set; }

        [Required]
        [Range(1, 999, ErrorMessage = "Aircraft Capacity must have a numeric value Integer between 1 to 999.")]
        public int Capacity { get; set; }

        public DateTime DtRegistry { get; set; }

        public DateTime? DtLastFlight { get; set; }

        [Required]
        public Company CompanyCnpj { get; set; }
    }
}
