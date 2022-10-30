using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class RestrictedPassenger
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        [JsonPropertyName("CPF")]
        public string CPF { get; set; }
    }
    public class RestrictedPassengerDTO
    {
        [Required]
        [StringLength(maximumLength: 11, MinimumLength = 11)]
        public string UnformattedCPF { get; set; }
    }

}
