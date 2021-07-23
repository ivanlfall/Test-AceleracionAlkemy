using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAceleracionAlkemy.Dto.In;
using TestAceleracionAlkemy.Dto.Out;
using TestAceleracionAlkemy.Models;

namespace TestAceleracionAlkemy.Mapper
{
    public class PeliculaProfile : Profile
    {
        public PeliculaProfile()
        {
            CreateMap<Pelicula, PeliculaOut>();
            CreateMap<PeliculaIn, Pelicula>();
        }
    }
}
