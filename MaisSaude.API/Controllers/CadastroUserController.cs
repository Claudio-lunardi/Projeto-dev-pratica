using MaisSaude.Business.CadastroUser;
using MaisSaude.Models.tUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaisSaude.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CadastroUserController : ControllerBase
    {

        private readonly ICadastroUser _cadastroUser;

        public CadastroUserController(ICadastroUser cadastroUser)
        {
            _cadastroUser = cadastroUser;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult> GetUsers()
        {
            var result = await _cadastroUser.GetUsers();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        [HttpPut("InsertTitular")]
        public async Task<ActionResult> InsertTitular(tUserRetorno tUserRetorno)
        {

            await _cadastroUser.InsertTitular(tUserRetorno);
            return Ok(); 
        }





    }
}
