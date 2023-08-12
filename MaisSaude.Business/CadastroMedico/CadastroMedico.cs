using Dapper;
using MaisSaude.Common;
using MaisSaude.Common.Connections;
using MaisSaude.Models.tUser;
using MaisSaude.Models.tUser.tMedico;
using MaisSaude.Models.tUser.tTitular;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Business.CadastroMedico
{
    public class CadastroMedico : ICadastroMedico
    {
        private readonly IOptions<ConnectionDataBase> _DefaultConnection;

        public CadastroMedico(IOptions<ConnectionDataBase> connectionDataBase)
        {
            _DefaultConnection = connectionDataBase;
        }



        public List<tUser> GetMedicos()
        {
            using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
            {
                string sql = @"SELECT * FROM tUser WHERE RoleID = 5 --Medico";

                return connection.Query<tUser>(sql).ToList();
            }
        }

        public List<SelectClinica> GetClinicas()
        {
            using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
            {
                string sql = @"SELECT Clinica.ID, tUser.Nome FROM tUser 
                               INNER JOIN Clinica ON Clinica.UserID = tUser.ID";

                return connection.Query<SelectClinica>(sql).ToList();
            }
        }


        public void CreateMedico(tMedicoRetorno tMedicoRetorno)
        {
            using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
            {
                var tUserID = Insertuser(tMedicoRetorno.tUser);
                tMedicoRetorno.tUserData.UserID = tUserID;

                CreateMedic(tMedicoRetorno);
                InsertUserData(tMedicoRetorno.tUserData);
            }
        }




        private void CreateMedic(tMedicoRetorno tMedicoRetorno)
        {
            try
            {
                using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
                {
                    string sql = @"INSERT INTO [dbo].[Medico]
                                                    ([UserID]
                                                    ,[Especialidade]
                                                    ,[ClinicaID])
                                              VALUES
                                                    (@UserID
                                                    ,@Especialidade
                                                    ,@ClinicaID)";

                    connection.Execute(sql, param:
                        new {
                            UserID = tMedicoRetorno.tUserData.UserID,
                            Especialidade = tMedicoRetorno.tMedico.Especialidade,
                            ClinicaID = tMedicoRetorno.tMedico.ClinicaID
                        });
                }
            }
            catch (Exception)
            {
                throw;
            }

        }













        private int Insertuser(tUser tUser)
        {
            try
            {
                tUser.DataCriacao = DateTime.Now;
                tUser.RoleID = (int)Role.Medico;
                tUser.Senha = tUser.Senha.GerarHash();
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
        private void UpdatetUser(tUser tUser)
        {
            tUser.Senha = tUser.Senha.GerarHash();
            using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
            {
                string sql = @"UPDATE [dbo].[tUser]
                                        SET [Nome] = @Nome,
                                            [Email] = @Email,
                                            [Senha] = @Senha,
                                            [Ativo] = @Ativo
                                      WHERE ID = @ID";
                connection.ExecuteScalar(sql, tUser);
            }
        }
        private void InsertUserData(tUserData tUserData)
        {
            try
            {
                tUserData.DateUpdate = DateTime.Now;
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

                    connection.Execute(sql, param: tUserData);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        private void UpdatetUserData(tUserData tUserData)
        {
            using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
            {
                string sql = @"UPDATE [dbo].[tUserData]
                                           SET 
                                               [Telefone] = @Telefone,
                                               [Celular] = @Celular,
                                               [CEP] = @CEP,
                                               [Logradouro] = @Logradouro,
                                               [Bairro] = @Bairro,
                                               [Cidade] = @Cidade,
                                               [Numero] = @Numero,
                                               [UF] = @UF,
                                               [Complemento] = @Complemento,
                                               [DateUpdate] = @DateUpdate
                                         WHERE ID = @ID ";
                connection.ExecuteScalar(sql, tUserData);
            }
        }

    }
}
