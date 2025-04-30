using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET_MedicosContigo_API.Data;
using NET_MedicosContigo_API.Reposotorio.DAO;

namespace NET_MedicosContigo_API.Controllers
{
    [Route("api/especialidades")]
    [ApiController]
    public class EspecialidadAPIController : ControllerBase
    {

        private readonly especialidadDTO _especialidadDTO;

        public EspecialidadAPIController(AplicationDbContext context)
        {
            _especialidadDTO = new especialidadDTO(context);
        }

        [HttpGet]
        public IActionResult ListarEspecialidades()
        {
            var lista = _especialidadDTO.ListarEspecialidades();
            return Ok(lista);
        }

    }
}
