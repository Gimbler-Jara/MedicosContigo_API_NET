using System.ComponentModel.DataAnnotations.Schema;

namespace NET_MedicosContigo_API.Models
{
    [Table("tb_cita_medica")]
    public class CitaMedica
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("id_medico")]
        public int IdMedico { get; set; }

        [Column("id_paciente")]
        public int IdPaciente { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Column("idHora")]
        public int IdHora { get; set; }

        [Column("estado")]
        public int Estado { get; set; } = 1;

        [ForeignKey("IdMedico")]
        public Medico? Medico { get; set; }

        [ForeignKey("IdPaciente")]
        public Paciente? Paciente { get; set; }

        [ForeignKey("IdHora")]
        public Hora? Hora { get; set; }

        [ForeignKey("Estado")]
        public EstadoCita? EstadoCita { get; set; }
    }

}
