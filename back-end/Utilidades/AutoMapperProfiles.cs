﻿using AutoMapper;
using back_end.DTO;
using back_end.Entidades;
using Microsoft.AspNetCore.Identity;
using NetTopologySuite.Geometries;

namespace back_end.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreacionDTO, Genero>();
            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreacionDTO, Actor>()
                .ForMember(x => x.Foto, options => options.Ignore());

            CreateMap<CineCreacionDTO, Cine>()
                .ForMember(x => x.Ubicacion, x => x.MapFrom(
                    dto => geometryFactory.CreatePoint(new Coordinate(dto.Longitud, dto.Latitud))));

            CreateMap<Cine, CineDTO>()
            .ForMember(x => x.Latitud, dto => dto.MapFrom(campo => campo.Ubicacion.Y))
            .ForMember(x => x.Longitud, dto => dto.MapFrom(campo => campo.Ubicacion.X));

            CreateMap<PeliculaCreacionDTO, Pelicula>()
                .ForMember(x => x.Poster, opciones => opciones.Ignore())
                .ForMember(x => x.PeliculasGeneros, opciones => opciones.MapFrom(MapearPeliculasGeneros))
                .ForMember(x => x.PeliculasCines, opciones => opciones.MapFrom(MapearPeliculasCines))
                .ForMember(x => x.PeliculasActores, opciones => opciones.MapFrom(MapearPeliculasActores));

            CreateMap<Pelicula, PeliculaDTO>()
                .ForMember(x => x.Generos, opciones => opciones.MapFrom(MapearPeliculaGeneros))
                .ForMember(x => x.Actores, opciones => opciones.MapFrom(MapearPeliculaActores))
                .ForMember(x => x.Cines, opciones => opciones.MapFrom(MapearPeliculaCines));

            CreateMap<IdentityUser, UsuarioDTO>();

        }

        private List<CineDTO> MapearPeliculaCines(Pelicula pelicula, PeliculaDTO peliculaDTO)
        {
            var resultado = new List<CineDTO>();

            if (pelicula.PeliculasCines != null)
            {
                foreach (var cinePeliculas in pelicula.PeliculasCines)
                {
                    resultado.Add(new CineDTO() {
                    Id = cinePeliculas.CineId,
                    Nombre = cinePeliculas.Cine.Nombre,
                    Latitud = cinePeliculas.Cine.Ubicacion.Y,
                    Longitud = cinePeliculas.Cine.Ubicacion.X
                    });
                }
            }
            return resultado;
        }

        private List<PeliculaActorDTO> MapearPeliculaActores(Pelicula pelicula, PeliculaDTO peliculaDTO)
        {
            var resultado = new List<PeliculaActorDTO>();

            if (pelicula.PeliculasActores != null)
            {
                foreach (var actorPeliculas in pelicula.PeliculasActores)
                {
                    resultado.Add(new PeliculaActorDTO() { Id = actorPeliculas.ActorId, Nombre = actorPeliculas.Actor.Nombre, Foto = actorPeliculas.Actor.Foto, Orden = actorPeliculas.Orden, Personaje = actorPeliculas.Personaje });
                }
            }
            return resultado;
        }

        private List<GeneroDTO> MapearPeliculaGeneros(Pelicula pelicula, PeliculaDTO peliculaDTO)
        {
            var resultado = new List<GeneroDTO>();

            if (pelicula.PeliculasGeneros != null)
            {
                foreach(var genero in pelicula.PeliculasGeneros)
                {
                    resultado.Add(new GeneroDTO() { Id = genero.GeneroId, Nombre = genero.Genero.Nombre });
                }
            }
            return resultado;
        }

        private List<PeliculasGeneros> MapearPeliculasGeneros(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var resultado = new List<PeliculasGeneros>();

            if(peliculaCreacionDTO.GenerosIds == null)
            {
                return resultado;
            }

            foreach (var id in peliculaCreacionDTO.GenerosIds)
            {
                resultado.Add(new PeliculasGeneros() { GeneroId = id });
            }
            return resultado;
        }
        private List<PeliculasCines> MapearPeliculasCines(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var resultado = new List<PeliculasCines>();

            if (peliculaCreacionDTO.CinesIds == null)
            {
                return resultado;
            }

            foreach (var id in peliculaCreacionDTO.CinesIds)
            {
                resultado.Add(new PeliculasCines() { CineId = id });
            }
            return resultado;
        }       
        private List<PeliculasActores> MapearPeliculasActores(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var resultado = new List<PeliculasActores>();

            if (peliculaCreacionDTO.Actores == null)
            {
                return resultado;
            }

            foreach (var actor in peliculaCreacionDTO.Actores)
            {
                resultado.Add(new PeliculasActores() { ActorId = actor.Id, Personaje = actor.Personaje });
            }
            return resultado;
        }
    }
}
