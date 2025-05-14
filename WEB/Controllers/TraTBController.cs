using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
    public class TraTBController : Controller
    {
        // GET: TraTBController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TraTBController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TraTBController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TraTBController/Create
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

        // GET: TraTBController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TraTBController/Edit/5
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

        // GET: TraTBController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TraTBController/Delete/5
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
