using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaisSaude.Controllers.Area.Operador
{
    public class CadastroUserController : Controller
    {
        // GET: CadastroUserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CadastroUserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CadastroUserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CadastroUserController/Create
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

        // GET: CadastroUserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CadastroUserController/Edit/5
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

        // GET: CadastroUserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CadastroUserController/Delete/5
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
