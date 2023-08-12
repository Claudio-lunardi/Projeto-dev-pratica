using MaisSaude.Extensoes;
using MaisSaude.Models;
using MaisSaude.Models.tUser;
using MaisSaude.Models.tUser.tMedico;
using MaisSaude.Models.tUser.tTitular;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MaisSaude.Controllers.Area.Operador
{
    //[Authorize(Roles = "4")]//clinica
    public class MedicoController : Controller
    {


        private readonly HttpClient _httpClient;
        private readonly IOptions<DadosBase> _DadosBase;
        private readonly IApiToken _IApiToken;

        public MedicoController(IHttpClientFactory Client, IHttpContextAccessor cookies, IOptions<DadosBase> dadosBase, IApiToken iApiToken)
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
                HttpResponseMessage response = await _httpClient.GetAsync($"{_DadosBase.Value.API_URL_BASE}Medico/GetMedicos");

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


        public async Task<ActionResult> Create()
        {

            ViewBag.Clinicas = await CarregarClinica();
            tMedicoRetorno tMedicoRetorno = new tMedicoRetorno()
            {
                tUser = new tUser()
                {
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                }

            };
            return View(tMedicoRetorno);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(tMedicoRetorno tMedicoRetorno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_DadosBase.Value.API_URL_BASE}Medico/CreateMedico", tMedicoRetorno);

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index), new { mensagem = "Salvo com sucesso", sucesso = true });
                    else
                        return View(tMedicoRetorno);
                }
                else
                {
                    return View(tMedicoRetorno);
                }
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Edit(int id)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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









        private async Task<List<SelectListItem>> CarregarClinica()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());

            HttpResponseMessage response = await _httpClient.GetAsync($"{_DadosBase.Value.API_URL_BASE}Medico/GetClinicas");

            if (response.IsSuccessStatusCode)
            {
                string conteudo = await response.Content.ReadAsStringAsync();
                List<SelectClinica> clinica = JsonConvert.DeserializeObject<List<SelectClinica>>(conteudo);

                foreach (var linha in clinica)
                {
                    lista.Add(new SelectListItem()
                    {
                        Value = linha.ID.ToString(),
                        Text = linha.Nome,
                        Selected = false,
                    });
                }

                return lista;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }









    }
}
