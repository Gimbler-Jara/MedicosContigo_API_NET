using Microsoft.EntityFrameworkCore;

namespace NET_MedicosContigo_API.Models
{
    [Keyless]
    public class DisponibilidadCitaPorMedicoDTO
    {
        public int IdDisponibilidad { get; set; }
        public int IdDia { get; set; }
        public string Dia { get; set; } = null!;
        public int IdHora { get; set; }
        public string Hora { get; set; } = null!;
        public bool Activo { get; set; }
    }
}
