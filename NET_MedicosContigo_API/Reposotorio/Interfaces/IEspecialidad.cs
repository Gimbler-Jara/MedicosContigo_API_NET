using NET_MedicosContigo_API.DTO;

namespace NET_MedicosContigo_API.Reposotorio.Interfaces
{
    public interface IEspecialidad
    {
        IEnumerable<EspecialidadDTO> ListarEspecialidades();
    }
}
