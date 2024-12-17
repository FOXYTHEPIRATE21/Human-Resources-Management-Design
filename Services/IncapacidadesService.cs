using IncapacidadesWeb.Data.Context;
using IncapacidadesWeb.Data.Models;

namespace IncapacidadesWeb.Services
{
    public class IncapacidadService
    {
        private readonly ApplicationDbContext _context;

        public IncapacidadService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Incapacidades> CrearIncapacidadAsync(Incapacidades incapacidad)
        {
            // Validación de datos básicos
            if (incapacidad.FechaFin <= incapacidad.FechaInicio)
                throw new ArgumentException("La fecha de fin debe ser mayor que la fecha de inicio");

            // Calculamos los días de incapacidad automáticamente
            incapacidad.DiasIncapacidad = (int)(incapacidad.FechaFin - incapacidad.FechaInicio).TotalDays + 1;

            // Guardar la nueva incapacidad
            _context.Incapacidades.Add(incapacidad);
            await _context.SaveChangesAsync();
            return incapacidad;
        }
    }
}
