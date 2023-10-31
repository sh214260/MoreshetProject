using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CartProductController : Controller
    {
        // GET: CartProductController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CartProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CartProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartProductController/Create
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

        // GET: CartProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartProductController/Edit/5
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

        // GET: CartProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartProductController/Delete/5
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
