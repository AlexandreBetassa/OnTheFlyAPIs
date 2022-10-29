using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Passenger
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string CPF { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public DateTime DtBirth { get; set; }
        public DateTime DtRegister { get; set; }
        public bool Status { get; set; }
        [JsonPropertyName("Address")]
        public Address Address { get; set; }

    }

    public class PassengerDTO
    {

        [Required]
        [StringLength(maximumLength: 11, MinimumLength = 11)]
        public string UnformattedCPF { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 1, MinimumLength = 1)]
        public string Gender { get; set; }
        [StringLength(maximumLength: 15, MinimumLength = 10)]
        public string PhoneOpt { get; set; }
        [Required]
        public DateTime DtBirth { get; set; }
        [Required]
        public AddressDTO Address { get; set; }
    }

    public class PassengerUpdateDTO
    {

        [Required]
        [StringLength(maximumLength: 11, MinimumLength = 11)]
        public string UnformattedCPF { get; set; }
        [Required]
        [StringLength(30)]
        public string NewName { get; set; }
        [Required]
        [StringLength(maximumLength: 1, MinimumLength = 1)]
        public string NewGender { get; set; }
        [StringLength(maximumLength: 15, MinimumLength = 10)]
        public string NewPhoneOpt { get; set; }
        [Required]
        public AddressDTO NewAddress { get; set; }

    }
}
