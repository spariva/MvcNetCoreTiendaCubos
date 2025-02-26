using Microsoft.EntityFrameworkCore;
using MvcNetCoreTiendaCubos.Models;


namespace MvcNetCoreTiendaCubos.Data
{
    public class CubosContext: DbContext
    {
        public CubosContext(DbContextOptions<CubosContext> options) : base(options)
        {
        }

        public DbSet<Cubo> cubos { get; set; }
        public DbSet<Compra> compras { get; set; }
    }
}
