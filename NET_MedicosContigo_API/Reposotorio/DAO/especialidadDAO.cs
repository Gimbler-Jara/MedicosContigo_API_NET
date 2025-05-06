using NET_MedicosContigo_API.Data;
using NET_MedicosContigo_API.DTO;
using NET_MedicosContigo_API.Reposotorio.Interfaces;

namespace NET_MedicosContigo_API.Reposotorio.DAO
{
    public class especialidadDAO : IEspecialidad
    {
        private readonly AplicationDbContext _context;
        public especialidadDAO(AplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<EspecialidadDTO> ListarEspecialidades()
        {
            return _context.Especialidades
                .Select(e => new EspecialidadDTO
                {
                    Id = e.Id,
                    Especialidad = e.Nombre
                })
                .OrderBy(e => e.Especialidad)
                .ToList();
        }
    }
}
