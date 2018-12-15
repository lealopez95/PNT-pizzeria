using System.Data.Entity;

namespace pizeria.Models
{
    public class ContextoDBContext : DbContext
    {
        public DbSet<Pedido> pedidos { get; set; }
        public DbSet<Producto> productos { get; set; }
        public DbSet<Sucursal> sucursales { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
    }
}