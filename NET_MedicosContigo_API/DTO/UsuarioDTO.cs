namespace NET_MedicosContigo_API.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Dni { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string FirstName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string PasswordHash { get; set; } = null!;
        public RolDTO Rol { get; set; } = null!;
        public DocumentTypeDTO DocumentType { get; set; } = null!;
    }
}
