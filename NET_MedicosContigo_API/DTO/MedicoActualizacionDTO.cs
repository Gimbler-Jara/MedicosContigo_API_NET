namespace NET_MedicosContigo_API.DTO
{
    public class MedicoActualizacionDTO
    {
        public int IdUsuario { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string? Telefono { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = null!;
        public int? EspecialidadId { get; set; }
    }
}
