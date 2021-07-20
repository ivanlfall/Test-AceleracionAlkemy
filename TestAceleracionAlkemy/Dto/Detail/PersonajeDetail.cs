using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAceleracionAlkemy.Dto
{
    public class PersonajeDetail
    {
        public int Id { get; set; }
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public decimal Peso { get; set; }
        public string Historia { get; set; }
        public string Pelicula { get; set; }
    }
}
