using IncapacidadesWeb.Data.Models;
using IncapacidadesWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace IncapacidadesWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncapacidadesController : ControllerBase
    {
        private readonly IncapacidadService _incapacidadService;

        public IncapacidadesController(IncapacidadService incapacidadService)
        {
            _incapacidadService = incapacidadService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearIncapacidad([FromBody] Incapacidades incapacidad)
        {
            if (incapacidad == null)
                return BadRequest("Los datos de la incapacidad son requeridos");

            try
            {
                incapacidad.FechaInicio = incapacidad.FechaInicio.ToUniversalTime();
            incapacidad.FechaFin = incapacidad.FechaFin.ToUniversalTime();
            
                var nuevaIncapacidad = await _incapacidadService.CrearIncapacidadAsync(incapacidad);
                return CreatedAtAction(nameof(CrearIncapacidad), new { id = nuevaIncapacidad.Id }, nuevaIncapacidad);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
