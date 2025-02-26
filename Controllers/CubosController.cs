using Microsoft.AspNetCore.Mvc;
using MvcNetCoreTiendaCubos.Models;
using MvcNetCoreTiendaCubos.Repositories;

namespace MvcNetCoreTiendaCubos.Controllers
{
    public class CubosController : Controller
    {
        private RepositoryCubos repo;

        public CubosController(RepositoryCubos repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Cubo> cubos = await this.repo.GetCubosAsync();
            return View(cubos);
        }

        public async Task<IActionResult> Details(int id)
        {
            Cubo cubo = await this.repo.FindCuboAsync(id);
            return View(cubo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string nombre, string modelo, string marca, string imagen, int precio)
        {
            int idCubo = await this.repo.GetMaxIdUser();
            await this.repo.InsertCuboAsync(idCubo, nombre, modelo, marca, imagen, precio);
            return RedirectToAction("Details", new { id = idCubo });
        }


    }
}
