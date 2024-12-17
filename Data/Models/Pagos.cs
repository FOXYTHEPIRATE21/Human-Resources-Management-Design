using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncapacidadesWeb.Data.Models;

public class Pagos
{
    [Key]
    public Guid Id { get; set; }
    public Guid IncapacidadId { get; set; }
    [Column(TypeName = "decimal(10, 2)")]
    public decimal MontoPagado { get; set; }
    public DateTime FechaPago { get; set; }
    [StringLength(50)]
    public string? MetodoPago { get; set; }

    // Navegación
    public virtual Incapacidades? Incapacidad { get; set; }
}