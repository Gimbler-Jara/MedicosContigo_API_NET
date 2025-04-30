using System.ComponentModel.DataAnnotations.Schema;

namespace NET_MedicosContigo_API.Models
{
    [Table("tb_hora")]
    public class Hora
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("hora")]
        public string Valor { get; set; } = null!;

        public ICollection<Disponibilidad>? Disponibilidades { get; set; }
        public ICollection<CitaMedica>? Citas { get; set; }
    }
}
