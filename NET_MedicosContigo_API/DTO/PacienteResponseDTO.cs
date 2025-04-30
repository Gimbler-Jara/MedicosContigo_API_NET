namespace NET_MedicosContigo_API.DTO
{
    public class PacienteResponseDTO
    {
        public int IdUsuario { get; set; }
        public UsuarioDTO Usuario { get; set; } = null!;
    }
}
