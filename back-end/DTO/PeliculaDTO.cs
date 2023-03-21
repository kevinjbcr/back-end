﻿using System.ComponentModel.DataAnnotations;

namespace back_end.DTO
{
    public class PeliculaDTO
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 300)]
        public string Titulo { get; set; }
        public string Resumen { get; set; }
        public string Trailer { get; set; }
        public bool EnCines { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public string Poster { get; set; }
        
        public List<GeneroDTO> Generos { get; set; }
        public List<PeliculaActorDTO> Actores { get; set; }
        public List<CineDTO> Cines { get; set; }
    }
}
