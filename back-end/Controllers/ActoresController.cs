using AutoMapper;
using back_end.DTO;
using back_end.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/actores")]
    [ApiController]
    public class ActoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ActoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ActorCreacionDTO actorCreacionDTO)
        {
            var actor = mapper.Map<Actor>(actorCreacionDTO);
            context.Add(actor);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
