using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    internal class Sales
    {
        [Required(ErrorMessage = "Não há voos vinculados a esta venda")]
        public Flights Flight { get; set; }
        [Required(ErrorMessage = "Não a pasageiros vinculados a esta venda")]
        public List<Passengers> Passenger { get; set; } = new List<Passengers>();
        public bool Reserved { get; set; } = false;
        public bool Sold { get; set; } = false;
    }
}
