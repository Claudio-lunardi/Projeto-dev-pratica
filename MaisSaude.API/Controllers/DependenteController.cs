using MaisSaude.Business.CadastroDependente;
using MaisSaude.Models.tUser.tDapendente;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MaisSaude.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DependenteController : ControllerBase
    {
        private readonly ICadastroDependente _CadastroDependente;

        public DependenteController(ICadastroDependente iCadastroDependente)
        {
            _CadastroDependente = iCadastroDependente;
        }

        [HttpGet("GetDependentes")]
        [SwaggerOperation(Summary = "Obtem todos os dependentes.", Description = "Obtem todos os dependentes.")]
        public async Task<ActionResult> GetDependentes()
        {
            var result = _CadastroDependente.GetDependentes();
            return Ok(result);
        }

        [HttpGet("GetDependente")]
        [SwaggerOperation(Summary = "Obtem um dependente.", Description = "Obtem um dependente.")]
        public async Task<ActionResult> GetDependente(int ID)
        {
            var result = _CadastroDependente.GetDependente(ID);
            return Ok(result);
        }

        [HttpPut("UpdateDependente")]
        [SwaggerOperation(Summary = "Atualiza um dependente.", Description = "Atualiza um dependente.")]
        public async Task<ActionResult> UpdateDependente(tDependenteRetorno tDependenteRetorno)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _CadastroDependente.UpdateDependente(tDependenteRetorno);
            return Ok();
        }

        [HttpGet("GetListTitular")]
        [SwaggerOperation(Summary = "Lista titulares.", Description = "Lista titulares.")]
        public async Task<ActionResult> GetListTitular()
        {

            var result = _CadastroDependente.GetListTitular();
            return Ok(result);
        }

        [HttpPost("CreateDependente")]
        [SwaggerOperation(Summary = "Criar dependente.", Description = "Criar dependente.")]
        public async Task<ActionResult> CreateDependente([FromBody] tDependenteRetorno tDependenteRetorno)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _CadastroDependente.PostDependente(tDependenteRetorno);
            return Ok();
        }
    }
}
