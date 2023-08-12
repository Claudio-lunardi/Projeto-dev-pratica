using MaisSaude.Business.Users;
using MaisSaude.Models.tUser;
using MaisSaude.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using MaisSaude.Extensoes;
using Microsoft.Extensions.Options;
using MaisSaude.Models.tUser.tTitular;
using Microsoft.AspNetCore.Authorization;

namespace MaisSaude.Controllers.Area.Operador
{
    //[Authorize(Roles = "4")]//clinica
    public class TitularController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<DadosBase> _DadosBase;
        private readonly IApiToken _IApiToken;

        public TitularController(IHttpClientFactory Client, IHttpContextAccessor cookies, IOptions<DadosBase> dadosBase, IApiToken iApiToken)
        {
            _httpClient = Client.CreateClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _DadosBase = dadosBase;
            _IApiToken = iApiToken;
        }

        public async Task<ActionResult> Index(string mensagem = null, bool sucesso = true)
        {

            try
            {
                if (sucesso)
                    TempData["sucesso"] = mensagem;
                else
                    TempData["erro"] = mensagem;

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage response = await _httpClient.GetAsync($"{_DadosBase.Value.API_URL_BASE}Titular/GetTitulares");

                if (response.IsSuccessStatusCode)
                    return View(JsonConvert.DeserializeObject<List<tUser>>(await response.Content.ReadAsStringAsync()));
                else
                    return View();
            }
            catch (Exception)
            {

                throw;
            }

        }

        #region CREATE
        public ActionResult Create()
        {
            tTitularRetorno tUserRetorno = new tTitularRetorno()
            {
                tUser = new tUser()
                {
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                }

            };

            return View(tUserRetorno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(tTitularRetorno tTitularRetorno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_DadosBase.Value.API_URL_BASE}Titular/CreateTitular", tTitularRetorno);

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index), new { mensagem = "Salvo com sucesso", sucesso = true });
                    else
                        return View(tTitularRetorno);
                }
                else
                {
                    return View(tTitularRetorno);
                }
            }
            catch
            {
                return View();
            }
        }



        #endregion


        public async Task<ActionResult> Edit(int ID)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage response = await _httpClient.GetAsync($"{_DadosBase.Value.API_URL_BASE}Titular/GetTitular?ID={ID}");

                if (response.IsSuccessStatusCode)
                    return View(JsonConvert.DeserializeObject<tTitularRetorno>(await response.Content.ReadAsStringAsync()));
                else
                    return View();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(tTitularRetorno tTitularRetorno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{_DadosBase.Value.API_URL_BASE}Titular/UpdateTitular", tTitularRetorno);

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index), new { mensagem = "Editado com sucesso", sucesso = true });
                    else
                        return View(tTitularRetorno);
                }
                else
                {
                    return View(tTitularRetorno);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: TitularController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TitularController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
