using NET_MedicosContigo_API.DTO;

namespace NET_MedicosContigo_API.Reposotorio.Interfaces
{
    public interface ICitaMedica
    {
        IEnumerable<CitasAgendadasResponseDTO> ListarCitasAgendadas(int idMedico);
        Dictionary<string, object> RegistrarDisponibilidadDeCita(int idMedico, int idDiaSemana, int idHora, int idEspecialidad);
        Dictionary<string, object> CambiarEstadoDisponibilidad(int idMedico, int idDiaSemana, int idHora, bool activo);
        bool CambiarEstadoCitaReservadoAAtendido(int idCita);
        Dictionary<string, object> EliminarCitaReservado(int idCita);
        void AgendarCita(int idMedico, int idPaciente, DateTime fecha, int idHora);
        IEnumerable<CitasReservadasPorPacienteResponseDTO> ListarCitasReservadasPorPaciente(int idPaciente);
    }
}
