using Dapper;
using MaisSaude.Common.Connections;
using MaisSaude.Models.tUser;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace MaisSaude.Business.Users
{
    public class Users : IUsers
    {
        private readonly IOptions<ConnectionDataBase> _DefaultConnection;
        public Users(IConfiguration configuration, IOptions<ConnectionDataBase> defaultConnection)
        {
            _DefaultConnection = defaultConnection;
        }

        public async Task<List<tUser>> GetUserClinica()
        {
            try
            {
                var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection);

                connection.Open();

                var result = connection.Query<tUser>("SELECT * FROM [dbo].[tUser] WHERE [Role] = 'Clinica' ").ToList();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<tUser>> GetUsers()
        {
            try
            {
                var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection);

                connection.Open();

                var result = connection.Query<tUser>("SELECT * FROM [dbo].[tUser]").ToList();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
