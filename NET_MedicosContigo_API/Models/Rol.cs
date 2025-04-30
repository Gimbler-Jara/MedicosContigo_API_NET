using System.ComponentModel.DataAnnotations.Schema;

namespace NET_MedicosContigo_API.Models
{
    [Table("tb_rol")]
    public class Rol
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("rol")]
        public string Nombre { get; set; } = null!;

        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
