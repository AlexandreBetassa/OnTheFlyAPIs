using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
<<<<<<<< HEAD:OnTheFlyApi/Models/Address.cs
    public class Address
========
    public class Passengers
>>>>>>>> 13b138f2ce58299bf8bdf6bc99f3268dea3986fd:OnTheFlyApi/Models/Passengers.cs
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
