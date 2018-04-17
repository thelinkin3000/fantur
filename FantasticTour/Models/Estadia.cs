using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasticTour.Models
{
    public class Estadia
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public Decimal Costo { get; set; }
        public Hotel Hotel { get; set; }    
    }
}
