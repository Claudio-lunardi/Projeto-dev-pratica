using Dapper;
using MaisSaude.Business.Login;
using MaisSaude.Common.Connections;
using MaisSaude.Models.tUser.tTitular;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Business.Validações
{
    public class Validacao : IValidacao
    {
        private readonly IOptions<ConnectionDataBase> _DefaultConnection;

        public Validacao(IOptions<ConnectionDataBase> defaultConnection)
        {
            _DefaultConnection = defaultConnection;
        }
        public bool ExisteEmail(string Email)
        {
            using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
            {
                string sql = @"SELECT COUNT(*) FROM  tUser WHERE LOWER(Email) = LOWER(@Email)";
                int emailCount = connection.ExecuteScalar<int>(sql, Email);

                if (emailCount > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
