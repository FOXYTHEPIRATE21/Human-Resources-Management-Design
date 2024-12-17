using System.ComponentModel.DataAnnotations;

namespace IncapacidadesWeb.Data.Models;

public class Notificaciones
{
    [Key]
    public Guid Id { get; set; }
    public Guid DestinatarioId { get; set; }
    public string? Mensaje { get; set; }
    [StringLength(255)]
    public string? Tipo { get; set; }
    public DateTime FechaEnvio { get; set; }
    public bool Estado { get; set; }

    // Navegación
    public virtual Usuario? Destinatario { get; set; }
}