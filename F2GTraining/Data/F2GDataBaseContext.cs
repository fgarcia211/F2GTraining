using F2GTraining.Models;
using Microsoft.EntityFrameworkCore;

namespace F2GTraining.Data
{
    public class F2GDataBaseContext : DbContext
    {
        public F2GDataBaseContext(DbContextOptions<F2GDataBaseContext> options) :
            base(options)
        { }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Equipo> Equipos { get; set; }

        public DbSet<Jugador> Jugadores { get; set; }
    }
}
