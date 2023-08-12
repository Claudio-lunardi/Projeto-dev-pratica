using MaisSaude.Business.CadastroTitular;
using MaisSaude.Models.tUser.tTitular;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MaisSaude.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TitularController : ControllerBase
    {
        private readonly ICadastroTitular _cadastroTitular;

        public TitularController(ICadastroTitular cadastroTitular)
        {
            _cadastroTitular = cadastroTitular;
        }

        [HttpGet("GetTitulares")]
        [SwaggerOperation(Summary = "Obtem titulares", Description = "Obtem titulares")]
        public async Task<ActionResult> GetUsers()
        {

            var result = _cadastroTitular.GetTitulares();
            return Ok(result);
        }

        [HttpGet("GetTitular")]
        [SwaggerOperation(Summary = "Obtem titular", Description = "Obtem titular")]
        public async Task<ActionResult> GetTitular(int ID)
        {

            var result = _cadastroTitular.GetTitular(ID);
            return Ok(result);
        }

        [HttpPost("CreateTitular")]
        [SwaggerOperation(Summary = "Criar titular", Description = "Criar titular")]
        public async Task<ActionResult> CreateTitular(tTitularRetorno tTitularRetorno)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _cadastroTitular.CreateTitular(tTitularRetorno);
            return Ok();
        }

        [HttpPut("UpdateTitular")]
        [SwaggerOperation(Summary = "Atualizar titular", Description = "Atualizar titular")]
        public async Task<ActionResult> UpdateTitular(tTitularRetorno tTitularRetorno)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _cadastroTitular.UpdateTitular(tTitularRetorno);
            return Ok();
        }


    }
}
