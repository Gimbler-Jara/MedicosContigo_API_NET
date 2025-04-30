using System.ComponentModel.DataAnnotations.Schema;

namespace NET_MedicosContigo_API.Models
{
    [Table("tb_paciente")]
    public class Paciente
    {
        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }

        public ICollection<CitaMedica>? Citas { get; set; }
    }
}
