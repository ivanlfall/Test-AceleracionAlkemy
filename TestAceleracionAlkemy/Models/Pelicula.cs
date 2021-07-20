﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAceleracionAlkemy.Models
{
    public class Pelicula
    {

        public int Id { get; set; }
        [Required]
        public string Imagen { get; set; }
        [Required]
        public string Titulo { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }
        [Range(1,5)]
        public int Calificacion { get; set; }

        public int GeneroID { get; set; }
        public virtual Genero Genero { get; set; }
        public ICollection<Personaje> Personajes { get; set; }
    }
}
