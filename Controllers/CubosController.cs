using Microsoft.AspNetCore.Mvc;
using MvcNetCoreTiendaCubos.Helpers;
using MvcNetCoreTiendaCubos.Models;
using MvcNetCoreTiendaCubos.Repositories;

namespace MvcNetCoreTiendaCubos.Controllers
{
    public class CubosController : Controller
    {
        private RepositoryCubos repo;
        private HelperPathProvider helperPath;

        public CubosController(RepositoryCubos repo, HelperPathProvider helperPath)
        {
            this.repo = repo;
            this.helperPath = helperPath;
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
        public async Task<IActionResult> Create(string nombre, string modelo, string marca, IFormFile imagen, int precio)
        {
            int idCubo = await this.repo.GetMaxIdUser();

            string fileName = imagen.FileName; //Para obtener el nombre del archivo

            string path = this.helperPath.MapPath(fileName, Folders.Images);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await imagen.CopyToAsync(stream);
            }

            await this.repo.InsertCuboAsync(idCubo, nombre, modelo, marca, fileName, precio);

            return RedirectToAction("Details", new { id = idCubo });
        }

        public async Task<IActionResult> Edit(int id)
        {
            Cubo cubo = await this.repo.FindCuboAsync(id);
            return View(cubo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, string nombre, string modelo, string marca, IFormFile imagen, int precio)
        {
            string fileName = imagen.FileName;
            string path = this.helperPath.MapPath(fileName, Folders.Images);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await imagen.CopyToAsync(stream);
            }

            await this.repo.UpdateCuboAsync(id, nombre, modelo, marca, fileName, precio);
            return RedirectToAction("Details", new { id = id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.repo.DeleteCuboAsync(id);
            return RedirectToAction("Index");
        }



    }
}
