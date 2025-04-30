using NET_MedicosContigo_API.Data;
using NET_MedicosContigo_API.Models;
using NET_MedicosContigo_API.Reposotorio.Interfaces;

namespace NET_MedicosContigo_API.Reposotorio.DAO
{
    public class documentTypeDTO  : IDocumentType
    {
        private readonly AplicationDbContext _context;
        public documentTypeDTO(AplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<DocumentType> ListarTiposDocumento()
        {
            return _context.DocumentTypes.ToList();
        }
    }
}
