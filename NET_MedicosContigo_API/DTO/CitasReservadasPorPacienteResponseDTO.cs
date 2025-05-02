namespace NET_MedicosContigo_API.DTO
{
    public class CitasReservadasPorPacienteResponseDTO
    {
        public int Id { get; set; }
        public int IdMedico { get; set; }
        public string Medico { get; set; } = null!;
        public string Especialidad { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public int IdHora { get; set; }
    }
}
