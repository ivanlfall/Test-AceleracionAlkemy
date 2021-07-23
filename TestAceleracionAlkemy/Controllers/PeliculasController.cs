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
using TestAceleracionAlkemy.Dto.Detail;
using TestAceleracionAlkemy.Dto.In;
using TestAceleracionAlkemy.Dto.Out;
using TestAceleracionAlkemy.Mapper;
using TestAceleracionAlkemy.Models;

namespace TestAceleracionAlkemy.Controllers
{
    [Route("movies")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly PeliculasContext _context;
        private readonly IMapper _mapper;

        public PeliculasController(PeliculasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeliculaOut>>> GetPelicula()
        {
            List<PeliculaOut> peliculas = new List<PeliculaOut>();

            var data = await _context.Pelicula.ToListAsync();
            data.ForEach(item => peliculas.Add(_mapper.Map<PeliculaOut>(item)));

            return peliculas;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PeliculaDetail>> GetPelicula(int id)
        {
            var data = await _context.Pelicula.Include(x => x.Personajes).Include(x => x.Genero).ToListAsync();

            var pelicula = data.ToList().Find(x => x.Id == id);

            if (pelicula == null)
            {
                return NotFound();
            }

            var personajes = pelicula.Personajes.ToList();
            List<PersonajeOut> personajeOuts = new List<PersonajeOut>();
            personajes.ForEach(item => personajeOuts.Add(_mapper.Map<PersonajeOut>(item)));

            PeliculaDetail peliculaDetalle = new PeliculaDetail()
            {
                Id = pelicula.Id,
                Imagen = pelicula.Imagen,
                Titulo = pelicula.Titulo,
                FechaCreacion = pelicula.FechaCreacion,
                Calificacion = pelicula.Calificacion,
                Genero = pelicula.Genero.Nombre,
                Personajes = personajeOuts
            };


            return peliculaDetalle;
        }

        [HttpGet("Buscar")]
        public async Task<ActionResult> GetBy([FromQuery] PeliculaBusqueda filtros)
        {
            var data = await _context.Pelicula.ToListAsync();

            if (filtros == null)
            {
                return Ok(data);
            }


            if (!string.IsNullOrEmpty(filtros.Nombre))
            {
                data = data.Where(x => x.Titulo.Contains(filtros.Nombre)).ToList();
            }
            if (filtros.GeneroId > 0)
            {
                data = data.Where(x => x.GeneroID == filtros.GeneroId).ToList();
            }
            if (!String.IsNullOrEmpty(filtros.Orden))
            {

                data = filtros.Orden.Equals("ASC") ? 
                    data.OrderBy(x => x.Titulo).ToList() : filtros.Orden.Equals("DESC") ? 
                    data.OrderByDescending(x => x.Titulo).ToList() : data;
             
            }

            return Ok(data);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutPelicula(int id, Pelicula pelicula)
        {
            if (id != pelicula.Id)
            {
                return BadRequest();
            }

            _context.Entry(pelicula).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeliculaExists(id))
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
        public async Task<ActionResult<Pelicula>> PostPelicula(PeliculaIn peliculaIn)
        {
            Pelicula pelicula = _mapper.Map<Pelicula>(peliculaIn);

            _context.Pelicula.Add(pelicula);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPelicula", new { id = pelicula.Id }, pelicula);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePelicula(int id)
        {
            var pelicula = await _context.Pelicula.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }

            _context.Pelicula.Remove(pelicula);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PeliculaExists(int id)
        {
            return _context.Pelicula.Any(e => e.Id == id);
        }
    }
}
