using System.ComponentModel.DataAnnotations.Schema;

namespace NET_MedicosContigo_API.Models
{
    [Table("tb_dia_semana")]
    public class DiaSemana
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("dia")]
        public string Dia { get; set; } = null!;


        public ICollection<Disponibilidad>? Disponibilidades { get; set; }
    }
}
