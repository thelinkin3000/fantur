using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasticTour.Models
{
    public class Vuelo
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public Decimal Costo { get; set; }
        public Ciudad Origen { get; set; }
        public Ciudad Destino { get; set; }
    }
}
