using Microsoft.EntityFrameworkCore;

namespace NET_MedicosContigo_API.DTO
{
    [Keyless]
    public class CitasAgendadasResponseDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string PacienteID { get; set; } = null!;
        public string PacienteNombre { get; set; } = null!;
        public int MedicoId { get; set; }
        public string MedicoNombre { get; set; } = null!;
        public string Especialidad { get; set; } = null!;
    }
}
