using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Airport
    {
        [Required]
        [StringLength(3, ErrorMessage = "Tamanho limite do campo IATA é de 3 caracteres")]
        public string IATA { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "Tamanho limite do campo State é de 2 caracteres")]
        public string State { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "Tamanho limite do campo Country é de 2 caracteres")]
        public string Country { get; set; }
    }
}
