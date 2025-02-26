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
    }
}
