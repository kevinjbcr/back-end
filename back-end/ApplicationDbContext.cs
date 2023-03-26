using back_end.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace back_end
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeliculasActores>()
                .HasKey(x => new { x.ActorId, x.PeliculaId });

            modelBuilder.Entity<PeliculasGeneros>()
                .HasKey(x => new { x.GeneroId, x.PeliculaId });

            modelBuilder.Entity<PeliculasCines>()
                .HasKey(x => new { x.CineId, x.PeliculaId });

            base.OnModelCreating(modelBuilder);
        }

        //Genera la tabla de acuerdo a un modelo
        public DbSet<Genero> Generos { get; set; }

        //Genera la tabla de Actores de acuerdo a su modelo
        public DbSet<Actor> Actores { get; set; }

        //Genera la tabla de Cines de acuerdo a su modelo
        public DbSet<Cine> Cines { get; set; }
        //Genera la tabla de Cines de acuerdo a su modelo
        public DbSet<Pelicula> Peliculas { get; set; }
        //Genera la tabla de Cines de acuerdo a su modelo
        public DbSet<PeliculasActores> PeliculasActores { get; set; }
        //Genera la tabla de Cines de acuerdo a su modelo
        public DbSet<PeliculasGeneros> PeliculasGeneros { get; set; }
        //Genera la tabla de Cines de acuerdo a su modelo
        public DbSet<PeliculasCines> PeliculasCines { get; set; }
        //Genera la tabla de Ratings de acuerdo a su modelo
        public DbSet<Rating> Ratings { get; set; }

    }
}
