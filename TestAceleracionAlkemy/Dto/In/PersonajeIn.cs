using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAceleracionAlkemy.Dto.In
{
    public class PersonajeIn
    {
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public decimal Peso { get; set; }
        public string Historia { get; set; }
        public int PeliculaID { get; set; }
    }
}
