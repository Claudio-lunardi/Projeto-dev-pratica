using MaisSaude.Extensoes;
using MaisSaude.Models;
using MaisSaude.Models.tUser;
using MaisSaude.Models.tUser.tDapendente;
using MaisSaude.Models.tUser.tTitular;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MaisSaude.Controllers.Area.Operador
{
    // [Authorize(Roles = "4")]//clinica
    public class DependenteController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<DadosBase> _DadosBase;
        private readonly IApiToken _IApiToken;

        public DependenteController(IHttpClientFactory Client, IHttpContextAccessor cookies, IOptions<DadosBase> dadosBase, IApiToken iApiToken)
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
                HttpResponseMessage response = await _httpClient.GetAsync($"{_DadosBase.Value.API_URL_BASE}Dependente/GetDependentes");

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
            ViewBag.SelectTitular = await CarregarTitular();

            tDependenteRetorno tDependenteRetorno = new tDependenteRetorno()
            {
                tUser = new tUser()
                {
                    DataCriacao = DateTime.Now,
                    Ativo = true
                }
            };
            return View(tDependenteRetorno);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(tDependenteRetorno tDependenteRetorno)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_DadosBase.Value.API_URL_BASE}Dependente/CreateDependente", tDependenteRetorno);

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index), new { mensagem = "Salvo com sucesso", sucesso = true });
                    else
                    {
                        ViewBag.SelectTitular = await CarregarTitular();
                        return View(tDependenteRetorno);
                    }

                }
                else
                {
                    ViewBag.SelectTitular = await CarregarTitular();
                    return View(tDependenteRetorno);
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
                HttpResponseMessage response = await _httpClient.GetAsync($"{_DadosBase.Value.API_URL_BASE}Dependente/GetDependente?ID={ID}");

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.SelectTitular = await CarregarTitular();
                    return View(JsonConvert.DeserializeObject<tDependenteRetorno>(await response.Content.ReadAsStringAsync()));
                }
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
        public async Task<ActionResult> Edit(tDependenteRetorno tDependenteRetorno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{_DadosBase.Value.API_URL_BASE}Dependente/UpdateDependente", tDependenteRetorno);

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index), new { mensagem = "Editado com sucesso", sucesso = true });
                    else
                        return View(tDependenteRetorno);
                }
                else
                {
                    return View(tDependenteRetorno);
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







        private async Task<List<SelectListItem>> CarregarTitular()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());

            HttpResponseMessage response = await _httpClient.GetAsync($"{_DadosBase.Value.API_URL_BASE}Dependente/GetListTitular");

            if (response.IsSuccessStatusCode)
            {
                string conteudo = await response.Content.ReadAsStringAsync();
                List<SelectTitular> titular = JsonConvert.DeserializeObject<List<SelectTitular>>(conteudo);

                foreach (var linha in titular)
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
