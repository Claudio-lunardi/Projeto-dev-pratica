using MaisSaude.ApiToken.Models;
using MaisSaude.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MaisSaude.Extensoes
{
    public static class ServicoExtencoes
    {
        public static void ConfiguraAPI(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DadosBase>(configuration.GetSection("DadosBase"));
        }

        public static void ConfigurarServicos(this IServiceCollection services)
        {
            services.AddScoped<IApiToken, ApiToken>();
            services.AddSingleton<LoginRespostaModel>();
            services.AddHttpClient();
        }



        public static void ConfigurarCookiePolicy(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        public static void ConfigurarAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Login/Login");
                });
        }
    }
}
