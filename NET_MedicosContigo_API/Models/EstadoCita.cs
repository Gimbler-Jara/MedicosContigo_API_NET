using System.ComponentModel.DataAnnotations.Schema;

namespace NET_MedicosContigo_API.Models
{
    [Table("tb_estado_cita")]
    public class EstadoCita
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("estado")]
        public string Estado { get; set; } = null!;

        public ICollection<CitaMedica>? Citas { get; set; }
    }
}
