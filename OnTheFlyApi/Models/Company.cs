using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class Company
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        public string CNPJ { get; set; }
        [Required]
        public string Name{ get; set; }
        public string NameOp { get; set; }
        public DateTime Date { get; set; }
        public bool? Status { get; set; }

        public Address Address { get; set; }

    }
}
