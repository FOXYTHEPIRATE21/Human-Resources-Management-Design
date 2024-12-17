using System.ComponentModel.DataAnnotations;

namespace IncapacidadesWeb.Data.Models;

public class Comunicaciones
{
    [Key]
    public Guid Id { get; set; }
    public Guid RemitenteId { get; set; }
    public Guid DestinatarioId { get; set; }
    public string? Mensaje { get; set; }
    public DateTime FechaEnvio { get; set; }

    // Navegación
    public virtual Usuario? Remitente { get; set; }
    public virtual Usuario? Destinatario { get; set; }
}