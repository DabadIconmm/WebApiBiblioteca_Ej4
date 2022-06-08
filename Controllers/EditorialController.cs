using Ejercicio_Sesión_1.DTOs;
using Ejercicio_Sesión_1.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ejercicio_Sesión_1.Controllers
{
    [ApiController]
    [Route("api/EditorialController")]
    public class EditorialController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        //4.2
        public EditorialController(ApplicationDbContext context)
        {
            this.context = context;
        }
        //4.3.a
        [HttpGet("ListEditoriales")]
        public async Task<IEnumerable<Editorial>> Get()
        {
            return await context.Editoriales.ToListAsync();
        }
        //4.3.b
        [HttpGet("{id:int}")] //asi restringimo el tipo de argumento que queremos recibir
        public async Task<ActionResult<Editorial>> GetEditorial(int id)
        {
            //var editorial = await context.Editoriales.SingleOrDefaultAsync();//esta es una de las opciones.
            ////esta es la segunda opcion
            //var editorial = await context.Editoriales.FirstOrDefaultAsync(x=>x.Id==id);
            //Para devolver un indice
            var editorial = await context.Editoriales.FindAsync(id);
            return Ok(editorial);
        }
        //4.3.c
        [HttpGet("contiene")]
        public async Task<ActionResult<IEnumerable<Editorial>>> GetEditorialContiene([FromQuery]string contiene)
        {
            var editorial = await context.Editoriales.Where(x => x.Nombre.Contains(contiene)).OrderBy(x => x.Nombre).ToListAsync();
            return Ok(editorial);
        }
        //4.3.d
        [HttpGet("getEditoriales/{id:int}")]
        public async Task<ActionResult<Editorial>> GetEditoriales(int id)
        {
            var editorial = await context.Editoriales.Include(x => x.Libros).FirstOrDefaultAsync(x => x.Id == id);
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
                                       IdEditorial = x.Id,
                                       Nombre = x.Nombre,
                                       Libros = x.Libros.Select(y => new DTOLibroItem
                                       {
                                           IdLibro = y.Id,
                                           Nombre = y.Titulo,
                                           Paginas = y.Paginas
                                       }).ToList(),
                                   }).FirstOrDefaultAsync(x => x.IdEditorial == id);

            if (editorial == null)
            {
                return NotFound();
            }
            return Ok(editorial);
        }

    }
}
