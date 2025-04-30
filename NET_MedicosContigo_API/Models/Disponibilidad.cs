namespace NET_MedicosContigo_API.Models
{
    public class Disponibilidad
    {
        public int Id { get; set; }
        public int IdMedico { get; set; }
        public int IdDiaSemana { get; set; }
        public int IdHora { get; set; }
        public int EspecialidadId { get; set; }
        public bool Activo { get; set; }

        public Usuario? Medico { get; set; }
        public DiaSemana? DiaSemana { get; set; }
        public Hora? Hora { get; set; }
        public Especialidad? Especialidad { get; set; }
    }
}
