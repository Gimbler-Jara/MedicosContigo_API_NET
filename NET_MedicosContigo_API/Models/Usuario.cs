using System.ComponentModel.DataAnnotations.Schema;

namespace NET_MedicosContigo_API.Models
{
    [Table("tb_usuario")]
    public class Usuario
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("document_type")]
        public int DocumentTypeId { get; set; }

        [Column("dni")]
        public string Dni { get; set; } = null!;

        [Column("last_name")]
        public string LastName { get; set; } = null!;

        [Column("middle_name")]
        public string? MiddleName { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; } = null!;

        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Column("gender")]
        public char Gender { get; set; }

        [Column("telefono")]
        public string? Telefono { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("password_hash")]
        public string PasswordHash { get; set; } = null!;

        [Column("rol_id")]
        public int RolId { get; set; }

        [Column("activo")]
        public bool Activo { get; set; }

        // Relaciones
        public DocumentType? DocumentType { get; set; }
        public Rol? Rol { get; set; }
        public Paciente? Paciente { get; set; }
        public Medico? Medico { get; set; }
        public ICollection<Disponibilidad>? Disponibilidades { get; set; }
    }
}
