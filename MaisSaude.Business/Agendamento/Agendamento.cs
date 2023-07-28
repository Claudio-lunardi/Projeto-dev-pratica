using Dapper;
using MaisSaude.Models.Agendamento;
using MaisSaude.Models.tUser;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Business.Agendamento
{
    public class Agendamento : IAgendamento
    {
        private readonly string _configuration;

        public Agendamento(IConfiguration configuration)
        {
            _configuration = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> CriarAgendamento(tAgendamento agendamento)
        {
            try
            {
                var connection = new SqlConnection(_configuration);
                connection.Open();


                string SqlInsert = @"INSERT INTO [dbo].[Agendamentos]
                                                       ([PacienteID]
                                                       ,[Paciente]
                                                       ,[Clinica]
                                                       ,[DataConsulta]
                                                       ,[MedicoEspecialidade]
                                                       ,[Enable])
                                                 VALUES
                                                       (@PacienteID
                                                       ,@Paciente
                                                       ,@Clinica
                                                       ,@DataConsulta
                                                       ,@MedicoEspecialidade
                                                       ,@Enable)";

                var affectedLines = await connection.ExecuteAsync(SqlInsert, agendamento);

                return (affectedLines > 0) ? true : false;


            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<tAgendamento>> GetAgendamentos(int ID)
        {
            var connection = new SqlConnection(_configuration);
            connection.Open();
            var result = await connection.QueryAsync<tAgendamento>("SELECT * FROM [Agendamentos] WHERE [PacienteID] = @ID", param: new { ID });
            return result;
        }

        public async Task<IEnumerable<tUser>> GetMedico()
        {
            var connection = new SqlConnection(_configuration);
            connection.Open();

            return await connection.QueryAsync<tUser>("SELECT * FROM [tUser] WHERE [Role] = 'Medico'");
        }

    }
}
