using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;

namespace FantasticTour.Models.ViewModels
{
    public class LoginVm
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}