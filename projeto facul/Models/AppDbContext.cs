using Microsoft.EntityFrameworkCore;

namespace projeto_facul.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Veiculo> Veiculos { get; set; }

        public DbSet<Gasto> Gastos { get; set; }
    }
}