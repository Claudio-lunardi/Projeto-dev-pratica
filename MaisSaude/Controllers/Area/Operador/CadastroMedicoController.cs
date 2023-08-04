using MaisSaude.Extensoes;
using MaisSaude.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace MaisSaude.Controllers.Area.Operador
{
    public class CadastroMedicoController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly IOptions<DadosBase> _DadosBase;
        private readonly IApiToken _IApiToken;

        public CadastroMedicoController(IHttpClientFactory Client, IHttpContextAccessor cookies, IOptions<DadosBase> dadosBase, IApiToken iApiToken)
        {
            _httpClient = Client.CreateClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _DadosBase = dadosBase;
            _IApiToken = iApiToken;
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: CadastroMedicoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CadastroMedicoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CadastroMedicoController/Create
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

        // GET: CadastroMedicoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CadastroMedicoController/Edit/5
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

        // GET: CadastroMedicoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CadastroMedicoController/Delete/5
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
