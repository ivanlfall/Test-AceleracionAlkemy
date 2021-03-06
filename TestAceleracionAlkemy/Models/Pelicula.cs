using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAceleracionAlkemy.Models
{
    public class Pelicula
    {

        public int Id { get; set; }
        public string Imagen { get; set; }
        public string Titulo { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }
        public int Calificacion { get; set; }

        public int GeneroID { get; set; }

        public virtual Genero Genero { get; set; }
        public ICollection<Personaje> Personajes { get; set; }
    }
}
