using Microsoft.EntityFrameworkCore;
using MantenimientoTrabajadores.Models;

namespace MantenimientoTrabajadores.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        //Tabla Trabajadores
        public DbSet<Trabajador> Trabajadores { get; set; }
    }
}