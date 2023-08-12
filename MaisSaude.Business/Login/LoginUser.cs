using Dapper;
using MaisSaude.Business.Users;
using MaisSaude.Common;
using MaisSaude.Common.Connections;
using MaisSaude.Models.tUser;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace MaisSaude.Business.Login
{
    public class LoginUser : ILoginUser
    {
        private readonly IDbtUser _IDbtUser;
        private readonly IOptions<ConnectionDataBase> _DefaultConnection;

        public LoginUser(IConfiguration configuration, IDbtUser iDbtUser, IOptions<ConnectionDataBase> defaultConnection)
        {

            _IDbtUser = iDbtUser;
            _DefaultConnection = defaultConnection;
        }


        public async Task<tUser> AuthenticarUsuario(LoginRequisicao loginRequisicao)
        {
            try
            {
                //criptografando a senha do usuário usando método de extensão
                loginRequisicao.Senha = loginRequisicao.Senha.GerarHash();

                using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
                {
                    string sql = @"SELECT * FROM [dbo].[tUser]
                                   WHERE Lower([Email]) = Lower(@Email)
                                   AND [Senha] = @Senha
                                   AND [Ativo] = 1";

                    var r = await connection.QueryFirstOrDefaultAsync<tUser>(sql: sql, param: loginRequisicao);
                    return r;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CreateUser(tUserRetorno tUserRetorno)
        {
            try
            {
                //criptografando a senha do usuário usando método de extensão
                tUserRetorno.tUser.Senha = tUserRetorno.tUser.Senha.GerarHash();

                tUserRetorno.tUserData.UserID = _IDbtUser.Insertuser(tUserRetorno.tUser);

                CreateTitular(tUserRetorno.tUserData.UserID);

                _IDbtUser.InsertUserData(tUserRetorno.tUserData);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<tUserRetorno> GettUser(int ID)
        {
            try
            {
                tUser tUser = GetUser(ID);
                tUserData tUserData = GetUserData(ID);

                tUserRetorno tUserRetorno = new tUserRetorno()
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


        public async Task<bool> UpdateUser(tUserRetorno tUserRetorno)
        {
            try
            {
                UpdatetUser(tUserRetorno.tUser);
                UpdatetUserData(tUserRetorno.tUserData);
                return true;
            }
            catch (Exception)
            {

                throw;
            }



        }


        #region PRIVATE

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


        public void UpdatetUserData(tUserData tUserData)
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


        #endregion

    }


}
