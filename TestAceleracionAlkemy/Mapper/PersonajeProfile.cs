using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAceleracionAlkemy.Dto;
using TestAceleracionAlkemy.Dto.In;
using TestAceleracionAlkemy.Models;

namespace TestAceleracionAlkemy.Mapper
{
    public class PersonajeProfile : Profile
    {
        public PersonajeProfile()
        {
            CreateMap<Personaje, PersonajeOut>();
            CreateMap<PersonajeIn, Personaje>();
            //CreateMap<Personaje, PersonajeDetail>();
        }
    }
}
