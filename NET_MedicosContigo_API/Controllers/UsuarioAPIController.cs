using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NET_MedicosContigo_API.Data;
using NET_MedicosContigo_API.DTO;
using NET_MedicosContigo_API.Models;
using NET_MedicosContigo_API.Reposotorio.DAO;

namespace NET_MedicosContigo_API.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioAPIController : ControllerBase
    {
        private readonly usuarioDAO _usuarioDao;

        public UsuarioAPIController(AplicationDbContext context)
        {
            _usuarioDao = new usuarioDAO(context);
        }

        [HttpGet]
        public IActionResult ListarUsuarios()
        {
            var usuarios = _usuarioDao.ListarUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerUsuario(int id)
        {
            var usuario = _usuarioDao.ObtenerUsuario(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult CrearUsuario([FromBody] Usuario dto)
        {
            var usuario = new Usuario
            {
                DocumentType = dto.DocumentType,
                Dni = dto.Dni,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                FirstName = dto.FirstName,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                Telefono = dto.Telefono,
                Email = dto.Email,
                PasswordHash = dto.PasswordHash,
                RolId = dto.RolId
            };

            var creado = _usuarioDao.CrearUsuario(usuario);
            return CreatedAtAction(nameof(ObtenerUsuario), new { id = creado.Id }, creado);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarUsuario(int id, [FromBody] Usuario dto)
        {
            var usuario = new Usuario
            {
                Id = id,
                DocumentType = dto.DocumentType,
                Dni = dto.Dni,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                FirstName = dto.FirstName,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                Telefono = dto.Telefono,
                Email = dto.Email,
                PasswordHash = dto.PasswordHash,
                RolId = dto.RolId
            };

            var actualizado = _usuarioDao.ActualizarUsuario(id, usuario);
            return Ok(actualizado);
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            _usuarioDao.EliminarUsuario(id);
            return NoContent();
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            var (usuario, emailExiste) = _usuarioDao.BuscarPorEmail(dto);

            if (usuario == null)
            {
                if (!emailExiste)
                    return NotFound(new { message = "Usuario no encontrado" });

                return Unauthorized(new { message = "Contraseña incorrecta" });
            }

            return Ok(usuario);
        }

    }
}
