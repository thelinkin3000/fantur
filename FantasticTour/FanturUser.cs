using System;
using Microsoft.AspNetCore.Identity;

namespace FantasticTour
{
    public class FanturUser : IdentityUser
    {
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Telefono { get; set; }
    }
}