using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class RestrictedCompany
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        [StringLength(19, ErrorMessage = "Numero de CNPJ Invalido")]
        [JsonPropertyName("CNPJ")]
        public string CNPJ { get; set; }
    }
}
