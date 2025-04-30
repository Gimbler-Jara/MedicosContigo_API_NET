using System.ComponentModel.DataAnnotations.Schema;

namespace NET_MedicosContigo_API.Models
{
    [Table("tb_medico")]
    public class Medico
    {
        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("especialidad_id")]
        public int EspecialidadId { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }
        public Especialidad? Especialidad { get; set; }

        public ICollection<CitaMedica>? Citas { get; set; }
    }
}
