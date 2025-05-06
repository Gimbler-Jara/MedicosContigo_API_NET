using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NET_MedicosContigo_API.Data;
using NET_MedicosContigo_API.DTO;
using NET_MedicosContigo_API.Reposotorio.Interfaces;

namespace NET_MedicosContigo_API.Reposotorio.DAO
{
    public class citaMedicaDAO : ICitaMedica
    {
        private readonly AplicationDbContext _context;

        public citaMedicaDAO(AplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<CitasAgendadasResponseDTO> ListarCitasAgendadas(int idMedico)
        {
            return _context.Set<CitasAgendadasResponseDTO>()
                   .FromSqlRaw("EXEC usp_listar_citas_Agendadas @idMedico",
                       new SqlParameter("@idMedico", idMedico))
                   .ToList();
        }

        public Dictionary<string, object> RegistrarDisponibilidadDeCita(int idMedico, int idDiaSemana, int idHora, int idEspecialidad)
        {
            var result = _context.ResultadosGenericos
                .FromSqlRaw("EXEC sp_registrar_disponibilidad @p_idMedico = {0}, @p_idDiaSemana = {1}, @p_idHora = {2}, @p_idEspecialidad = {3}",
                            idMedico, idDiaSemana, idHora, idEspecialidad)
                .AsEnumerable()
                .FirstOrDefault();

            return new Dictionary<string, object>
            {
                { "success", result?.Success == 1 },
                { "message", result?.Message ?? "Error al registrar disponibilidad" }
            };
        }

        public Dictionary<string, object> CambiarEstadoDisponibilidad(int idMedico, int idDiaSemana, int idHora, bool activo)
        {
            try
            {
                int activoInt = activo ? 1 : 0;

                _context.Database.ExecuteSqlRaw("EXEC sp_cambiar_estado_disponibilidad @p_idMedico = {0}, @p_idDiaSemana = {1}, @p_idHora = {2}, @p_activo = {3}",
                    idMedico, idDiaSemana, idHora, activoInt);

                return new Dictionary<string, object>
                     {
                         { "success", true },
                         { "message", "Estado de disponibilidad actualizado (o ya estaba igual)" }
                     };
            }
            catch (Exception)
            {
                return new Dictionary<string, object>
                  {
                      { "success", false },
                      { "message", "Error al cambiar estado de disponibilidad" }
                  };
            }
        }

        public bool CambiarEstadoCitaReservadoAAtendido(int idCita)
        {
            try
            {
                _context.Database.ExecuteSqlRaw("EXEC sp_cambiar_estado_cita_Reservado_a_atendido @idCita = {0}", idCita);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cambiar estado de cita: " + ex.Message);
                return false;
            }
        }

        public Dictionary<string, object> EliminarCitaReservado(int idCita)
        {
            var result = _context.ResultadoCitaDTO
                .FromSqlRaw("EXEC sp_eliminar_cita_Reservado @idCita",
                new SqlParameter("@idCita", idCita))
                .AsEnumerable()
                .FirstOrDefault();

            return new Dictionary<string, object>
            {
                { "success", result?.Success == 1 },
                { "message", result?.Message ?? "Error al eliminar cita" }
            };
        }

        public void AgendarCita(int idMedico, int idPaciente, DateTime fecha, int idHora)
        {
            _context.Database.ExecuteSqlRaw("EXEC sp_agendar_cita @idMedico, @idPaciente, @fecha, @idHora",
                new SqlParameter("@idMedico", idMedico),
                new SqlParameter("@idPaciente", idPaciente),
                new SqlParameter("@fecha", fecha),
                new SqlParameter("@idHora", idHora));
        }

        public IEnumerable<CitasReservadasPorPacienteResponseDTO> ListarCitasReservadasPorPaciente(int idPaciente)
        {
            return _context.CitasReservadasPorPaciente
                .FromSqlRaw("EXEC sp_listar_citas_programadas_por_paciente @idPaciente",
                    new SqlParameter("@idPaciente", idPaciente))
                .AsEnumerable();
        }

    }
}
