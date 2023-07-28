using MaisSaude.Business.Login;
using MaisSaude.Models.tUser;
using Microsoft.AspNetCore.Mvc;

namespace MaisSaude.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginUser _loginUser;

        public LoginController(ILoginUser loginUser)
        {
            _loginUser = loginUser;
        }


        [HttpPost("AuthenticarUsuario")]
        public async Task<ActionResult> AuthenticarUsuario([FromBody] LoginRequisicao loginRequisicao)
        {
            var result = await _loginUser.AuthenticarUsuario(loginRequisicao);
            return Ok(result);
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] tUserRetorno tUserRetorno)
        {
            var isSuccess = await _loginUser.CreateUser(tUserRetorno);
            if (!isSuccess) return BadRequest();
            return Ok();

        }

        [HttpGet("GetUser")]
        public async Task<ActionResult> GetUser(int UserID)
        {
            var user = await _loginUser.GettUser(UserID);
            return Ok(user);
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult> UpdateUser([FromBody] tUserRetorno tUserRetorno)
        {
            var user = await _loginUser.UpdateUser(tUserRetorno);
            return Ok(user);
        }




    }
}
