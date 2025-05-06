using NET_MedicosContigo_API.Data;
using NET_MedicosContigo_API.Models;
using NET_MedicosContigo_API.Reposotorio.Interfaces;

namespace NET_MedicosContigo_API.Reposotorio.DAO
{
    public class diaSemanaDAO : IDiaSemana
    {

        private readonly AplicationDbContext _context;
        public diaSemanaDAO(AplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<DiaSemana> ListarDiasSemana()
        {
            return _context.DiasSemana.ToList();
        }
    }
}
