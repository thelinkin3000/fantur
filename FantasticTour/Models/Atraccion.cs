using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasticTour.Models
{
    public class Atraccion
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public DateTime Fecha { get; set; } 
        public Decimal Costo { get; set; }
        public Ciudad Ciudad { get; set; }
    }
}
