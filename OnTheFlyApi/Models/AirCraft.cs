using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    internal class AirCraft
    {        
        public string Rab { get; set; }
        public int Capacity { get; set; }
        public DateTime DtRegistry { get; set; }
        public DateTime DtLastFlight { get; set; }
        public Company Company { get; set; }
    }
}
