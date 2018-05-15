using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantasticTour.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasticTour
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }

        public DbSet<Atraccion> Atracciones { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Estadia> Estadias { get; set; }
        public DbSet<Hotel> Hoteles { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Paquete> Paquetes { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Transporte> Vuelos{ get; set; }
    }
}
