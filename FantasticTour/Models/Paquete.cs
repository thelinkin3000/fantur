using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasticTour.Models
{
    public class Paquete
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool Disponible { get; set; }
        public Vuelo Vuelo { get; set; }
        public Estadia Estadia { get; set; }
        public Atraccion Atraccion { get; set; }
    }
}
