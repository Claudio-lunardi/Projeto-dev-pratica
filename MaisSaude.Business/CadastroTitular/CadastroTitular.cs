using Dapper;
using MaisSaude.Common;
using MaisSaude.Common.Connections;
using MaisSaude.Models.tUser;
using MaisSaude.Models.tUser.tTitular;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;


namespace MaisSaude.Business.CadastroTitular
{
    public class CadastroTitular : ICadastroTitular
    {
        private readonly IOptions<ConnectionDataBase> _DefaultConnection;

        public CadastroTitular(IOptions<ConnectionDataBase> connectionDataBase)
        {
            _DefaultConnection = connectionDataBase;
        }

        public List<tUser> GetTitulares()
        {
            using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
            {
                string sql = @"SELECT * FROM tUser WHERE RoleID = 2 --titular";

                return connection.Query<tUser>(sql).ToList();
            }
        }

        public tTitularRetorno GetTitular(int ID)
        {
            try
            {
                tUser tUser = GetUser(ID);
                tUserData tUserData = GetUserData(ID);

                tTitularRetorno tUserRetorno = new tTitularRetorno()
                {
                    tUser = tUser,
                    tUserData = tUserData,
                };

                return tUserRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateTitular(tTitularRetorno tTitularRetorno)
        {
            using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
            {
                var tUserID = Insertuser(tTitularRetorno.tUser);
                tTitularRetorno.tUserData.UserID = tUserID;

                CreateTitular(tUserID);
                InsertUserData(tTitularRetorno.tUserData);
            }
        }

        public void UpdateTitular(tTitularRetorno tTitularRetorno)
        {
            try
            {
                UpdatetUser(tTitularRetorno.tUser);
                UpdatetUserData(tTitularRetorno.tUserData);
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
                tUser.RoleID = (int)Role.Tiular;
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

        private void CreateTitular(int UserID)
        {
            try
            {
                using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
                {
                    string sql = @"INSERT INTO [dbo].[Titular]
                                               ([UserID])
                                         VALUES
                                               (@UserID)";

                    connection.Execute(sql, param: new { UserID });
                }
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




    }
}
