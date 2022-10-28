using MongoDB.Bson.Serialization.Attributes;
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
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }


        [Required]
        [StringLength(6, ErrorMessage = "Invalid RAB Code. Maximum restriction of 6 characters.")] /// perguntar se o ID(RAB) da Aeronave vai ser 5 char (Sem formatação) ou 6 char (Com Formatação -)
        public string RAB { get; set; }


        [Required]
        [Range(1, 999, ErrorMessage = "Aircraft Capacity must have a numeric value Integer between 1 to 999.")]
        public int Capacity { get; set; }


        public DateTime DtRegistry { get; set; }


        public DateTime? DtLastFlight { get; set; }


        [Required]
        public Company Company { get; set; }


    }
}
