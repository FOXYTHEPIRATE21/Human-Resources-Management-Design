using System.ComponentModel.DataAnnotations;

namespace IncapacidadesWeb.Data.Models;

public class Transcripciones
{
    [Key]
    public Guid Id { get; set; }
    public Guid IncapacidadId { get; set; }
    public Guid PortalId { get; set; }
    public DateTime FechaTranscripcion { get; set; }
    [StringLength(50)]
    public string Estado { get; set; } = "pendiente";

    // Navegación
    public virtual Incapacidades? Incapacidad { get; set; }
    public virtual Portal? Portal { get; set; }
    public virtual ICollection<Cobros> Cobros { get; set; } = new List<Cobros>();
}