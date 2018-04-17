using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasticTour.Models
{
    public class Provincia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Pais Pais { get; set; }
    }
}
