using System.ComponentModel.DataAnnotations;

namespace IncapacidadesWeb.Data.Models;

public class Portal
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [StringLength(255)]
    public string Nombre { get; set; } = string.Empty;
    [StringLength(255)]
    public string? Direccion { get; set; }
    [StringLength(50)]
    public string? Telefono { get; set; }
    [StringLength(100)]
    [EmailAddress]
    public string? Email { get; set; }

    // Navegación
    public virtual ICollection<Transcripciones> Transcripciones { get; set; } = new List<Transcripciones>();
}