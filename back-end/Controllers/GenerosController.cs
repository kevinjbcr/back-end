﻿using AutoMapper;
using back_end.DTO;
using back_end.Entidades;
using back_end.Utilidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controllers
{
    [Route("api/generos")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class GenerosController : ControllerBase
    {
        private readonly ILogger<GenerosController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GenerosController(ILogger<GenerosController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Genero>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = context.Generos.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var generos = await queryable.OrderBy(x => x.Id).Paginar(paginacionDTO).ToListAsync();
            return mapper.Map<List<Genero>>(generos);
        }

        [HttpGet("todos")]
        [AllowAnonymous]
        public async Task<ActionResult<List<GeneroDTO>>> Todos() 
        {
            var generos = await context.Generos.ToListAsync();
            return mapper.Map<List<GeneroDTO>>(generos);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<GeneroDTO>> Get(int Id)
        {
            var genero = await context.Generos.FirstOrDefaultAsync(x => x.Id == Id);

            if (genero == null)
            {
                return NotFound();
            }

            return mapper.Map<GeneroDTO>(genero);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            var genero = mapper.Map<Genero>(generoCreacionDTO);
            context.Add(genero);
            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult> Put(int Id, [FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            var genero = await context.Generos.FirstOrDefaultAsync(x => x.Id == Id);

            if (genero == null)
            {
                return NotFound();
            }

            genero = mapper.Map(generoCreacionDTO, genero);
            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Generos.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Genero() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();

        }
    }
}
