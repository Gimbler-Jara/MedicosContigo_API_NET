using NET_MedicosContigo_API.Models;

namespace NET_MedicosContigo_API.Reposotorio.Interfaces
{
    public interface IDiaSemana
    {
        IEnumerable<DiaSemana> ListarDiasSemana();
    }
}
