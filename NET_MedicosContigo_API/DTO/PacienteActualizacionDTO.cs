namespace NET_MedicosContigo_API.DTO
{
    public class PacienteActualizacionDTO
    {
        public int IdUsuario { get; set; }
        public int DocumentTypeId { get; set; }
        public string Dni { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string FirstName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; } = null!;
    }
}
