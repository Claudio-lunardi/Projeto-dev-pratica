using Dapper;
using MaisSaude.Common.Connections;
using MaisSaude.Models.tUser;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace MaisSaude.Business.Login
{
    public class DBtUser : IDbtUser
    {
        private readonly IOptions<ConnectionDataBase> _DefaultConnection;

        public DBtUser(IOptions<ConnectionDataBase> defaultConnection)
        {
            _DefaultConnection = defaultConnection;
        }

        public int Insertuser(tUser tUser)
        {
            using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
            {
                string sql = @"INSERT INTO [dbo].[tUser]
                                                       ([Nome]
                                                       ,[Usuario]
                                                       ,[Email]
                                                       ,[Senha]
                                                       ,[DataCriacao]
                                                       ,[Ativo])
                                                 VALUES
                                                       (@Nome
                                                       ,@Usuario
                                                       ,@Email
                                                       ,@Senha
                                                       ,@DataCriacao
                                                       ,@Ativo);
                                                        SELECT SCOPE_IDENTITY();";

                int UserID = connection.ExecuteScalar<int>(sql, param: tUser);
                return UserID;
            }
        }

        public int InsertUserData(tUserData tUserData)
        {
            using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
            {
                string sql = @"INSERT INTO [dbo].[tUserData]
                                                       ([UserID]
                                                       ,[CPF]
                                                       ,[CNPJ]
                                                       ,[Telefone]
                                                       ,[Celular]
                                                       ,[CEP]
                                                       ,[Logradouro]
                                                       ,[Bairro]
                                                       ,[Cidade]
                                                       ,[Numero]
                                                       ,[UF]
                                                       ,[Complemento])
                                                 VALUES
                                                       (@UserID
                                                       ,@CPF
                                                       ,@CNPJ
                                                       ,@Telefone
                                                       ,@Celular
                                                       ,@CEP
                                                       ,@Logradouro
                                                       ,@Bairro
                                                       ,@Cidade
                                                       ,@Numero
                                                       ,@UF
                                                       ,@Complemento)";

                int UserID = connection.Execute(sql, param: tUserData);
                return UserID;
            }
        }
    }
}
