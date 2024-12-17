using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncapacidadesWeb.Data.Models;

public class Cobros
{
    [Key]
    public Guid Id { get; set; }
    public Guid TranscripcionId { get; set; }
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Monto { get; set; }
    public DateTime FechaCobro { get; set; }
    [StringLength(50)]
    public string Estado { get; set; } = "pendiente";

    // Navegación
    public virtual Transcripciones? Transcripcion { get; set; }
}