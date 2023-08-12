using MaisSaude.Extensoes;
using MaisSaude.Models;
using MaisSaude.Models.tUser;
using MaisSaude.Models.tUser.tClinica;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MaisSaude.Controllers.Area.Operador
{
    //[Authorize(Roles = "4")]//clinica
    public class ClinicaController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<DadosBase> _DadosBase;
        private readonly IApiToken _IApiToken;

        public ClinicaController(IHttpClientFactory Client, IHttpContextAccessor cookies, IOptions<DadosBase> dadosBase, IApiToken iApiToken)
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
                HttpResponseMessage response = await _httpClient.GetAsync($"{_DadosBase.Value.API_URL_BASE}Clinica/GetClinicas");

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

        public ActionResult Create()
        {
            tClinicaRetorno tClinicaRetorno = new tClinicaRetorno
            {
                tUser = new tUser
                {
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                }
            };

            return View(tClinicaRetorno);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(tClinicaRetorno tClinicaRetorno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_DadosBase.Value.API_URL_BASE}Clinica/CreateClinica", tClinicaRetorno);

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index), new { mensagem = "Salvo com sucesso", sucesso = true });
                    else
                        return View(tClinicaRetorno);
                }
                else
                {
                    return View(tClinicaRetorno);
                }
            }
            catch
            {
                return View();
            }
        }


        public async Task<ActionResult> Edit(int ID)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage response = await _httpClient.GetAsync($"{_DadosBase.Value.API_URL_BASE}Titular/GetTitular?ID={ID}");

                if (response.IsSuccessStatusCode)
                    return View(JsonConvert.DeserializeObject<tClinicaRetorno>(await response.Content.ReadAsStringAsync()));
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
        public async Task<ActionResult> Edit(tClinicaRetorno tClinicaRetorno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{_DadosBase.Value.API_URL_BASE}Clinica/UpdateClinica", tClinicaRetorno);

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index), new { mensagem = "Editado com sucesso", sucesso = true });
                    else
                        return View(tClinicaRetorno);
                }
                else
                {
                    return View(tClinicaRetorno);
                }
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            return View();
        }


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
