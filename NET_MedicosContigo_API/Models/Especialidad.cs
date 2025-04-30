using System.ComponentModel.DataAnnotations.Schema;

namespace NET_MedicosContigo_API.Models
{
    [Table("tb_especialidad")]
    public class Especialidad
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("especialidad")]
        public string Nombre { get; set; } = null!;

        public ICollection<Medico>? Medicos { get; set; }
        public ICollection<Disponibilidad>? Disponibilidades { get; set; }
    }
}
