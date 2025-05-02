using NET_MedicosContigo_API.DTO;
using NET_MedicosContigo_API.Models;

namespace NET_MedicosContigo_API.Reposotorio.Interfaces
{
    public interface IMedico
    {
        IEnumerable<MedicoResponseDTO> ListarMedicos();
        MedicoResponseDTO RegistrarMedico(RegistroMedicoDTO dto);

        bool ActualizarMedico(int id, MedicoActualizacionDTO dto);

        Especialidad? ObtenerEspecialidadPorIdMedico(int idMedico);

        IEnumerable<MedicosPorEspecialidadDTO> ListarMedicosPorEspecialidad(int idEspecialidad);
        IEnumerable<DiasDisponiblesPorMedicoDTO> ListarDiasDisponiblesPorMedico(int idMedico);

        IEnumerable<HorasDisponiblesDeCitasDTO> ListarHorasDisponibles(int idMedico, DateTime fecha);

        IEnumerable<DisponibilidadCitaPorMedicoDTO> ListarHorariosDeTrabajoMedico(int idMedico);
    }
}
