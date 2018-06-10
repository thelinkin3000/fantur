using System;
using System.Collections.Generic;
using System.Linq;

namespace FantasticTour.Models.ViewModels
{
    public class UserRegisterVm
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordAgain { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Sexo { get; set; }
        public string Telefono { get; set; }
    }
}
