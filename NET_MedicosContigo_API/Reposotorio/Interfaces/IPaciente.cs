using NET_MedicosContigo_API.DTO;
using NET_MedicosContigo_API.Models;

namespace NET_MedicosContigo_API.Reposotorio.Interfaces
{
    public interface IPaciente
    {

        IEnumerable<PacienteResponseDTO> listarPacientes();
        PacienteResponseDTO registrarPaciente(RegistroPacienteDTO paciente);
        bool actualizarPaciente(int id, PacienteActualizacionDTO paciente);
    }
}
