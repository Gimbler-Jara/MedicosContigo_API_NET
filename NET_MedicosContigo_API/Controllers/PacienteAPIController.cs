﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET_MedicosContigo_API.Data;
using NET_MedicosContigo_API.DTO;
using NET_MedicosContigo_API.Models;
using NET_MedicosContigo_API.Reposotorio.DAO;

namespace NET_MedicosContigo_API.Controllers
{
    [Route("api/pacientes")]
    [ApiController]
    public class PacienteAPIController : ControllerBase
    {
        private readonly pacienteDAO _pacienteDTO;

        public PacienteAPIController(AplicationDbContext context)
        {
            _pacienteDTO = new pacienteDAO(context);
        }

        // GET: /api/pacientes
        [HttpGet]
        public IActionResult ListarPacientes()
        {
            var lista = _pacienteDTO.listarPacientes();
            return Ok(lista);
        }

        // POST: /api/pacientes
        [HttpPost]
        public ActionResult<PacienteResponseDTO> RegistrarPaciente([FromBody] RegistroPacienteDTO dto)
        {
            try
            {
                var paciente = _pacienteDTO.registrarPaciente(dto);
                return CreatedAtAction(nameof(RegistrarPaciente), new { id = paciente.IdUsuario }, paciente);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // PUT: /api/pacientes/{id}
        [HttpPut("{id}")]
        public IActionResult ActualizarPaciente(int id, [FromBody] PacienteActualizacionDTO dto)
        {
            var actualizado = _pacienteDTO.actualizarPaciente(id, dto);
            if (!actualizado)
                return NotFound(new { success = false, message = "Paciente no encontrado" });

            return Ok(new { success = true, message = "Paciente actualizado correctamente" });
        }
    }
}
