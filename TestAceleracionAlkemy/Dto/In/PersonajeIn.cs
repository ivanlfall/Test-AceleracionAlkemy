using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAceleracionAlkemy.Dto.In
{
    public class PersonajeIn
    {
        [Required(ErrorMessage = "La Imagen es Requerida")]
        public string Imagen { get; set; }
        [Required(ErrorMessage = "El Nombre es requerido")]
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public decimal Peso { get; set; }
        public string Historia { get; set; }
        public int PeliculaID { get; set; }
    }
}
