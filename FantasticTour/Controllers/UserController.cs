using System;
using System.Threading.Tasks;
using FantasticTour.Models.ViewModels;
using FantasticTour.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FantasticTour.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
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

        [Route("/api/[controller]/[action]")]
        public async Task<IActionResult> Login([FromBody] LoginVm vm)
        {
            string token = await _userService.Login(vm);
            if (token == null)
                token = "Ah ah ah, no dijiste la palabra m�gica"; 
            return new OkObjectResult(token);
        }
    }
}