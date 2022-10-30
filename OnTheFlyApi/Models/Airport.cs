using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Airport
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "Tamanho limite do campo IATA é de 3 caracteres")]
        [JsonProperty("iata")]
        public string IATA { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "Tamanho limite do campo State é de 2 caracteres")]
        [JsonProperty("time_zone_id")]
        public string TimeZoneId { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "Tamanho limite do campo Country é de 2 caracteres")]
        [JsonProperty("country_id")]
        public string Country { get; set; }

        [JsonProperty("name")]
        public string NameAirport { get; set; }

        [JsonProperty("city_code")]
        public string CityCode { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("elevation")]
        public string Elevation { get; set; }

        [JsonProperty("icao")]
        public string Icao { get; set; }
    }
}
