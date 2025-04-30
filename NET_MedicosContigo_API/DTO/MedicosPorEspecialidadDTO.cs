using Microsoft.EntityFrameworkCore;

namespace NET_MedicosContigo_API.DTO
{
    [Keyless]
    public class MedicosPorEspecialidadDTO
    {
        public int Id { get; set; }
        public string Medico { get; set; } = null!;
    }
}
