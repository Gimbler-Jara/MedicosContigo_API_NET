using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NET_MedicosContigo_API.Data;
using NET_MedicosContigo_API.DTO;
using NET_MedicosContigo_API.Emun;
using NET_MedicosContigo_API.Models;
using NET_MedicosContigo_API.Reposotorio.Interfaces;
using System.Text.Json;

namespace NET_MedicosContigo_API.Reposotorio.DAO
{
    public class usuarioDAO : IUsuario
    {
        private readonly AplicationDbContext _context;

        public usuarioDAO(AplicationDbContext context)
        {
            _context = context;
        }

        public Usuario ActualizarUsuario(int id, Usuario usuario)
        {
            var existente = _context.Usuarios.Find(id);
            if (existente == null) return null!;
            _context.Entry(existente).CurrentValues.SetValues(usuario);
            _context.SaveChanges();
            return existente;
        }

        public (Usuario? usuario, LoginResultado resultado) BuscarPorEmail(LoginDTO dto)
        {
            var usuario = _context.Usuarios
                .Include(u => u.DocumentType)
                .Include(u => u.Rol)
                .FirstOrDefault(u => u.Email == dto.Email);

            if (usuario == null)
            {
                return (null, LoginResultado.UsuarioNoExiste);
            }

            if (!usuario.Activo)
            {
                return (null, LoginResultado.UsuarioInactivo);
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
            {
                return (null, LoginResultado.ContraseñaIncorrecta);
            }

            return (usuario, LoginResultado.Exitoso);
        }

        public bool CambiarEstadoUsuario(int idUsuario)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == idUsuario);

            if (usuario == null)
                return false;

            usuario.Activo = !usuario.Activo;
            _context.SaveChanges();

            return true;
        }

        public Usuario CrearUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }

        public void EliminarUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Usuario> ListarUsuarios()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario ObtenerUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
            {
                throw new InvalidOperationException($"Usuario con ID {id} no encontrado.");
            }
            return usuario;
        }
    }
}
