using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using FantasticTour.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace FantasticTour.Service
{
    public interface IUserService
    {
        Task<FanturUser> CreateUser(UserRegisterVm vm);
        Task<string> Login(LoginVm vm);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<FanturUser> _userManager;
        private readonly SignInManager<FanturUser> _signInManager;
        private readonly IConfiguration _configuration;
        private JwtIssuerOptions jwtIssuerOptions;

        public UserService(UserManager<FanturUser> userManager, SignInManager<FanturUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            jwtIssuerOptions = new JwtIssuerOptions();
            jwtIssuerOptions.Issuer = _configuration["Jwt:Issuer"];
            jwtIssuerOptions.Audience = _configuration["Jwt:Audience"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            jwtIssuerOptions.SigningCredentials = creds;
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

        public async Task<string> Login(LoginVm vm)
        {
            FanturUser user = await _userManager.FindByNameAsync(vm.Email);
            if (user == null)
                //Handleo
                return null;
            var result = await _signInManager.PasswordSignInAsync(user,vm.Password, true,false);

            if (result.Succeeded)
            {
                var token = await GenerateEncodedToken(user);
                return token;
            }
            return null;   
        }

        public async Task<string> GenerateEncodedToken(FanturUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, await jwtIssuerOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(jwtIssuerOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                new Claim("roles", await GetRolesForJwt(user))
            };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                notBefore: jwtIssuerOptions.NotBefore,
                expires: jwtIssuerOptions.Expiration,
                signingCredentials: jwtIssuerOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private async Task<string> GetRolesForJwt(FanturUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return JsonConvert.SerializeObject(roles);
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() -
                                 new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
    }
}
