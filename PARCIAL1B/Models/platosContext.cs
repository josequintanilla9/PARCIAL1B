using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using PARCIAL1B.Models;


namespace PARCIAL1B.models
{
    public class platosContext : DbContext
    {
        public platosContext(DbContextOptions<platosContext> options) : base(options)
        {
        }

        public DbSet<Platos> platos { get; set; }
        public DbSet<Elementos> elementos { get; set; }
        public DbSet<ElementosPorPlato> elementosPorPlato{ get; set; }
        public DbSet<PlatosPorCombo> platosPorCombo { get; set; }

    }
}