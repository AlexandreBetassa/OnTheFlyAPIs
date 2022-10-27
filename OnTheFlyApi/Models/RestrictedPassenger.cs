using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [StringLength(14)]
        public string CPF { get; set; }
    }

}
