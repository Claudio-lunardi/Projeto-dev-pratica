﻿using Dapper;
using MaisSaude.Common.Connections;
using MaisSaude.Models.tUser;
using MaisSaude.Models.tUser.tTitular;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace MaisSaude.Business.CadastroUser
{
    public class CadastroUser : ICadastroUser
    {
        private readonly IOptions<ConnectionDataBase> _DefaultConnection;

        public CadastroUser(IOptions<ConnectionDataBase> connectionDataBase)
        {
            _DefaultConnection = connectionDataBase;
        }

        public async Task<List<tUser>> GetUsers()
        {
            try
            {
                using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
                {
                    string sql = @"SELECT tUser.[ID]
                                       ,tUser.[Nome]
                                       ,tUser.[Email]
                                       ,tUser.[Senha]
                                       ,tRole.[Role]
                                       ,tUser.[DataCriacao]
                                       ,tUser.[Ativo]
                                   FROM [dbo].[tUser]
                                   LEFT JOIN tRole ON tRole.ID = tUser.RoleID";
                    return connection.Query<tUser>(sql).ToList();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> InsertTitular(tTitularRetorno tTitularRetorno)
        {
            try
            {
                InsertTabelaTitular(tTitularRetorno);
                UpdateRole(tTitularRetorno);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private bool InsertTabelaTitular(tTitularRetorno tTitularRetorno)
        {
            using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
            {
                string sql = @"INSERT INTO [dbo].[Titular]
                                                     ([UserID]
                                                     ,[Identificacao])
                                               VALUES
                                                     (@UserID
                                                     ,@Identificacao)";
                connection.ExecuteScalar(sql, tTitularRetorno.tTitular);
                return true;
            }
        }

        private bool UpdateRole(tTitularRetorno tTitularRetorno)
        {

            using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
            {
                string sql = @"UPDATE [dbo].[tUser]
                                       SET [RoleID] = @RoleID
                                     WHERE ID = @ID";
                connection.ExecuteScalar(sql, tTitularRetorno.tUser);
                return true;
            }


        }

    }
}
