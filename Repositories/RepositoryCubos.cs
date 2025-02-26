using Microsoft.EntityFrameworkCore;
using MvcNetCoreTiendaCubos.Data;
using MvcNetCoreTiendaCubos.Models;

namespace MvcNetCoreTiendaCubos.Repositories
{
    public class RepositoryCubos
    {
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
    }
}
