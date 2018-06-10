using System;
using System.Threading.Tasks;
using FantasticTour.Models.ViewModels;
using FantasticTour.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FantasticTour.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("/api/[controller]/[action]")]
        public async Task<IActionResult> Register([FromBody] UserRegisterVm vm)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(
                    new {error = "Algún campo falta o es inválido."});
            }

            try
            {
                await _userService.CreateUser(vm);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(
                    new {error = ex.Message});
            }

            return new OkResult();
        }
    }
}