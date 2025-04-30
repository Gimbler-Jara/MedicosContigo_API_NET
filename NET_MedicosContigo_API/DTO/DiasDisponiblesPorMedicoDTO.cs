using Microsoft.EntityFrameworkCore;

namespace NET_MedicosContigo_API.DTO
{
    [Keyless]
    public class DiasDisponiblesPorMedicoDTO
    {
        public int Id { get; set; }
        public string Dia { get; set; } = null!;
    }
}
