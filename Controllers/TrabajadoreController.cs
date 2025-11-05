using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MantenimientoTrabajadores.Data;
using MantenimientoTrabajadores.Models;
using System.Threading.Tasks;

namespace MantenimientoTrabajadores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrabajadoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly TrabajadorRepository _repo;

        public TrabajadoresController(ApplicationDbContext context, TrabajadorRepository repo)
        {
            _context = context;
            _repo = repo;
        }
        
        // ✅ GET: api/trabajadores?sexo=Masculino
        [HttpGet]
        public async Task<IActionResult> GetTrabajadores([FromQuery] string? sexo)
        {
            var lista = await _repo.ListarAsync(sexo);
            return Ok(lista);
        }

        // ✅ GET: api/trabajadores/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrabajador(int id)
        {
            var trabajador = await _context.Trabajadores.FindAsync(id);
            if (trabajador == null)
                return NotFound();

            return Ok(trabajador);
        }

        // ✅ POST: api/trabajadores
        [HttpPost]
        public async Task<IActionResult> CrearTrabajador([FromBody] Trabajador trabajador)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.Trabajadores.AddAsync(trabajador);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Trabajador registrado correctamente" });
        }

        // ✅ PUT: api/trabajadores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarTrabajador(int id, [FromBody] Trabajador trabajador)
        {
            if (id != trabajador.Id)
                return BadRequest("ID de trabajador no coincide");

            var existente = await _context.Trabajadores.FindAsync(id);
            if (existente == null)
                return NotFound();

            _context.Entry(existente).CurrentValues.SetValues(trabajador);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Trabajador actualizado correctamente" });
        }

        // ✅ DELETE: api/trabajadores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTrabajador(int id)
        {
            var trabajador = await _context.Trabajadores.FindAsync(id);
            if (trabajador == null)
                return NotFound();

            _context.Trabajadores.Remove(trabajador);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Trabajador eliminado correctamente" });
        }

        //public async Task<IActionResult> Index(string sexo)
        //{
        //    var trabajadores = await _repo.ListarAsync(sexo);
        //    return View(trabajadores);
        //}
    }
}
