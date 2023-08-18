using MaisSaude.Business.CadastroClinica;
using MaisSaude.Models.tUser.tClinica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MaisSaude.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClinicaController : ControllerBase
    {

        private readonly ICadastroClinica _CadastroClinica;

        public ClinicaController(ICadastroClinica cadastroClinica)
        {
            _CadastroClinica = cadastroClinica;
        }

        [HttpGet("GetClinicas")]
        [SwaggerOperation(Summary = "Obtem todas as clinicas.", Description = "Obtem todas as clinicas.")]
        public async Task<ActionResult> GetClinicas()
        {
            var result = _CadastroClinica.GetClinicas();
            return Ok(result);
        }
        [HttpGet("GetClinica")]
        [SwaggerOperation(Summary = "Obtem uma clinica.", Description = "Obtem uma clinica.")]
        public async Task<ActionResult> GetClinica(int ID)
        {
            var result = _CadastroClinica.GetClinica(ID);
            return Ok(result);
        }

        [HttpPost("CreateClinica")]
        [SwaggerOperation(Summary = "Criar uma clinica.", Description = "Criar uma clinica.")]
        public async Task<ActionResult> CreateClinica(tClinicaRetorno tClinicaRetorno)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _CadastroClinica.CreateClinica(tClinicaRetorno);
            return Ok();
        }

        [HttpPut("UpdateClinica")]
        [SwaggerOperation(Summary = "Atualizar uma clinica", Description = "Atualizar uma clinica")]
        public async Task<ActionResult> UpdateClinica([FromBody] tClinicaRetorno tClinicaRetorno)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _CadastroClinica.UpdateClinica(tClinicaRetorno);
            return Ok();
        }
    }
}
