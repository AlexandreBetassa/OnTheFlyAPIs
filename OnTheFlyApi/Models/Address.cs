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
        [StringLength(9, ErrorMessage = "Tamanho máximo do campo ZipCode 9 caracteres")]
        public string ZipCode { get; set; }
        [StringLength(100, ErrorMessage = "Tamanho máximo do campo Logradouro 100 caracteres")]
        public string Logradouro { get; set; }
        public int Number { get; set; }
        [StringLength(10, ErrorMessage = "Tamanho máximo do campo Complement 10 caracteres")]
        public string Complement { get; set; }
        [StringLength(30, ErrorMessage = "Tamanho máximo do campo City 30 caracteres")]
        public string City { get; set; }
        [StringLength(2, ErrorMessage = "Tamanho máximo do campo State 2 caracteres")]
        public string State { get; set; }
    }
}
