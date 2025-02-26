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
                return await this.context.cubos.MaxAsync
                    (x => x.IdCubo) + 1;
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


    }
}
