using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET_MedicosContigo_API.Data;
using NET_MedicosContigo_API.Reposotorio.DAO;

namespace NET_MedicosContigo_API.Controllers
{
    [Route("api/diasSemana")]
    [ApiController]
    public class DiaSemanaAPIController : ControllerBase
    {

        private readonly diaSemanaDTO _diaSemanaDTO;


        public DiaSemanaAPIController(AplicationDbContext context)
        {
            _diaSemanaDTO = new diaSemanaDTO(context);
        }


        [HttpGet]
        public IActionResult ListarDiasSemana()
        {
            var dias = _diaSemanaDTO.ListarDiasSemana();

            if (dias == null || !dias.Any())
                return NoContent();

            return Ok(dias);
        }
    }
}
