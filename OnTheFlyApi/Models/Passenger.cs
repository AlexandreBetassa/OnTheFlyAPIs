using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Address Address { get; set; }

    }

    public class PassengerDTO
    {

        [Required]
        [StringLength(11)]
        public string UnformattedCPF { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(1)]
        public string Gender { get; set; }
        [StringLength(14)]
        public string PhoneOpt { get; set; }
        [Required]
        public DateTime DtBirth { get; set; }
        [Required]
        public AddressDTO Address { get; set; }

    }
}
