using MaisSaude.Business.CadastroMedico;
using MaisSaude.Models.tUser.tMedico;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MaisSaude.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MedicoController : ControllerBase
    {

        private readonly ICadastroMedico _cadastroMedico;

        public MedicoController(ICadastroMedico cadastroMedico)
        {
            _cadastroMedico = cadastroMedico;
        }

        [HttpGet("GetMedicos")]
        [SwaggerOperation(Summary = "Obtem medicos", Description = "Obtem medicos")]
        public async Task<ActionResult> GetMedicos()
        {

           
            var result = _cadastroMedico.GetMedicos();
            return Ok(result);
        }


        [HttpGet("GetClinicas")]
        [SwaggerOperation(Summary = "Obtem clinicas", Description = "Obtem clinicas")]
        public async Task<ActionResult> GetClinicas()
        {

            var result = _cadastroMedico.GetClinicas();
            return Ok(result);
        }

        [HttpPost("CreateMedico")]
        [SwaggerOperation(Summary = "Criar um médico", Description = "Criar um médico")]
        public async Task<ActionResult> CreateMedico(tMedicoRetorno tMedicoRetorno)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _cadastroMedico.CreateMedico(tMedicoRetorno);
            return Ok();
        }
    }
}

