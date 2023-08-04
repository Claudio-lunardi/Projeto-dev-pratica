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

namespace MaisSaude.Controllers.Area.Operador
{
    public class CadastroTitularController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<DadosBase> _DadosBase;
        private readonly IApiToken _IApiToken;

        public CadastroTitularController(IHttpClientFactory Client, IHttpContextAccessor cookies, IOptions<DadosBase> dadosBase, IApiToken iApiToken)
        {
            _httpClient = Client.CreateClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _DadosBase = dadosBase;
            _IApiToken = iApiToken;
        }

        public async Task<ActionResult> Index()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
            HttpResponseMessage response = await _httpClient.GetAsync($"{_DadosBase.Value.API_URL_BASE}Login/GetUser?UserID=");

            if (response.IsSuccessStatusCode)
                return View(JsonConvert.DeserializeObject<tUserRetorno>(await response.Content.ReadAsStringAsync()));
            else
                return View();


         
        }

        // GET: CadastroTitularController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CadastroTitularController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CadastroTitularController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: CadastroTitularController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CadastroTitularController/Edit/5
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

        // GET: CadastroTitularController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CadastroTitularController/Delete/5
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
