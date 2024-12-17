using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncapacidadesWeb.Data.Models
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Apellidos { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Direccion { get; set; }

        [StringLength(20)]
        public string? Telefono { get; set; }

        [Required]
        public Role Rol { get; set; } // Usamos el enum directamente

        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Cargo { get; set; }

        [StringLength(255)]
        public string? Departamento { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Navegación
        public virtual ICollection<Incapacidades> Incapacidades { get; set; } = new List<Incapacidades>();
        public virtual ICollection<Notificaciones> NotificacionesRecibidas { get; set; } = new List<Notificaciones>();
        public virtual ICollection<Comunicaciones> ComunicacionesEnviadas { get; set; } = new List<Comunicaciones>();
        public virtual ICollection<Comunicaciones> ComunicacionesRecibidas { get; set; } = new List<Comunicaciones>();
    }
}
