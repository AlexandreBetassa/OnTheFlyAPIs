using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Models
{
    public class Company
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string CNPJ { get; set; }
        public string Name{ get; set; }
        public string NameOp { get; set; }
        public DateTime DtOpen { get; set; }
        public bool? Status { get; set; }
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
