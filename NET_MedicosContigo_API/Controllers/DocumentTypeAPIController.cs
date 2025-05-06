using Microsoft.AspNetCore.Mvc;
using NET_MedicosContigo_API.Data;
using NET_MedicosContigo_API.Reposotorio.DAO;

namespace NET_MedicosContigo_API.Controllers
{
    [Route("api/document-types")]
    [ApiController]
    public class DocumentTypeAPIController : ControllerBase
    {
        private readonly documentTypeDAO _documentTypeDTO;

        public DocumentTypeAPIController(AplicationDbContext context)
        {
            _documentTypeDTO = new documentTypeDAO(context);
        }

        [HttpGet]
        public IActionResult ListarTiposDocumento()
        {
            var tipos = _documentTypeDTO.ListarTiposDocumento();

            if (tipos == null || !tipos.Any())
                return NoContent();

            return Ok(tipos);
        }
    }
}
