using Dapper;
using MaisSaude.Common;
using MaisSaude.Common.Connections;
using MaisSaude.Models.tUser;
using MaisSaude.Models.tUser.tDapendente;
using MaisSaude.Models.tUser.tMedico;
using MaisSaude.Models.tUser.tTitular;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace MaisSaude.Business.CadastroDependente
{
    public class CadastroDependente : ICadastroDependente
    {

        private readonly IOptions<ConnectionDataBase> _DefaultConnection;

        public CadastroDependente(IOptions<ConnectionDataBase> connectionDataBase)
        {
            _DefaultConnection = connectionDataBase;
        }

        public List<tUser> GetDependentes()
        {
            using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
            {
                string sql = @"SELECT * FROM tUser WHERE RoleID = 3 --dependente";
                return connection.Query<tUser>(sql).ToList();
            }
        }

        public List<SelectClinica> GetListTitular()
        {
            using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
            {
                string sql = @"SELECT tUser.[Nome], Titular.[ID] FROM tUser
                            INNER JOIN Titular ON Titular.UserID =tUser.ID";
                return connection.Query<SelectClinica>(sql).ToList();
            }
        }
        public void PostDependente(tDependenteRetorno tDependenteRetorno)
        {
            using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
            {
                var tUserID = Insertuser(tDependenteRetorno.tUser);
                tDependenteRetorno.tUserData.UserID = tUserID;

                CreateDependente(tDependenteRetorno);
                InsertUserData(tDependenteRetorno.tUserData);
            }
        } 
        public tDependenteRetorno GetDependente(int ID)
        {
            try
            {
                tUser tUser = GetUser(ID);
                tUserData tUserData = GetUserData(ID);
                tTitular tTitular = GetTitular1(ID);


                tDependenteRetorno tUserRetorno = new tDependenteRetorno()
                {
                    tUser = tUser,
                    tUserData = tUserData,
                    tTitular = tTitular
                };

                return tUserRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }


   

        private void CreateDependente(tDependenteRetorno tDependenteRetorno) 
        {
            try
            {
                using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
                {
                    string sql = @"INSERT INTO [dbo].[Dependente]
                                                   ([TitularID]
                                                   ,[UserID])
                                             VALUES
                                                   (@TitularID
                                                   ,@UserID)";

                    connection.Execute(sql, param: new { TitularID = tDependenteRetorno.tTitular.ID, UserID = tDependenteRetorno.tUserData.UserID }) ;
                  
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
                tUser.RoleID = (int)Role.Dependente;
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


        private tTitular GetTitular1(int UserID) {

            using (var connection = new SqlConnection(_DefaultConnection.Value.DefaultConnection))
            {
                string sql = @"SELECT TitularID AS ID FROM Dependente WHERE UserID = @UserID";
                var tUser = connection.QueryFirstOrDefault<tTitular>(sql: sql, param: new { UserID });
                return tUser;
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



        public void UpdateDependente(tDependenteRetorno tDependenteRetorno)
        {
            try
            {
                UpdatetUser(tDependenteRetorno.tUser);
                UpdatetUserData(tDependenteRetorno.tUserData);
            }
            catch (Exception)
            {
                throw;
            }
        }





    }
}
