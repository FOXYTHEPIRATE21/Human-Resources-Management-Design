using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncapacidadesWeb.Data.Models;

public class Incapacidades
{
    [Key]
    public Guid Id { get; set; }
    public Guid ColaboradorId { get; set; }
    [StringLength(100)]
    public string? TipoIncapacidad { get; set; }
    [Required]
    public DateTime FechaInicio { get; set; }
    [Required]
    public DateTime FechaFin { get; set; }
    [Required]
    public int DiasIncapacidad { get; set; }
    [StringLength(50)]
    public string Estado { get; set; } = "pendiente";

    // Navegación
    public virtual Usuario? Colaborador { get; set; }
    public virtual ICollection<Transcripciones> Transcripciones { get; set; } = new List<Transcripciones>();
    public virtual ICollection<Pagos> Pagos { get; set; } = [];
}