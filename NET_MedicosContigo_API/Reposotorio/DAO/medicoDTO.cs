using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NET_MedicosContigo_API.Data;
using NET_MedicosContigo_API.DTO;
using NET_MedicosContigo_API.Models;
using NET_MedicosContigo_API.Reposotorio.Interfaces;

namespace NET_MedicosContigo_API.Reposotorio.DAO
{
    public class medicoDTO : IMedico
    {

        private readonly AplicationDbContext _context;

        public medicoDTO(AplicationDbContext context)
        {
            _context = context;
        }

        public bool ActualizarMedico(int id, MedicoActualizacionDTO dto)
        {
            var medico = _context.Medicos
               .Include(m => m.Usuario)
               .FirstOrDefault(m => m.IdUsuario == id);

            if (medico == null)
                return false;

            var usuario = medico.Usuario!;
            usuario.FirstName = dto.FirstName;
            usuario.MiddleName = dto.MiddleName;
            usuario.LastName = dto.LastName;
            usuario.Telefono = dto.Telefono;

            // Si se envía nueva especialidad
            if (dto.EspecialidadId.HasValue)
            {
                var especialidad = _context.Especialidades.Find(dto.EspecialidadId.Value);
                if (especialidad != null)
                {
                    medico.EspecialidadId = especialidad.Id;
                }
            }

            _context.SaveChanges();
            return true;
        }

        public IEnumerable<MedicoResponseDTO> ListarMedicos()
        {
            return _context.Medicos
             .Include(m => m.Usuario)!
                 .ThenInclude(u => u.DocumentType)
             .Include(m => m.Usuario)!
                 .ThenInclude(u => u.Rol)
             .Include(m => m.Especialidad)
             .Select(m => new MedicoResponseDTO
             {
                 IdUsuario = m.IdUsuario,
                 Usuario = new UsuarioDTO
                 {
                     Id = m.Usuario!.Id,
                     Dni = m.Usuario.Dni,
                     LastName = m.Usuario.LastName,
                     MiddleName = m.Usuario.MiddleName,
                     FirstName = m.Usuario.FirstName,
                     BirthDate = m.Usuario.BirthDate,
                     Gender = m.Usuario.Gender.ToString(),
                     Telefono = m.Usuario.Telefono,
                     Email = m.Usuario.Email,
                     PasswordHash = m.Usuario.PasswordHash,
                     Activo = m.Usuario.Activo,
                     DocumentType = new DocumentTypeDTO
                     {
                         Id = m.Usuario.DocumentType!.Id,
                         Doc = m.Usuario.DocumentType.Doc
                     },
                     Rol = new RolDTO
                     {
                         Id = m.Usuario.Rol!.Id,
                         Rol = m.Usuario.Rol.Nombre
                     }
                 },
                 Especialidad = new EspecialidadDTO
                 {
                     Id = m.Especialidad!.Id,
                     Especialidad = m.Especialidad.Nombre
                 }
             }).ToList();
        }



        public MedicoResponseDTO RegistrarMedico(RegistroMedicoDTO dto)
        {
            if (_context.Usuarios.Any(u => u.Email == dto.Email))
                throw new ArgumentException("El correo electrónico ya está registrado.");

            var documentType = _context.DocumentTypes.Find(dto.DocumentTypeId)
                ?? throw new Exception("Tipo de documento no encontrado");

            var rolMedico = _context.Roles.Find(2)
                ?? throw new Exception("Rol médico no encontrado");

            var especialidad = _context.Especialidades.Find(dto.EspecialidadId)
                ?? throw new Exception("Especialidad no encontrada");

            var usuario = new Usuario
            {
                DocumentTypeId = dto.DocumentTypeId,
                Dni = dto.Dni,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                FirstName = dto.FirstName,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender[0],
                Telefono = dto.Telefono,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RolId = rolMedico.Id,
                Activo = true
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            var medico = new Medico
            {
                IdUsuario = usuario.Id,
                EspecialidadId = especialidad.Id
            };

            _context.Medicos.Add(medico);
            _context.SaveChanges();

            return new MedicoResponseDTO
            {
                IdUsuario = usuario.Id,
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
                        Id = rolMedico.Id,
                        Rol = rolMedico.Nombre
                    }
                },
                Especialidad = new EspecialidadDTO
                {
                    Id = especialidad.Id,
                    Especialidad = especialidad.Nombre
                }
            };
        }

        public Especialidad? ObtenerEspecialidadPorIdMedico(int idMedico)
        {
            return _context.Especialidades
                .FromSqlRaw("EXEC sp_consultar_especialidad_por_id_medico @idMedico",
                    new SqlParameter("@idMedico", idMedico))
                .AsEnumerable()
                .FirstOrDefault();
        }

        public IEnumerable<MedicosPorEspecialidadDTO> ListarMedicosPorEspecialidad(int idEspecialidad)
        {
            return _context.Set<MedicosPorEspecialidadDTO>()
               .FromSqlRaw("EXEC sp_listar_medicos_por_especialidad @idEspecialidad",
                   new SqlParameter("@idEspecialidad", idEspecialidad))
               .ToList();
        }

        public IEnumerable<DiasDisponiblesPorMedicoDTO> ListarDiasDisponiblesPorMedico(int idMedico)
        {
            return _context.Set<DiasDisponiblesPorMedicoDTO>()
               .FromSqlRaw("EXEC sp_listar_dias_disponibles_por_medico @idMedico",
                   new SqlParameter("@idMedico", idMedico))
               .ToList();
        }

        public IEnumerable<HorasDisponiblesDeCitasDTO> ListarHorasDisponibles(int idMedico, DateTime fecha)
        {
            return _context.Set<HorasDisponiblesDeCitasDTO>()
               .FromSqlRaw("EXEC sp_listar_horas_disponibles @idMedico, @fecha",
                   new SqlParameter("@idMedico", idMedico),
                   new SqlParameter("@fecha", fecha))
               .ToList();
        }

        public IEnumerable<DisponibilidadCitaPorMedicoDTO> ListarHorariosDeTrabajoMedico(int idMedico)
        {
            return _context.Set<DisponibilidadCitaPorMedicoDTO>()
               .FromSqlRaw("EXEC sp_listar_horarios_de_trabajo_medico @idMedico",
                   new SqlParameter("@idMedico", idMedico))
               .ToList();
        }
    }
}
