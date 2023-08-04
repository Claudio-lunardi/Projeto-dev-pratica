using MaisSaude.Business.Agendamento;
using MaisSaude.Business.CadastroClinica;
using MaisSaude.Business.CadastroDependente;
using MaisSaude.Business.CadastroMedico;
using MaisSaude.Business.CadastroTitular;
using MaisSaude.Business.CadastroUser;
using MaisSaude.Business.Login;
using MaisSaude.Business.Users;
using MaisSaude.Common;
using MaisSaude.Common.Connections;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography;

namespace MaisSaude.API.Extensoes
{
    public static class Extensoes
    {
        public static void ConfigurarServicos(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionDataBase>(configuration.GetSection("ConnectionDataBase"));

            services.AddScoped<ILoginUser, LoginUser>();
            services.AddScoped<IUsers, Users>();
            services.AddScoped<IDbtUser, DBtUser>();
            services.AddScoped<ICadastroUser, CadastroUser>();
            services.AddScoped<IAgendamento, Agendamento>();
            services.AddScoped<ICadastroClinica, CadastroClinica>();
            services.AddScoped<ICadastroDependente, CadastroDependente>();
            services.AddScoped<ICadastroMedico, CadastroMedico>();
            services.AddScoped<ICadastroTitular, CadastroTitular>();
        }

        public static void ConfigurarSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo
                {

                    Title = "Api - Claudio ",
                    Version = "v1",
                    Description = "Apis para cadastros"

                });

                c.EnableAnnotations();
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Autenticação JWT",
                    Description = "Informe o token JTW Bearer **_somente_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, Array.Empty<string>() }
                });
            });


        public static void ConfigurarJwt(this IServiceCollection services)
        {
            Environment.SetEnvironmentVariable("JWT_SECRETO",
                    Convert.ToBase64String(new HMACSHA256().Key), EnvironmentVariableTarget.Process);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = "EmitenteDoJWT",
                        ValidAudience = "DestinatarioDoJWT",
                        IssuerSigningKey = new SymmetricSecurityKey(
                        Convert.FromBase64String(Environment.GetEnvironmentVariable("JWT_SECRETO")))
                    };

                });

        }


    }
}
