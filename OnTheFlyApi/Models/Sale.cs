using Bogus.Bson;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Sale
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [Required(ErrorMessage = "Não há voos vinculados a esta venda")]
        public Flight Flight { get; set; }
        [Required(ErrorMessage = "Não a passageiros vinculados a esta venda")]
        public List<Passenger> Passenger { get; set; } = new List<Passenger>();
        public bool Reserved { get; set; } = false;
        public bool Sold { get; set; } = false;
    }
}
