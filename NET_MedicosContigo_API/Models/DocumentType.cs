using System.ComponentModel.DataAnnotations.Schema;

namespace NET_MedicosContigo_API.Models
{
    [Table("tb_document_type")]
    public class DocumentType
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("doc")]
        public string Doc { get; set; } = null!;

        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
