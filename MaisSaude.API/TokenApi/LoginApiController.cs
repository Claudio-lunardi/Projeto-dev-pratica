using MaisSaude.ApiToken.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MaisSaude.API.TokenApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginApiController : ControllerBase
    {

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<LoginRespostaModel>> Login([FromBody] LoginRequisicaoModel loginRequisicaoModel)
        {
            return Ok(await new LoginServico().Login(loginRequisicaoModel));
        }

    }
}
