using MaisSaude.Business.Validações;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaisSaude.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Validacao : ControllerBase
    {
        private readonly IValidacao _validacao;

        public Validacao(IValidacao validacao)
        {
            _validacao = validacao;
        }

        [HttpGet("ExisteEmail")]
        public async Task<ActionResult> ExisteEmail()
        {
            return Ok();
        }
    }
}
