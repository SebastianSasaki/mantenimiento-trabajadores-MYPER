using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MantenimientoTrabajadores.Data;
using MantenimientoTrabajadores.Models;

namespace MantenimientoTrabajadores.Controllers
{
    public class TrabajadoresController : Controller
    {
        private readonly TrabajadorRepository _repo;

        public TrabajadoresController(TrabajadorRepository repo) { _repo = repo; }

        // ✅ GET: /Trabajadores?sexo=Masculino
        public async Task<IActionResult> Index(string? sexo)
        {
            sexo = string.IsNullOrWhiteSpace(sexo) ? null : sexo;
            var lista = await _repo.ListarAsync(string.IsNullOrEmpty(sexo) ? null : sexo);
            ViewBag.SexoSeleccionado = sexo;
            return View(lista);
        }

        // ✅ GET: /Trabajadores/Obtener/5
        [HttpGet]
        public async Task<IActionResult> Obtener(int id)
        {
            var trabajador = await _repo.ObtenerPorIdAsync(id);
            if (trabajador == null)
                return NotFound();

            return View(trabajador);
        }

        // ✅ POST: /Trabajadores/Crear
        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] Trabajador trabajador, IFormFile? foto)
        {
            if (foto != null && foto.Length > 0)
            {
                var extension = Path.GetExtension(foto.FileName);
                var permitido = new[] { ".jpg", ".jpeg", ".png" };

                if (!permitido.Contains(extension.ToLower()))
                    return BadRequest(new { message = "Formato de imagen no permitido." });

                var fileName = Guid.NewGuid().ToString() + extension;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                    await foto.CopyToAsync(stream);

                trabajador.FotoUrl = "/uploads/" + fileName;
            }

            _repo.Agregar(trabajador);
            await _repo.GuardarAsync();
            return Ok(new { message = "Trabajador agregado correctamente" });
        }

        // ✅ POST: /Trabajadores/Editar
        [HttpPost]
        public async Task<IActionResult> Editar([FromForm] Trabajador trabajador, IFormFile? foto)
        {
            var trabajadorDb = await _repo.ObtenerPorIdAsync(trabajador.Id);
            if (trabajadorDb == null)
                return NotFound();

            trabajadorDb.Nombres = trabajador.Nombres;
            trabajadorDb.Apellidos = trabajador.Apellidos;
            trabajadorDb.TipoDocumento = trabajador.TipoDocumento;
            trabajadorDb.NumeroDocumento = trabajador.NumeroDocumento;
            trabajadorDb.Sexo = trabajador.Sexo;
            trabajadorDb.FechaNacimiento = trabajador.FechaNacimiento;
            trabajadorDb.Direccion = trabajador.Direccion;

            if (foto != null && foto.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                    await foto.CopyToAsync(stream);

                trabajadorDb.FotoUrl = "/uploads/" + fileName;
            }

            await _repo.GuardarAsync();
            return Ok(new { message = "Trabajador actualizado correctamente" });
        }

        // ✅ POST: /Trabajadores/Eliminar
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var trabajador = await _repo.ObtenerPorIdAsync(id);
            if (trabajador == null)
                return NotFound();

            _repo.Eliminar(trabajador);
            await _repo.GuardarAsync();
            return Ok(new { message = "Trabajador eliminado correctamente" });
        }
    }
}