using Microsoft.EntityFrameworkCore;

namespace NET_MedicosContigo_API.DTO
{
    [Keyless]
    public class ResultadoGenerico
    {
        public int Success { get; set; }
        public string Message { get; set; } = null!;
    }

}
