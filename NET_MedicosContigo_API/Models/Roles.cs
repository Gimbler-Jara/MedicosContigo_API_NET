using System.ComponentModel.DataAnnotations.Schema;

namespace NET_MedicosContigo_API.Models
{
    [Table("tb_rol")]
    public class Roles
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("rol")]
        public string Rol { get; set; } = null!;

        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
