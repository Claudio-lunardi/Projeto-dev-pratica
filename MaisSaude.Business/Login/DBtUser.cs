﻿using Dapper;
using MaisSaude.Common;
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
            try
            {
                tUser.DataCriacao = DateTime.Now;
                tUser.RoleID = (int)Role.Tiular;

                using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
                {
                    string sql = @"INSERT INTO [dbo].[tUser]
                                                       ([Nome]
                                                       ,[Email]
                                                       ,[Senha]
                                                       ,[DataCriacao]
                                                       ,RoleID
                                                       ,[Ativo])
                                                 VALUES
                                                       (@Nome
                                                       ,@Email
                                                       ,@Senha
                                                       ,@DataCriacao
                                                       ,@RoleID
                                                       ,@Ativo);
                                                        SELECT SCOPE_IDENTITY();";

                    int UserID = connection.ExecuteScalar<int>(sql, param: tUser);
                    return UserID;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public int InsertUserData(tUserData tUserData)
        {
            using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
            {
                string sql = @"INSERT INTO [dbo].[tUserData]
                                                       ([UserID]
                                                       ,[CPF]
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
