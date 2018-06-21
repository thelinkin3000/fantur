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


        [HttpPost]
        [Route("/api/[controller]/[action]")]
        public async Task<IActionResult> ConfirmMail([FromBody] EmailConfirmVm vm)
        {
            if(!ModelState.IsValid)
                return new OkObjectResult(new RequestResultVm(){Message = "Algún dato faltó.", Valid = false});
            var result = await _userService.ConfirmEmail(vm.UserId, vm.Token);
            if(result != null)
                return new OkObjectResult(new RequestResultVm(){Message = "", Valid = true});
            return new OkObjectResult(new RequestResultVm(){Message = "El token no pudo verificarse", Valid = false});
        }

        [Route("/api/[controller]/[action]")]
        public async Task<IActionResult> CreateRoles()
        {
            try
            {
                await _userService.CreateRoles();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return new OkResult();
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        public async Task<IActionResult> Login([FromBody] LoginVm vm)
        {
            RequestResultVm result = new RequestResultVm();

            string token = await _userService.Login(vm);
            if (token == null)
            {
                result.Valid = false;
                result.Message = "Ah ah ah, no dijiste la palabra magica!";
                return new OkObjectResult(result);
            }
            result.Valid = true;
            result.Message = token;
            return new OkObjectResult(result);
        }

        [Route("/api/[controller]/[action]")]
        public async Task<IActionResult> ResetPassword(string username, string password)
        {
            var result = await _userService.ResetPassword(username, password);
            return new OkObjectResult(result);
        }
    }
}