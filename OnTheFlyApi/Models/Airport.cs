using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Airport
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [JsonPropertyName("Id")]
        public string Id { get; set; }
        [Required]
        [StringLength(3, ErrorMessage = "Tamanho limite do campo IATA é de 3 caracteres")]
        [JsonPropertyName("iata")]
        public string IATA { get; set; }

        [Required]
        [JsonPropertyName("time_zone_id")]
        public string TimeZoneId { get; set; }

        [Required]
        [JsonPropertyName("country_id")]
        public string Country { get; set; }

        [JsonPropertyName("name")]
        public string NameAirport { get; set; }

        [JsonPropertyName("city_code")]
        public string CityCode { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("elevation")]
        public string Elevation { get; set; }

        [JsonPropertyName("icao")]
        public string Icao { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }
    }
}
