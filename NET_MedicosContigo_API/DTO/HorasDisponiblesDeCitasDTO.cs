using Microsoft.EntityFrameworkCore;

namespace NET_MedicosContigo_API.DTO
{
    [Keyless]
    public class HorasDisponiblesDeCitasDTO
    {
        public int Id { get; set; }
        public string Hora { get; set; } = null!;
    }
}
