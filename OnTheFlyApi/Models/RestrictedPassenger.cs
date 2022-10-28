using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class RestrictedPassenger
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string CPF { get; set; }
    }
    public class RestrictedPassengerDTO
    {
        [Required]
        [StringLength(maximumLength: 11, MinimumLength = 11)]
        public string UnformattedCPF { get; set; }
    }

}
