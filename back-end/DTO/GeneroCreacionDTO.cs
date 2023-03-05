using back_end.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace back_end.DTO
{
    public class GeneroCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50)]
        [PrimeraLetraMayusculaAtrtribute]
        public string Nombre { get; set; }
    }
}
