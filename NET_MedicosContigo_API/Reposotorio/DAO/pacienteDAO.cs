using Microsoft.EntityFrameworkCore;
using NET_MedicosContigo_API.Data;
using NET_MedicosContigo_API.DTO;
using NET_MedicosContigo_API.Models;
using NET_MedicosContigo_API.Reposotorio.Interfaces;

namespace NET_MedicosContigo_API.Reposotorio.DAO
{
    public class pacienteDAO : IPaciente
    {
        private readonly AplicationDbContext _context;

        public pacienteDAO(AplicationDbContext context)
        {
            _context = context;
        }

        public bool actualizarPaciente(int id, PacienteActualizacionDTO dto)
        {
            var paciente = _context.Pacientes
        .Include(p => p.Usuario)
        .FirstOrDefault(p => p.IdUsuario == id);

            if (paciente == null) return false;

            var usuario = paciente.Usuario!;
            usuario.LastName = dto.LastName;
            usuario.MiddleName = dto.MiddleName;
            usuario.FirstName = dto.FirstName;
            usuario.Telefono = dto.Telefono;

            _context.SaveChanges();
            return true;
        }

        public IEnumerable<PacienteResponseDTO> listarPacientes()
        {
            return _context.Pacientes
                    .Include(p => p.Usuario)!
                        .ThenInclude(u => u.DocumentType)
                    .Include(p => p.Usuario)!
                        .ThenInclude(u => u.Rol)
                    .Select(p => new PacienteResponseDTO
                    {
                        IdUsuario = p.IdUsuario,
                        Usuario = new UsuarioDTO
                        {
                            Id = p.Usuario!.Id,
                            Dni = p.Usuario.Dni,
                            LastName = p.Usuario.LastName,
                            MiddleName = p.Usuario.MiddleName,
                            FirstName = p.Usuario.FirstName,
                            BirthDate = p.Usuario.BirthDate,
                            Gender = p.Usuario.Gender.ToString(),
                            Telefono = p.Usuario.Telefono,
                            Email = p.Usuario.Email,
                            PasswordHash = p.Usuario.PasswordHash,
                            Activo = p.Usuario.Activo,
                            DocumentType = new DocumentTypeDTO
                            {
                                Id = p.Usuario.DocumentType!.Id,
                                Doc = p.Usuario.DocumentType.Doc
                            },
                            Rol = new RolDTO
                            {
                                Id = p.Usuario.Rol!.Id,
                                Rol = p.Usuario.Rol.Rol
                            }
                        }
                    })
                    .ToList();
        }

        public PacienteResponseDTO registrarPaciente(RegistroPacienteDTO dto)
        {
            if (_context.Usuarios.Any(u => u.Email == dto.Email))
            {
                throw new ArgumentException("El correo electrónico ya está registrado.");
            }

            var documentType = _context.DocumentTypes.Find(dto.DocumentTypeId)
                ?? throw new Exception("Tipo de documento no encontrado");

            var rolPaciente = _context.Roles.Find(1)
                ?? throw new Exception("Rol paciente no encontrado");

            var usuario = new Usuario
            {
                DocumentTypeId = documentType.Id,
                Dni = dto.Dni,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                FirstName = dto.FirstName,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender[0],
                Telefono = dto.Telefono,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RolId = rolPaciente.Id,
                Activo = true
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            var paciente = new Paciente
            {
                IdUsuario = usuario.Id
            };

            _context.Pacientes.Add(paciente);
            _context.SaveChanges();

            return new PacienteResponseDTO
            {
                IdUsuario = paciente.IdUsuario,
                Usuario = new UsuarioDTO
                {
                    Id = usuario.Id,
                    Dni = usuario.Dni,
                    LastName = usuario.LastName,
                    MiddleName = usuario.MiddleName,
                    FirstName = usuario.FirstName,
                    BirthDate = usuario.BirthDate,
                    Gender = usuario.Gender.ToString(),
                    Telefono = usuario.Telefono,
                    Email = usuario.Email,
                    PasswordHash = usuario.PasswordHash,
                    Activo = usuario.Activo,
                    DocumentType = new DocumentTypeDTO
                    {
                        Id = documentType.Id,
                        Doc = documentType.Doc
                    },
                    Rol = new RolDTO
                    {
                        Id = rolPaciente.Id,
                        Rol = rolPaciente.Rol
                    }
                }
            };
        }
    }
}
