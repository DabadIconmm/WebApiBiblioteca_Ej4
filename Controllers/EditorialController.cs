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

        public EditorialController(ApplicationDbContext context)
        {
            this.context = context;
        }
        //4.2
        [HttpGet("ListEditoriales")]
        public async Task<IEnumerable<Editorial>> Get()
        {
            return await context.Editoriales.ToListAsync();
        }
    }
}
