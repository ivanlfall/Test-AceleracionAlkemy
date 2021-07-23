using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAceleracionAlkemy.Dto.Detail
{
    public class PeliculaDetail
    {
        public int Id { get; set; }
        public string Imagen { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int Calificacion { get; set; }
        public string Genero { get; set; }
        public IEnumerable<PersonajeOut> Personajes { get; set; }
    }
}
