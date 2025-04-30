namespace NET_MedicosContigo_API.DTO
{
    public class MedicoResponseDTO
    {
        public int IdUsuario { get; set; }
        public UsuarioDTO Usuario { get; set; } = null!;
        public EspecialidadDTO Especialidad { get; set; } = null!;
    }
}
