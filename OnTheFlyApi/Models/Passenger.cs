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
        //[Required]
        //[MaxLength(14)]
        public string CPF { get; set; }
        //[Required]
        //[MaxLength(30)]
        public string Name { get; set; }
        //[Required]
        //[MaxLength(1)]
        public string Gender { get; set; }
        //[MaxLength(14)]
        public string Phone { get; set; }
        //[Required]
        public DateTime DtBirth { get; set; }
        //[Required]
        public DateTime DtRegister { get; set; }
        //[Required]
        public bool Status { get; set; }
        //[Required]
        //public Address Address { get; set; }

    }
}
