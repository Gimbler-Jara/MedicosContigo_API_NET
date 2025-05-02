using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET_MedicosContigo_API.Data;
using NET_MedicosContigo_API.DTO;
using NET_MedicosContigo_API.Reposotorio.DAO;

namespace NET_MedicosContigo_API.Controllers
{
    [Route("api/cita-medica")]
    [ApiController]
    public class CitaMedicaAPIController : ControllerBase
    {
        private readonly citaMedicaDTO _citaMedicaDTO;
        public CitaMedicaAPIController(AplicationDbContext context)
        {
            _citaMedicaDTO = new citaMedicaDTO(context);
        }


        [HttpGet("citas-agendadas/{idMedico}")]
        public IActionResult ListarCitasAgendadas(int idMedico)
        {
            var citas = _citaMedicaDTO.ListarCitasAgendadas(idMedico);

            if (citas == null || !citas.Any())
                return NoContent();

            return Ok(citas);
        }


        [HttpPost("registrar-disponibilidad")]
        public IActionResult RegistrarDisponibilidadDeCita([FromBody] RegistrarDisponibilidadDeCitaDTO req)
        {
            var result = _citaMedicaDTO.RegistrarDisponibilidadDeCita(req.IdMedico, req.IdDiaSemana, req.IdHora, req.IdEspecialidad);
            return Ok(result);
        }


        [HttpPut("cambiar-estado-disponibilidad")]
        public IActionResult CambiarEstadoDisponibilidad([FromBody] CambiarEstadoDisponibilidadDTO dto)
        {
            var resultado = _citaMedicaDTO.CambiarEstadoDisponibilidad(dto.IdMedico, dto.IdDiaSemana, dto.IdHora, dto.Activo);
            return Ok(resultado);
        }


        [HttpPut("cambiar-estado-cita-reservado-atendido/{idCita}")]
        public IActionResult CambiarEstadoCitaReservadoAAtendido(int idCita)
        {
            bool exito = _citaMedicaDTO.CambiarEstadoCitaReservadoAAtendido(idCita);

            if (exito)
                return Ok();

            return StatusCode(500);
        }


        [HttpDelete("eliminar-cita/{idCita}")]
        public IActionResult EliminarCita(int idCita)
        {
            var resultado = _citaMedicaDTO.EliminarCitaReservado(idCita);
            if ((bool)resultado["success"])
                return Ok(resultado);
            else
                return BadRequest(resultado);
        }


        [HttpPost("agendar-cita")]
        public IActionResult AgendarCita([FromBody] AgendarCitaDTO dto)
        {
            try
            {
                _citaMedicaDTO.AgendarCita(dto.IdMedico, dto.IdPaciente, dto.Fecha, dto.IdHora);
                return StatusCode(StatusCodes.Status201Created, new
                {
                    success = true,
                    message = "Cita agendada correctamente"
                });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("No hay disponibilidad"))
                {
                    return BadRequest(new { success = false, message = ex.Message });
                }

                return StatusCode(500, new { success = false, message = "Error interno", error = ex.Message });
            }
        }

        [HttpGet("citas-reservadas-paciente/{idPaciente}")]
        public IActionResult ListarCitasReservadasPorPaciente(int idPaciente)
        {
            var citas = _citaMedicaDTO.ListarCitasReservadasPorPaciente(idPaciente).ToList();

            if (!citas.Any())
                return NoContent();

            return Ok(citas);
        }


    }
}
