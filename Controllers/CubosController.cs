using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MvcNetCoreTiendaCubos.Extensions;
using MvcNetCoreTiendaCubos.Helpers;
using MvcNetCoreTiendaCubos.Models;
using MvcNetCoreTiendaCubos.Repositories;

namespace MvcNetCoreTiendaCubos.Controllers
{
    public class CubosController : Controller
    {
        private RepositoryCubos repo;
        private HelperPathProvider helperPath;
        private IMemoryCache memoryCache;

        public CubosController(RepositoryCubos repo, HelperPathProvider helperPath, IMemoryCache memoryCache)
        {
            this.repo = repo;
            this.helperPath = helperPath;
            this.memoryCache = memoryCache;
        }

        public async Task<IActionResult> Index()
        {
            List<Cubo> cubos = await this.repo.GetCubosAsync();
            return View(cubos);
        }

        #region CRUD
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
        #endregion

        #region Favs

        public async Task<IActionResult> Favoritos()
        {
            List<Cubo> favs = this.memoryCache.Get<List<Cubo>>("Favoritos");
            if (favs == null)
            {
                ViewBag.Mensaje = "No hay cubos en favoritos";
                return View();
            }
            else if (favs.Count == 0)
            {
                ViewBag.Mensaje = "No hay cubos en favoritos";
                this.memoryCache.Remove("Favoritos");
                return View();
            }

            return View(favs);
        }

        public async Task<IActionResult> AddFav(int id)
        {
            List<Cubo> favs = this.memoryCache.Get<List<Cubo>>("Favoritos");
            if (favs == null)
            {
                favs = new List<Cubo>();
            }

            Cubo cubo = await this.repo.FindCuboAsync(id);
            favs.Add(cubo);
            this.memoryCache.Set("Favoritos", favs);

            return RedirectToAction("Index");
        }


        public IActionResult RemoveFav(int? id)
        {
            List<Cubo> favs = this.memoryCache.Get<List<Cubo>>("Favoritos");

            if (favs.Count == 1)
            {
                this.memoryCache.Remove("Favoritos");
                return RedirectToAction("Index");
            }

            Cubo cubo = favs.SingleOrDefault(x => x.IdCubo == id);
            favs.Remove(cubo);
            this.memoryCache.Set("Favoritos", favs);

            return RedirectToAction("Index");
        }





        #endregion

        #region Carrito

        public async Task<IActionResult> Carrito()
        {
            List<int> idsCarrito = HttpContext.Session.GetObject<List<int>>("Ids");
            if (idsCarrito == null)
            {
                ViewBag.Mensaje = "No hay cubos en la cesta";
                return View();
            }
            else if (idsCarrito.Count == 0)
            {
                ViewBag.Mensaje = "No hay cubos en la cesta";
                HttpContext.Session.Remove("Ids");
                return View();
            }

            List<Cubo> cubos = await this.repo.GetCubosSessionAsync(idsCarrito);
            return View(cubos);
        }

        public async Task<IActionResult> AddCart(int id)
        {
            List<int> idsCarrito = HttpContext.Session.GetObject<List<int>>("Ids");
            if (idsCarrito == null)
            {
                idsCarrito = new List<int>();
            }

            //si ya está no puede volver a comprarlo.
            if (idsCarrito.Contains(id))
            {
                return RedirectToAction("Index");
            }

            idsCarrito.Add(id);
            HttpContext.Session.SetObject("Ids", idsCarrito);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveCart(int id)
        {
            List<int> idsCarrito = HttpContext.Session.GetObject<List<int>>("Ids");
            idsCarrito.Remove(id);
            HttpContext.Session.SetObject("Ids", idsCarrito);
            return RedirectToAction("Carrito");
        }

        #endregion

        




    }
}
