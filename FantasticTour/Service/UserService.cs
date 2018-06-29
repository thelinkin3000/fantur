using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using FantasticTour.Models.ViewModels;
using Hangfire;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Newtonsoft.Json;

namespace FantasticTour.Service
{
    public interface IUserService
    {
        Task<FanturUser> CreateUser(UserRegisterVm vm);
        Task<string> Login(LoginVm vm);
        Task<FanturUser> ResetPassword(string username, string password);
        Task CreateRoles();
        Task<FanturUser> ConfirmEmail(string userId, string token);
        Task<string> GetTokenForEmail(FanturUser user);
        Task SendConfirmationEmail(FanturUser user);
        bool FireSendConfirmationEmail(FanturUser user);
        List<string> GetMailsForMailing();
    }

    public class UserService : IUserService
    {
        private readonly UserManager<FanturUser> _userManager;
        private readonly SignInManager<FanturUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private JwtIssuerOptions jwtIssuerOptions;
        private IdentityContext _identityContext;

        public UserService(UserManager<FanturUser> userManager, SignInManager<FanturUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager, IdentityContext identityContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _identityContext = identityContext;
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
            await _userManager.AddToRoleAsync(newUser, "user");
            FireSendConfirmationEmail(newUser);
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

        public async Task<FanturUser> ResetPassword(string username, string password)
        {
            FanturUser user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return null;
            var result = await _userManager.RemovePasswordAsync(user);
            if (result.Succeeded)
            {
                var result2 = await _userManager.AddPasswordAsync(user, password);
                if (result2.Succeeded)
                {
                    return user;
                }
            }
            return null;
        }

        public async Task CreateRoles()
        {
            List<string> roles = new List<string>(){"admin", "user"};
            foreach (string role in roles)
            {
                bool roleExists = await _roleManager.RoleExistsAsync(role);
                if(!roleExists)
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        public async Task<FanturUser> ConfirmEmail(string userId, string token)
        {
            FanturUser user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return null;
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
                return null;
            return user;
        }

        public async Task<string> GetTokenForEmail(FanturUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        private string GetLinkForMail(FanturUser user, string token)
        {
            string tokenstring = WebUtility.UrlEncode(token);
            return $"http://localhost:57770/confirmation?userId={user.Id}&token={tokenstring}";
        }

        public async Task SendConfirmationEmail(FanturUser user)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Fantur", "fan@tour.com"));
            message.To.Add(new MailboxAddress("", user.Email));
            message.Subject = "Confirmación de cuenta";

            message.Body = new TextPart("plain")
            {
                Text = $"Hola {user.Nombre}!\r\nHaciendo click en el siguiente link vas a poder confirmar tu cuenta de correo:\r\n{GetLinkForMail(user,await GetTokenForEmail(user))}\r\nTe esperamos!\r\n\r\n\r\nEl equipo de Fantur."
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("fanturgrupo2@gmail.com", "fanturdacs");
                client.Send(message);
                client.Disconnect(true);
            }
       }

        public bool FireSendConfirmationEmail(FanturUser user)
        {
            BackgroundJob.Enqueue(() => SendConfirmationEmail(user));
            return true;
        }


        public async Task<string> GenerateEncodedToken(FanturUser user)
        {
            var claimsList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, await jwtIssuerOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(jwtIssuerOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64)
            };
            foreach (string role in await GetRolesForJwt(user))
            {
                claimsList.Add(new Claim("roles",role));
            }
            claimsList.Add(new Claim("roles", "dummy"));
            var claims = claimsList.ToArray();
            

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

        public List<string> GetMailsForMailing()
        {
            return _identityContext.Users.Select(u => u.Email).ToList();
        }



        private async Task<List<string>> GetRolesForJwt(FanturUser user)
        {
            return (await _userManager.GetRolesAsync(user)).ToList();
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() -
                                 new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
    }
}
