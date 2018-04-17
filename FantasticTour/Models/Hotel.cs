using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FantasticTour.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public String Direccion { get; set; }
        public String Telefono { get; set; }
        [InverseProperty("Hoteles")]
        public Ciudad Ciudad { get; set; }
    }
}
