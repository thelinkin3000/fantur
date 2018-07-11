using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasticTour.Models;
using FantasticTour.Models.ViewModels;
using FantasticTour.Repository;
using FantasticTour.Service;
using FantasticTour.URF;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FantasticTour
{
    public class Startup
    {
        private SecurityKey _signInKey;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateFormatString = "dd/MM/yyyy";
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>(options => options.UseNpgsql(Configuration.GetConnectionString("FanturDatabase")));
            var storage = new PostgreSqlStorage(Configuration.GetConnectionString("FanturDatabase"));
            Hangfire.GlobalConfiguration.Configuration.UseStorage(storage);
            services.AddHangfire(c => c.UseStorage(storage));
            services.AddHangfire(config => config.UseStorage(storage));

            _signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = Configuration["Jwt:Issuer"];
                options.Audience = Configuration["Jwt:Audience"];
                options.SigningCredentials = new SigningCredentials(_signInKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Configuration["Jwt:Issuer"],

                ValidateAudience = true, 
                ValidAudience = Configuration["Jwt:Audience"],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signInKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddDbContext<IdentityContext>(
                options => options.UseNpgsql(Configuration.GetConnectionString("FanturDatabase")));

            services.AddIdentity<FanturUser, IdentityRole>(config =>
                    config.SignIn.RequireConfirmedEmail = true
                    )
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = Configuration["Jwt:Issuer"];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Jwt.Role, Constants.Jwt.ApiAccess));
            });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 2;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                // If the LoginPath isn't set, ASP.NET Core defaults 
                // the path to /Account/Login.
                options.LoginPath = "/Account/Login";
                // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                // the path to /Account/AccessDenied.
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            //Register services

            services.AddTransient<IRepository<Hotel>, Repository<Hotel>>();
            services.AddTransient<IRepository<Pais>, Repository<Pais>>();
            services.AddTransient<IRepository<Ciudad>, Repository<Ciudad>>();
            services.AddTransient<IRepository<Transporte>, Repository<Transporte>>();
            services.AddTransient<IRepository<Atraccion>, Repository<Atraccion>>();
            services.AddTransient<IRepository<Estadia>, Repository<Estadia>>();
            services.AddTransient<IRepository<Paquete>, Repository<Paquete>>();
            services.AddTransient<IRepository<PaqueteContratado>, Repository<PaqueteContratado>>();
            services.AddTransient<IService<Hotel>, Service<Hotel>>();
            services.AddTransient<IService<Pais>, Service<Pais>>();
            services.AddTransient<IService<Ciudad>, Service<Ciudad>>();
            services.AddTransient<IService<Transporte>, Service<Transporte>>();
            services.AddTransient<IService<Atraccion>, Service<Atraccion>>();
            services.AddTransient<IService<Estadia>, Service<Estadia>>();
            services.AddTransient<IService<Paquete>, Service<Paquete>>();
            services.AddTransient<IService<PaqueteContratado>, Service<PaqueteContratado>>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAutocompleteService, AutocompleteService>();
            services.AddTransient<IMapperService, MapperService>();
            services.AddTransient<IMailingService, MailingService>();

        }

        // This method gets caljled by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseHangfireServer();
            app.UseHangfireDashboard();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
