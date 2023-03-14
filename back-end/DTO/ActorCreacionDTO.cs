using System.ComponentModel.DataAnnotations;

namespace back_end.DTO
{
    public class ActorCreacionDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Nombre { get; set; }
        public string Biografia { get; set; }
        public DateTime FechaNacimiento { get; set; }

    }
}
