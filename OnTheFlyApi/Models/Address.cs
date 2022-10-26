using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Address
    {
        
        [Required]
        [MaxLength(9)]
        [JsonProperty("cep")]
        public string ZipCode { get; set; }
        [MaxLength(100)]
        [JsonProperty("logradouro")]
        public string Street { get; set; }
        [Required]
        public int Number { get; set; }
        public string Complement { get; set; }
        //[Required]
        [MaxLength(30)]
        [JsonProperty("localidade")]
        public string City { get; set; }
        //[Required]
        [MaxLength(2)]
        [JsonProperty("uf")]
        public string State { get; set; }
        
    }
}
