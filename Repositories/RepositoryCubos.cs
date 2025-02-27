using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using MvcNetCoreTiendaCubos.Data;
using MvcNetCoreTiendaCubos.Models;

namespace MvcNetCoreTiendaCubos.Repositories
{
    public class RepositoryCubos
    {
        #region Vistas y procedures
        //        CREATE VIEW V_COMPRADETALLE
        //as 
        //	select cubos.nombre as nombrecubo, cubos.precio as preciocubo, compra.cantidad,
        //    compra.precio as preciocompra, compra.fechapedido
        //    from cubos inner join compra
        //    on cubos.id_cubo = compra.id_cubo;
        #endregion

        private CubosContext context;
        public RepositoryCubos(CubosContext context)
        {
            this.context = context;
        }

        #region CRUD

        public async Task<List<Cubo>> GetCubosAsync()
        {
            var consulta = from datos in this.context.cubos
                           select datos;
            return await consulta.ToListAsync();
        }

        public async Task<Cubo> FindCuboAsync(int id)
        {
            var consulta = from datos in this.context.cubos
                           where datos.IdCubo == id
                           select datos;
            return await consulta.FirstOrDefaultAsync();
        }

        public async Task<int> GetMaxIdUser()
        {
            if (this.context.cubos.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.cubos.MaxAsync(x => x.IdCubo) + 1;
            }
        }

        public async Task InsertCuboAsync(int idCubo, string nombre, string modelo, string marca, string imagen, int precio)
        {
            Cubo cubo = new Cubo();
            cubo.IdCubo = idCubo;
            cubo.Nombre = nombre;
            cubo.Modelo = modelo;
            cubo.Marca = marca;
            cubo.Imagen = imagen;
            cubo.Precio = precio;
            this.context.cubos.Add(cubo);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateCuboAsync(int idCubo, string nombre, string modelo, string marca, string imagen, int precio)
        {
            Cubo cubo = await this.FindCuboAsync(idCubo);
            cubo.Nombre = nombre;
            cubo.Modelo = modelo;
            cubo.Marca = marca;
            cubo.Imagen = imagen;
            cubo.Precio = precio;
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteCuboAsync(int idCubo)
        {
            Cubo cubo = await this.FindCuboAsync(idCubo);
            this.context.cubos.Remove(cubo);
            await this.context.SaveChangesAsync();
        }

        public async Task<List<Cubo>> GetCubosSessionAsync(List<int> ids)
        {
            var consulta = from datos in this.context.cubos
                           where ids.Contains(datos.IdCubo)
                           select datos;

            if (consulta == null)
            {
                return null;
            }

            return await consulta.ToListAsync();
        }

        #endregion


        #region Comprar

        public async Task InsertCompraAsync(int idCubo, int cantidad, int precio)
        {
            Compra compra = new Compra();
            compra.IdCubo = idCubo;
            compra.Cantidad = cantidad;
            compra.Precio = precio;
            compra.FechaPedido = DateTime.Now;
            this.context.compras.Add(compra);
            await this.context.SaveChangesAsync();
        }

        #endregion



    }
}
