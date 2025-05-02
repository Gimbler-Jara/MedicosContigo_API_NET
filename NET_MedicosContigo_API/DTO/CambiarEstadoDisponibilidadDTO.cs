namespace NET_MedicosContigo_API.DTO
{
    public class CambiarEstadoDisponibilidadDTO
    {
        public int IdMedico { get; set; }
        public int IdDiaSemana { get; set; }
        public int IdHora { get; set; }
        public bool Activo { get; set; }
    }
}
