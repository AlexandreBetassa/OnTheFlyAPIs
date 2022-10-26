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
        public string ZipCode { get; set; }
        [MaxLength(100)]
        public string Street { get; set; }
        [Required]
        public int Number { get; set; }
        public int Complement { get; set; }
        [Required]
        [MaxLength(30)]
        public string City { get; set; }
        [Required]
        [MaxLength(2)]
        public string State { get; set; }

    }
}
