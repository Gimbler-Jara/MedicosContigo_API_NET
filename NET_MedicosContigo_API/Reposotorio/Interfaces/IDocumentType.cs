using NET_MedicosContigo_API.Models;

namespace NET_MedicosContigo_API.Reposotorio.Interfaces
{
    public interface IDocumentType
    {
        IEnumerable<DocumentType> ListarTiposDocumento();
    }
}
