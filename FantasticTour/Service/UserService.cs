using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantasticTour.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace FantasticTour.Service
{
    public interface IUserService
    {
        Task<FanturUser> CreateUser(UserRegisterVm vm);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<FanturUser> _userManager;

        public UserService(UserManager<FanturUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<FanturUser> CreateUser(UserRegisterVm vm)
        {
            FanturUser newUser = new FanturUser();
            newUser.FechaNacimiento = vm.FechaNacimiento;
            newUser.Nombre = vm.Nombre;
            newUser.NumeroDocumento = vm.NumeroDocumento;
            newUser.Telefono = vm.Telefono;
            newUser.UserName = vm.Email;
            newUser.Email = vm.Email;
            await _userManager.CreateAsync(newUser, vm.Password);
            return newUser;
        }
    }
}
