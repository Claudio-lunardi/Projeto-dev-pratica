
using MaisSaude.Business.Agendamento;
using MaisSaude.Business.Login;
using MaisSaude.Business.Users;
using MaisSaude.Common;
using MaisSaude.Common.Connections;

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
            services.AddScoped<IAgendamento, Agendamento>();
        }
    }
}
