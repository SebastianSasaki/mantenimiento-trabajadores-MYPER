using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MantenimientoTrabajadores.Models;

namespace MantenimientoTrabajadores.Data
{
    public class TrabajadorRepository
    {
        private readonly ApplicationDbContext _db;

        public TrabajadorRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        // Método para listar trabajadores con filtro opcional por sexo
        public async Task<List<Trabajador>> ListarAsync(string sexo = null)
        {
            var param = new SqlParameter("@Sexo", string.IsNullOrEmpty(sexo) ? (object)DBNull.Value : sexo);
            var lista = await _db.Trabajadores
                .FromSqlRaw("EXEC dbo.sp_ListarTrabajadores @Sexo", param)
                .ToListAsync();

            return lista;
        }
    }
}
