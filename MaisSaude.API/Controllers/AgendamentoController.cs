using MaisSaude.Business.Agendamento;
using MaisSaude.Models.Agendamento;
using MaisSaude.Models.tUser;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MaisSaude.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {

        private readonly IAgendamento _agendamento;

        public AgendamentoController(IAgendamento agendamento)
        {
            _agendamento = agendamento;
        }

        [HttpPost("CriarAgendamento")]
        public async Task<ActionResult> CriarAgendamento([FromBody] tAgendamento agendamento)
        {
            return Ok(await _agendamento.CriarAgendamento(agendamento));
        }

        [HttpGet("GetMedico")]
        public async Task<ActionResult<IEnumerable<tUser>>> GetMedico()
        {
            return Ok(await _agendamento.GetMedico());
        }

        [HttpGet("GetAgendamentos")]
        public async Task<ActionResult<IEnumerable<tAgendamento>>> GetAgendamentos(int ID)
        {
            return Ok(await _agendamento.GetAgendamentos(ID));
        }


    }
}
