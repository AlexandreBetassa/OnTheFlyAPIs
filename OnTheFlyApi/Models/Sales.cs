using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    internal class Sales
    {
        public  Flights Flight { get; set; }
        public List<Passengers> passenger { get; set; }
        public bool Reserved { get; set; }
        public bool Sold { get; set; }
    }
}
