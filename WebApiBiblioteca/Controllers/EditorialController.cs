using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiBiblioteca.DTOs;
using WebApiBiblioteca.Entidades;

namespace WebApiBiblioteca.Controllers
{
    // 4.2
    [ApiController]
    [Route("api/editoriales")]
    public class EditorialController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        // 4.2
        public EditorialController(ApplicationDbContext context)
        {
            this.context = context;
        }

        //4.3.a
        [HttpGet]
        public async Task<IEnumerable<Editorial>> GetEditoriales()
        {
            return await context.Editoriales.ToListAsync();
        }

        //4.3.b
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Editorial>> EditorialPorId(int id)
        {
            var editorial = await context.Editoriales.FindAsync(id);
            if (editorial == null)
            {
                return NotFound();
            }
            return Ok(editorial);
        }

        //4.3.c
        [HttpGet("parametrocontienequery")]
        public async Task<ActionResult<IEnumerable<Editorial>>> GetEditorialesContieneParametroRuta([FromQuery] string contiene)
        {
            var editoriales = await context.Editoriales.Where(x => x.Nombre.Contains(contiene)).OrderBy(x => x.Nombre).ToListAsync();
            return Ok(editoriales);
        }

        //4.3.d
        [HttpGet("editorialeslibros/{id:int}")]
        public async Task<ActionResult<Editorial>> GetEditorialesLibros(int id)
        {
            var editorial = await context.Editoriales.Include(x => x.Libros).FirstOrDefaultAsync(x => x.EditorialId == id);
            if (editorial == null)
            {
                return NotFound();
            }
            return Ok(editorial);
        }

        //4.4.a
        [HttpGet("editorialeslibrosdto/{id:int}")]
        public async Task<ActionResult<Editorial>> GetEditorialesLibrosDTO(int id)
        {
            var editorial = await (from x in context.Editoriales
                                   select new DTOEditorialLibro
                                   {
                                       IdEditorial = x.EditorialId,
                                       Nombre = x.Nombre,
                                       Libros = x.Libros.Select(y => new DTOLibroItem
                                       {
                                           IdLibro = y.LibroId,
                                           Nombre = y.Nombre,
                                           Paginas = y.Paginas
                                       }).ToList(),
                                   }).FirstOrDefaultAsync(x => x.IdEditorial == id);

            if (editorial == null)
            {
                return NotFound();
            }
            return Ok(editorial);
        }

        //4.10
        [HttpGet("keyless")]
        public async Task<ActionResult<IEnumerable<ConsultaKeyLess>>> GetEditorialesLibrosKeyLess()
        {
            var datos = await context.ConsultaKeyLess.ToListAsync();

            return Ok(datos);
        }


        //4.5
        [HttpPost]
        public async Task<ActionResult> Post(Editorial editorial)
        {
            await context.AddAsync(editorial);
            await context.SaveChangesAsync();
            return Ok();
        }

        //4.6
        [HttpPost("editoriallibros")]
        public async Task<ActionResult> PostDTOFamiliaProductos(DTOEditorialLibro editorialLibros)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var newEditorial = new Editorial()
                {
                    Nombre = editorialLibros.Nombre
                };

                await context.AddAsync(newEditorial);
                await context.SaveChangesAsync();

                foreach (var libro in editorialLibros.Libros)
                {
                    var newLibro = new Libro()
                    {
                        Nombre = libro.Nombre,
                        Paginas = libro.Paginas,
                        Editorial = newEditorial
                    };

                    await context.AddAsync(newLibro);
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Created("Editorial", new { editorial = newEditorial });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest("Se ha producido un error");
            }
        }

        // 4.7
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutEditorial([FromRoute] int id, [FromBody] Editorial editorial)
        {
            if (id != editorial.EditorialId)
            {
                return BadRequest("Los ids proporcionados son diferentes");
            }
            var existe = await context.Editoriales.AnyAsync(x => x.EditorialId == id);
            if (!existe)
            {
                return NotFound();
            }
            context.Update(editorial);

            await context.SaveChangesAsync();
            return NoContent();
        }

        // 4.8
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var editorial = await context.Editoriales.Include(x => x.Libros).FirstOrDefaultAsync(x => x.EditorialId == id);

            if (editorial is null)
            {
                return NotFound();
            }

            if (editorial.Libros.Count > 0)
            {
                return BadRequest("Esta editorial tiene libros asignados. No se puede eliminar");
            }

            context.Remove(editorial);
            await context.SaveChangesAsync();
            return Ok();
        }

        // 4.9.c
        [HttpDelete("Logico/{id:int}")]
        public async Task<ActionResult> DeleteLogico(int id)
        {
            var editorial = await context.Editoriales.AsTracking().FirstOrDefaultAsync(x => x.EditorialId == id);

            if (editorial is null)
            {
                return NotFound();
            }

            editorial.Eliminado = true;
            await context.SaveChangesAsync();
            return Ok();
        }

        // 4.9.e
        [HttpPut("Restaurar/{id:int}")]
        public async Task<ActionResult> Restaurar(int id)
        {
            var familia = await context.Editoriales.AsTracking()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.EditorialId == id);

            if (familia is null)
            {
                return NotFound();
            }

            familia.Eliminado = false;
            await context.SaveChangesAsync();
            return Ok();
        }

    }


}

