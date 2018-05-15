using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FantasticTour.Models
{
    [Table("Transporte")]
    public class Transporte
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public Decimal Costo { get; set; }
        public Ciudad Origen { get; set; }
        public Ciudad Destino { get; set; }
        public TipoTransporte TipoTransporte { get; set; }
    }
}
