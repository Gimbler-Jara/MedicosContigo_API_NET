using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET_MedicosContigo_API.Data;
using NET_MedicosContigo_API.DTO;
using NET_MedicosContigo_API.Reposotorio.DAO;

namespace NET_MedicosContigo_API.Controllers
{
    [Route("api/medicos")]
    [ApiController]
    public class MedicoAPIController : ControllerBase
    {

        private readonly medicoDAO _medicoDTO;

        public MedicoAPIController(AplicationDbContext context)
        {
            _medicoDTO = new medicoDAO(context);
        }

        [HttpGet]
        public IActionResult ListarMedicos()
        {
            var medicos = _medicoDTO.ListarMedicos();   
            return Ok(medicos);
        }

        [HttpPost]
        public ActionResult<MedicoResponseDTO> RegistrarMedico([FromBody] RegistroMedicoDTO dto)
        {
            try
            {
                var medico = _medicoDTO.RegistrarMedico(dto);
                return CreatedAtAction(nameof(RegistrarMedico), new { id = medico.IdUsuario }, medico);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public IActionResult ActualizarMedico(int id, [FromBody] MedicoActualizacionDTO dto)
        {
            var actualizado = _medicoDTO.ActualizarMedico(id, dto);

            if (!actualizado)
                return NotFound(new { success = false, message = "Médico no encontrado" });

            return Ok(new { success = true, message = "Médico actualizado correctamente" });
        }


        [HttpGet("especialidad_por_id_medico/{idMedico}")]
        public IActionResult ObtenerEspecialidadPorIdMedico(int idMedico)
        {
            var especialidad = _medicoDTO.ObtenerEspecialidadPorIdMedico(idMedico);

            if (especialidad == null)
                return NoContent();

            return Ok(especialidad);
        }


        [HttpGet("medicos-por-especialidad/{idEspecialidad}")]
        public IActionResult ListarMedicosPorEspecialidad(int idEspecialidad)
        {
            var medicos = _medicoDTO.ListarMedicosPorEspecialidad(idEspecialidad);

            if (medicos == null || !medicos.Any())
                return NoContent();

            return Ok(medicos);
        }


        [HttpGet("dias-disponibles/{idMedico}")]
        public IActionResult ListarDiasDisponiblesPorMedico(int idMedico)
        {
            var dias = _medicoDTO.ListarDiasDisponiblesPorMedico(idMedico);

            if (dias == null || !dias.Any())
                return NoContent();

            return Ok(dias);
        }


        [HttpGet("horas-disponibles")]
        public IActionResult ListarHorasDisponibles([FromQuery] int idMedico, [FromQuery] DateTime fecha)
        {
            var horas = _medicoDTO.ListarHorasDisponibles(idMedico, fecha);

            if (horas == null || !horas.Any())
                return NoContent();

            return Ok(horas);
        }


        [HttpGet("horario-trabajo-medico/{idMedico}")]
        public IActionResult ListarHorariosDeTrabajoMedico(int idMedico)
        {
            var lista = _medicoDTO.ListarHorariosDeTrabajoMedico(idMedico);

            var response = new
            {
                success = true,
                message = "Disponibilidades encontradas",
                data = lista
            };

            return Ok(response);
        }


    }
}
