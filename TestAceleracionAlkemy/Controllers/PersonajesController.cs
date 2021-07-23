using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAceleracionAlkemy.Data;
using TestAceleracionAlkemy.Dto;
using TestAceleracionAlkemy.Dto.In;
using TestAceleracionAlkemy.Models;

namespace TestAceleracionAlkemy.Controllers
{
    [Route("characters")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly PeliculasContext _context;

        public PersonajesController(PeliculasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PersonajeOut>> GetPersonaje()
        {
            var data = await _context.Personaje.ToListAsync();
            List<PersonajeOut> personajes = new List<PersonajeOut>();
            data.ForEach(item =>
            personajes.Add(_mapper.Map<PersonajeOut>(item)));

            return Ok(personajes);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PersonajeDetail>> GetPersonaje(int id)
        {
            var personajes = await _context.Personaje.Include(x => x.Pelicula).ToListAsync();

            var personaje = personajes.ToList().Find(x => x.Id == id);

            if (personaje == null)
            {
                return NotFound();
            }


            var pelicula = personaje.Pelicula.Titulo;

            PersonajeDetail personajeDetail = new PersonajeDetail()
            {
                Id = personaje.Id,
                Imagen = personaje.Imagen,
                Nombre = personaje.Nombre,
                Edad = personaje.Edad,
                Peso = personaje.Peso,
                Historia = personaje.Historia,
                Pelicula = pelicula
            };


            return personajeDetail;
        }

        
       [HttpGet("Buscar")]
       public async Task<ActionResult> GetBy([FromQuery]PersonajeBusqueda filtros)
        {
            var data = await _context.Personaje.ToListAsync();

            if (filtros == null)
            {
                return Ok(data);
            }


            if (!string.IsNullOrEmpty(filtros.Nombre))
            {
                data = data.Where(x => x.Nombre.Contains(filtros.Nombre)).ToList();
            }
            if (filtros.Edad > 0 )
            {
                data = data.Where(x => x.Edad == filtros.Edad).ToList();
            }
            if (filtros.PeliculaId > 0)
            {
                data = data.Where(x => x.PeliculaID == filtros.PeliculaId).ToList();
            }

            return Ok(data);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonaje(int id, Personaje personaje)
        {
            if (id != personaje.Id)
            {
                return BadRequest();
            }

            _context.Entry(personaje).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonajeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Personaje>> PostPersonaje(PersonajeIn personajeIn)
        {
            Personaje personaje = _mapper.Map<Personaje>(personajeIn);

            _context.Personaje.Add(personaje);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersonaje), new { id = personaje.Id }, personaje);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonaje(int id)
        {
            var personaje = await _context.Personaje.FindAsync(id);
            if (personaje == null)
            {
                return NotFound();
            }

            _context.Personaje.Remove(personaje);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonajeExists(int id)
        {
            return _context.Personaje.Any(e => e.Id == id);
        }
    }
}
