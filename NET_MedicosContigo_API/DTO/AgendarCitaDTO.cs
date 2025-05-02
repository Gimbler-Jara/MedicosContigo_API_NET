namespace NET_MedicosContigo_API.DTO
{
    public class AgendarCitaDTO
    {
        public int IdMedico { get; set; }
        public int IdPaciente { get; set; }
        public DateTime Fecha { get; set; }
        public int IdHora { get; set; }
    }

}
