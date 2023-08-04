using MaisSaude.Extensoes;
using MaisSaude.Models;
using MaisSaude.Models.tUser;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace MaisSaude.Controllers.Area.Login
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<DadosBase> _DadosBase;
        private readonly IApiToken _IApiToken;

        public LoginController(IHttpClientFactory Client, IHttpContextAccessor cookies, IOptions<DadosBase> dadosBase, IApiToken iApiToken)
        {
            _httpClient = Client.CreateClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _DadosBase = dadosBase;
            _IApiToken = iApiToken;
        }


        #region Tela Login
        public async Task<ActionResult> Login(string mensagem = null, bool sucesso = true)
        {
            try
            {
                if (sucesso)
                    TempData["sucesso"] = mensagem;
                else
                    TempData["erro"] = mensagem;


                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return View();
            }
            catch (Exception)
            {
                throw;
            }



        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginRequisicao loginRequisicao)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_DadosBase.Value.API_URL_BASE}Login/AuthenticarUsuario", loginRequisicao);
                if (response.IsSuccessStatusCode)
                {
                    var user = JsonConvert.DeserializeObject<tUser>(await response.Content.ReadAsStringAsync());

                    if (user == null)
                    {
                        ModelState.AddModelError("Senha", "Usuario ou senha incorretos"); TempData["erro"] = "erro"; return View();
                    }

                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, user.Nome),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.RoleID.ToString()),
                        new Claim("UserID", user.ID.ToString())
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return View();
            }
        }

        #endregion


        #region Primeiro acesso - criar conta
        public ActionResult PrimeiroAcesso(string mensagem = null, bool sucesso = true)
        {
            if (sucesso)
                TempData["sucesso"] = mensagem;
            else
                TempData["erro"] = mensagem;

            tUserRetorno tUserRetorno = new tUserRetorno()
            {
                tUser = new tUser()
                {
                    DataCriacao = DateTime.Now,
                    Ativo = true
                }
            };
            return View(tUserRetorno);
        }

        [HttpPost]
        public async Task<ActionResult> PrimeiroAcesso(tUserRetorno tUserRetorno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_DadosBase.Value.API_URL_BASE}Login/CreateUser", tUserRetorno);

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction("Login", "Login", new { mensagem = "Usuário criado com sucesso!", sucesso = true });
                    else
                    {
                        TempData["erro"] = "Erro ao criar usuário!";
                        return View();
                    }
                }
                else
                {
                    TempData["erro"] = "Algum campo falta ser preenchido!";
                    return View();
                }

            }
            catch (Exception x)
            {
                return View();
            }
        }


        #endregion


        #region Update Usuario
        public async Task<ActionResult> UpdateUser()
        {

            var UserID = User.FindFirst("UserID")?.Value;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
            HttpResponseMessage response = await _httpClient.GetAsync($"{_DadosBase.Value.API_URL_BASE}Login/GetUser?UserID={UserID}");

            if (response.IsSuccessStatusCode)
                return View(JsonConvert.DeserializeObject<tUserRetorno>(await response.Content.ReadAsStringAsync()));
            else
                return View();

        }
        [HttpPost]
        public async Task<ActionResult> UpdateUser(tUserRetorno tUserRetorno)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{_DadosBase.Value.API_URL_BASE}Login/UpdateUser", tUserRetorno);

            if (response.IsSuccessStatusCode)
            {
                //ViewData["sucesso"] = "Alterações salvas!";
                return RedirectToAction("Index", "Home");

            }
            else
            {
                //ViewData["erro"] = "Erro ao salvar alterações!";
                return View();

            }
        }
    }
    #endregion


}
