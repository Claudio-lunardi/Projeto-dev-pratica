using Microsoft.AspNetCore.Mvc;

namespace MaisSaude.Controllers.Area.Cliente
{
    public class AgendamentoConsultaController : Controller
    {
        // GET: AgendamentoConsultaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AgendamentoConsultaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AgendamentoConsultaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgendamentoConsultaController/Create
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

        // GET: AgendamentoConsultaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AgendamentoConsultaController/Edit/5
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

        // GET: AgendamentoConsultaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AgendamentoConsultaController/Delete/5
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
