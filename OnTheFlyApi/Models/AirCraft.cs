using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AirCraft
    {

        [Required]
        [StringLength(6, ErrorMessage = "Invalid RAB Code. Maximum 5 Characteres")] /// perguntar se o ID(RAB) da Aeronave vai ser 5 char (Sem formatação) ou 6 char (Com Formatação -)
        public string RAB { get; set; }
        [Required]
        [Range(1, 999, ErrorMessage = "Capacidade da Aeronave precisa constar um valor numerico Inteiro entre 1 a 999.")]
        public int Capacity { get; set; }


        public DateTime DtRegistry { get; set; }


        public DateTime? DtLastFlight { get; set; }
        [Required]
        public Company Company { get; set; }


    }
}
