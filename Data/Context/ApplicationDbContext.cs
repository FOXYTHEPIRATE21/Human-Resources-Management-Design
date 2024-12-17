using Microsoft.EntityFrameworkCore;
using IncapacidadesWeb.Data.Models;

namespace IncapacidadesWeb.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public required DbSet<Usuario> Usuarios { get; set; }
    public required DbSet<Incapacidades> Incapacidades { get; set; }
    public required DbSet<Portal> Portales { get; set; }
    public required DbSet<Transcripciones> Transcripciones { get; set; }
    public required DbSet<Cobros> Cobros { get; set; }
    public required DbSet<Pagos> Pagos { get; set; }
    public required DbSet<Notificaciones> Notificaciones { get; set; }
    public required DbSet<Comunicaciones> Comunicaciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuraciones específicas
        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Comunicaciones>()
            .HasOne(c => c.Remitente)
            .WithMany(u => u.ComunicacionesEnviadas)
            .HasForeignKey(c => c.RemitenteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Comunicaciones>()
            .HasOne(c => c.Destinatario)
            .WithMany(u => u.ComunicacionesRecibidas)
            .HasForeignKey(c => c.DestinatarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}