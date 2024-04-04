using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstateApp.Core.Application.Interfaces.Services;
using System.Reflection;
using RealEstateApp.Infrastructure.Identity.Contexts;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Infrastructure.Identity.Models;

namespace RealEstateApp.Infrastructure.Identity
{
    public static class ServiceRegistration
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            //Configurar el appsettings.json

            #region Contexts
            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<CustomerContext>(options => options.UseInMemoryDatabase("CustomerDB"));
                services.AddDbContext<InternalUserContext>(options => options.UseInMemoryDatabase("InternalDB"));

            }
            else
            {
                services.AddDbContext<CustomerContext>(options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(config.GetConnectionString("IdentityConnection"),
                    m => m.MigrationsAssembly(typeof(CustomerContext).Assembly.FullName));
                });
                services.AddDbContext<InternalUserContext>(options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(config.GetConnectionString("IdentityConnection"),
                    m => m.MigrationsAssembly(typeof(InternalUserContext).Assembly.FullName));
                });
            }
            #endregion

            #region Mapings
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            #endregion

            #region Identity
            services.AddIdentityCore<Customer>()
                .AddUserManager<UserManager<Customer>>()
                .AddRoles<CustomerRole>()
                .AddRoleManager<RoleManager<CustomerRole>>()
                .AddSignInManager<SignInManager<Customer>>()
                .AddEntityFrameworkStores<CustomerContext>()
                .AddTokenProvider<EmailTokenProvider<Customer>>("CustomerProvider");

            services.AddIdentityCore<InternalUser>()
              .AddUserManager<UserManager<InternalUser>>()
              .AddRoles<InternalUserRole>()
              .AddRoleManager<RoleManager<InternalUserRole>>()
              .AddSignInManager<SignInManager<InternalUser>>()
              .AddEntityFrameworkStores<InternalUserContext>()
              .AddTokenProvider<EmailTokenProvider<InternalUser>>("InternalProvider");

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User";
                options.AccessDeniedPath = "/User/AccessDenied";
                options.ReturnUrlParameter = "/AccessDenied";
            });

            //services.Configure<JWTSettings>(config.GetSection("JWTSettings"));
            services.AddAuthentication();
            //services.AddAuthentication(opt =>
            //{
            //    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            //}).AddJwtBearer(opt =>
            //{
            //    opt.RequireHttpsMetadata = true;
            //    opt.SaveToken = false;
            //    opt.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ClockSkew = TimeSpan.Zero,
            //        ValidIssuer = config["JWTSettings:Issuer"],
            //        ValidAudience = config["JWTSettings:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTSettings:Key"]))
            //    };
            //    opt.Events = new JwtBearerEvents()
            //    {


            //        OnAuthenticationFailed = c =>
            //        {
            //            c.NoResult();
            //            c.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //            c.Response.ContentType = ContentType.TextPlain.ToString();
            //            return c.Response.WriteAsync(c.Exception.ToString());
            //        },
            //        OnChallenge = c =>
            //        {
            //            c.HandleResponse();
            //            c.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //            c.Response.ContentType = ContentType.ApplicationJson.ToString();
            //            var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "You are not authorized" });
            //            return c.Response.WriteAsync(result);
            //        },
            //        OnForbidden = c =>
            //        {
            //            c.Response.StatusCode = StatusCodes.Status403Forbidden;
            //            c.Response.ContentType = ContentType.ApplicationJson.ToString();
            //            var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "You are not authorized to access this resource" });
            //            return c.Response.WriteAsync(result);
            //        }
            //    };


            //});
            #endregion

            #region Services
            //services.AddTransient<IAccountService, AccountService>();

            #endregion
        }
        public static void AddIdentityInfrastructureTesting(this IServiceCollection services)
        {
            //Configurar el appsettings.json

            #region Contexts

            services.AddDbContext<CustomerContext>(options => options.UseInMemoryDatabase("CustomerDB"));
            services.AddDbContext<InternalUserContext>(options => options.UseInMemoryDatabase("InternalDB"));


            #endregion


            #region Mapings
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            #endregion


            #region Identity
            //services.AddIdentity<Customer, CustomerRole>()
            //    .AddUserManager<UserManager<Customer>>()
            //    .AddEntityFrameworkStores<CustomerContext>()
            //    .AddDefaultTokenProviders();
            //services.AddIdentity<InternalUser, InternalUserRole>()
            //    .AddUserManager<UserManager<InternalUser>>()
            //    .AddEntityFrameworkStores<InternalUserContext>()
            //    .AddDefaultTokenProviders();

            services.AddIdentityCore<Customer>()
                .AddUserManager<UserManager<Customer>>()
                .AddRoles<CustomerRole>()
                .AddRoleManager<RoleManager<CustomerRole>>()
                .AddSignInManager<SignInManager<Customer>>()
                .AddEntityFrameworkStores<CustomerContext>()
                .AddTokenProvider<EmailTokenProvider<Customer>>("CustomerProvider");

            services.AddIdentityCore<InternalUser>()
              .AddUserManager<UserManager<InternalUser>>()
              .AddRoles<InternalUserRole>()
              .AddRoleManager<RoleManager<InternalUserRole>>()
              .AddSignInManager<SignInManager<InternalUser>>()
              .AddEntityFrameworkStores<InternalUserContext>()
              .AddTokenProvider<EmailTokenProvider<InternalUser>>("InternalProvider");
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User";
                options.AccessDeniedPath = "/User/AccessDenied";
                options.ReturnUrlParameter = "/AccessDenied";
            });

            //services.Configure<JWTSettings>(config.GetSection("JWTSettings"));
            services.AddAuthentication();
            //services.AddAuthentication(opt =>
            //{
            //    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            //}).AddJwtBearer(opt =>
            //{
            //    opt.RequireHttpsMetadata = true;
            //    opt.SaveToken = false;
            //    opt.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ClockSkew = TimeSpan.Zero,
            //        ValidIssuer = config["JWTSettings:Issuer"],
            //        ValidAudience = config["JWTSettings:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTSettings:Key"]))
            //    };
            //    opt.Events = new JwtBearerEvents()
            //    {


            //        OnAuthenticationFailed = c =>
            //        {
            //            c.NoResult();
            //            c.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //            c.Response.ContentType = ContentType.TextPlain.ToString();
            //            return c.Response.WriteAsync(c.Exception.ToString());
            //        },
            //        OnChallenge = c =>
            //        {
            //            c.HandleResponse();
            //            c.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //            c.Response.ContentType = ContentType.ApplicationJson.ToString();
            //            var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "You are not authorized" });
            //            return c.Response.WriteAsync(result);
            //        },
            //        OnForbidden = c =>
            //        {
            //            c.Response.StatusCode = StatusCodes.Status403Forbidden;
            //            c.Response.ContentType = ContentType.ApplicationJson.ToString();
            //            var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "You are not authorized to access this resource" });
            //            return c.Response.WriteAsync(result);
            //        }
            //    };


            //});
            #endregion
            #region Services

            #endregion
        }
    }
}
