using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using ProyectoLuisOrellana.Aplication.General;
using System.Text;
using ProyectoLuisOrellana.Infrastructure.DAL.DbConecction;
namespace ProyectoLuisOrellana.Base.Api
{
    public static class ConfigureBase
    {

        public static void ConfigureBaseManager(this IServiceCollection services, IConfiguration config)
        {

            var key = Encoding.ASCII.GetBytes(config["Jwt:Key"]);

            services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new DefaultNamingStrategy() // Mantiene PascalCase
        };

    });
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true; // Desactiva la validación automática del estado del modelo
            });

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,

                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[]{}
        }
    });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    //ValidIssuer = false,
                    //ValidAudience = false,

                };
                options.Events = new JwtBearerEvents
                {

                    OnTokenValidated = async context =>
                    {

                        var httpContext = context.HttpContext;
                        var logonName = context.Principal?.FindFirst(JwtRegisteredClaimNames.Name)?.Value;
                        var organizationId = context.Principal?.FindFirst("OrganizationId")?.Value; // Claim personalizado

                        if (!string.IsNullOrEmpty(logonName) && !string.IsNullOrEmpty(organizationId))
                        {
                            // Guardar en HttpContext.Items
                            httpContext.Items["LogonName"] = logonName;
                            httpContext.Items["OrganizationId"] = organizationId;
                        }
                        //var tokenType = context.SecurityToken.GetType().FullName;
                        //Console.WriteLine($"SecurityToken Type: {tokenType}");
                        // Verifica si el token es un JWT
                        if (context.SecurityToken is JsonWebToken jwtToken)
                        {
                            // Aquí puedes trabajar con jwtToken
                            var tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();
                            var isValid = await tokenService.IsTokenValidAsync(jwtToken.EncodedToken);

                            if (!isValid)
                            {
                                context.Fail("Token has been revoked or is invalid.");
                            }
                        }
                        else
                        {
                            // Si no es un JWT válido, marca el contexto como fallido
                            context.Fail("Invalid token format.");
                        }
                    },
                  
                };

            });
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddHttpContextAccessor();
            services.ConfigureSqlContextManager(config);
            services.ConfigureRepositoryManager();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IErrorLogService, ErrorLogService>();
            services.AddScoped<IIntentoService, IntentoService>();


        }
    }
}
