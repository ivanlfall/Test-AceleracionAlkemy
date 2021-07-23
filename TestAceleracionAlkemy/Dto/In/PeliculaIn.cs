using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAceleracionAlkemy.Dto.In
{
    public class PeliculaIn
    {
        [Required(ErrorMessage = "La Imagen es requerida")]
        public string Imagen { get; set; }
        [Required(ErrorMessage = "El Titulo es requerido")]
        public string Titulo { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }
        [Range(1, 5, ErrorMessage = "Valor Fuera de rango")]
        public int Calificacion { get; set; }
        public int GeneroId { get; set; }
    }
}
