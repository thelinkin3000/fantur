using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FantasticTour.Models
{
    public class Ciudad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Pais Pais { get; set; }
        [InverseProperty("Ciudad")]
        public virtual ICollection<Hotel> Hoteles { get; set; }
    }
}
