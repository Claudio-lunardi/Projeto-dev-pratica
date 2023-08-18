using Dapper;
using MaisSaude.Common;
using MaisSaude.Common.Connections;
using MaisSaude.Models.tUser;
using MaisSaude.Models.tUser.tClinica;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace MaisSaude.Business.CadastroClinica
{
    public class CadastroClinica : ICadastroClinica
    {
        private readonly IOptions<ConnectionDataBase> _DefaultConnection;

        public CadastroClinica(IOptions<ConnectionDataBase> connectionDataBase)
        {
            _DefaultConnection = connectionDataBase;
        }
        public List<tUser> GetClinicas()
        {
            using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
            {
                string sql = @"SELECT * FROM tUser WHERE RoleID = 4 --titular";
                return connection.Query<tUser>(sql).ToList();
            }
        }

        public void CreateClinica(tClinicaRetorno tClinicaRetorno)
        {
            using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
            {
                var UserID = Insertuser(tClinicaRetorno.tUser);
                tClinicaRetorno.tUserData.UserID = UserID;

                PostClinica(UserID);
                InsertUserData(tClinicaRetorno.tUserData);
            }
        }

        private void PostClinica(int UserID)
        {
            try
            {
                using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
                {
                    string sql = @"INSERT INTO [dbo].[Clinica]
                                                   ([UserID])
                                             VALUES
                                                   (@UserID)";
                    connection.Execute(sql, param: new { UserID = UserID });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private int Insertuser(tUser tUser)
        {
            try
            {
                tUser.DataCriacao = DateTime.Now;
                tUser.RoleID = (int)Role.Clinica;
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
        public void UpdateClinica(tClinicaRetorno tClinicaRetorno)
        {
            try
            {
                UpdatetUser(tClinicaRetorno.tUser);
                UpdatetUserData(tClinicaRetorno.tUserData);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public tClinicaRetorno GetClinica(int ID)
        {
            try
            {
                tUser tUser = GetUser(ID);
                tUserData tUserData = GetUserData(ID);

                tClinicaRetorno tClinicaRetorno = new tClinicaRetorno()
                {
                    tUser = tUser,
                    tUserData = tUserData,
                };

                return tClinicaRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private tUser GetUser(int ID)
        {
            using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
            {
                string sql = @"SELECT * FROM tUser WHERE ID = @ID";
                var tUser = connection.QueryFirstOrDefault<tUser>(sql: sql, param: new { ID });
                return tUser;
            }
        }
        private tUserData GetUserData(int ID)
        {
            using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
            {
                string sql = @"SELECT * FROM tUserData WHERE UserID = @ID";
                var tUserData = connection.QueryFirstOrDefault<tUserData>(sql: sql, param: new { ID });
                return tUserData;
            }
        }
        private void UpdatetUser(tUser tUser)
        {
            using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
            {
                string sql = @"UPDATE [dbo].[tUser]
                                        SET [Nome] = @Nome,
                                            [Email] = @Email,
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
