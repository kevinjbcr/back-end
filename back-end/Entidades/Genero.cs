﻿using back_end.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace back_end.Entidades
{
    public class Genero
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength:50)]
        [PrimeraLetraMayusculaAtrtribute]
        public string Nombre { get; set; }
    }
}
