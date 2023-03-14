﻿using back_end.Entidades;
using Microsoft.EntityFrameworkCore;

namespace back_end
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

         //Genera la tabla de acuerdo a un modelo
        public DbSet<Genero> Generos { get; set; }

        //Genera la tabla de Actores de acuerdo a su modelo
        public DbSet<Actor> Actores { get; set; }

    }
}
