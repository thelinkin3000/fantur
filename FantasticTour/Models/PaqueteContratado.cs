using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasticTour.Models
{
    public class PaqueteContratado
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public Paquete Paquete { get; set; }
        public int Cantidad { get; set; }
        public string UserId { get; set; }
    }
}
