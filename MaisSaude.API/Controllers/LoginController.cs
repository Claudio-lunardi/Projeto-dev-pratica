using MaisSaude.Business.Login;
using MaisSaude.Business.Rabbit;
using MaisSaude.Common.Connections;
using MaisSaude.Models.tUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MaisSaude.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private readonly ILoginUser _loginUser;
        private readonly IMensageria _rabbitMQ;
        public LoginController(ILoginUser loginUser, IMensageria rabbitMQ)
        {
            _loginUser = loginUser;
            _rabbitMQ = rabbitMQ;
        }

        [HttpPost("AuthenticarUsuario")]
        [SwaggerOperation(Summary = "Authenticar usuario/login", Description = "Authenticar usuario/login")]
        public async Task<ActionResult> AuthenticarUsuario([FromBody] LoginRequisicao loginRequisicao)
        {
            var result = await _loginUser.AuthenticarUsuario(loginRequisicao);
            return Ok(result);
        }

        [HttpPost("CreateUser")]
        [SwaggerOperation(Summary = "Criar um novo usuario", Description = "Criar um novo usuario")]
        public async Task<ActionResult> CreateUser([FromBody] tUserRetorno tUserRetorno)
        {
            var isSuccess = await _loginUser.CreateUser(tUserRetorno);
            if (!isSuccess) return BadRequest();


            ModelRabbit modelRabbit = new ModelRabbit()
            {
                Nome = tUserRetorno.tUser.Nome,
                Email = tUserRetorno.tUser.Email
            };
            _rabbitMQ.EnviarMensagemRabbit(modelRabbit, "", "Email");
            return Ok();

        }

        [HttpGet("GetUser")]
        [SwaggerOperation(Summary = "Obtem um usuario por id", Description = "Obtem um usuario por id")]
        public async Task<ActionResult> GetUser(int UserID)
        {
            var user = await _loginUser.GettUser(UserID);
            return Ok(user);
        }

        [HttpPut("UpdateUser")]
        [SwaggerOperation(Summary = "Atualiza um usuario", Description = "Atualiza um usuario")]
        public async Task<ActionResult> UpdateUser([FromBody] tUserRetorno tUserRetorno)
        {
            var user = await _loginUser.UpdateUser(tUserRetorno);
            return Ok(user);
        }




    }
}
