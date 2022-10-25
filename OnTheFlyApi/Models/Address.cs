using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    internal class Adress
    {
        [Required]
        [StringLength(9)]
        public string ZipCode { get; set; }
        [StringLength(100)]
        public string Logradouro { get; set; }
        public int Number { get; set; }
        [StringLength(10)]
        public string Complement { get; set; }
        [StringLength(30)]
        public string City { get; set; }
        [StringLength(2)]
        public string State { get; set; }
    }
}
