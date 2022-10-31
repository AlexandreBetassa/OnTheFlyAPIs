using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class Company
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        [JsonPropertyName("CNPJ")]
        public string CNPJ { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("NameOp")]
        public string NameOp { get; set; }
        [JsonPropertyName("DtOpen")]
        public DateTime DtOpen { get; set; }
        [JsonPropertyName("Status")]
        public bool? Status { get; set; }
        [JsonPropertyName("Address")]
        public Address Address { get; set; }

    }
    public class CompanyDTO
    {
        [Required]
        [StringLength(19, ErrorMessage = "Numero de CNPJ Invalido")]
        public string CNPJ { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Numeros de caracter permitidos excedidos")]
        public string Name { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Numeros de caracter permitidos excedidos")]
        public string NameOp { get; set; }
        public DateTime DtOpen { get; set; }

        public AddressDTO Address { get; set; }
    }
}
