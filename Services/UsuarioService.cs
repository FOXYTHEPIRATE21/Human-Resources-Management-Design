using IncapacidadesWeb.Data.Context;
using IncapacidadesWeb.Data.Models;
using System;
using System.Threading.Tasks;

namespace IncapacidadesWeb.Services
{
    public class UsuarioService
    {
        private readonly ApplicationDbContext _context;

        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> CrearUsuarioAsync(Usuario usuario)
        {
            // Validar que los datos requeridos estén presentes
            if (string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrEmpty(usuario.Apellidos) ||
                string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.PasswordHash))
            {
                throw new ArgumentException("Faltan datos requeridos.");
            }

            // Establecer la fecha de creación del usuario
            usuario.CreatedAt = DateTime.UtcNow;

            try
            {
                // Agregar el nuevo usuario a la base de datos
                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();

                return usuario;
            }
            catch (Exception ex)
            {
                // Si ocurre un error al guardar
                throw new InvalidOperationException("Ocurrió un error al crear el usuario.", ex);
            }
        }
    }
}
