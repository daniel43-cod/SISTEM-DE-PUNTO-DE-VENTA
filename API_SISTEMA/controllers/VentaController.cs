using API_SISTEMA.data;
using API_SISTEMA.DTOs;
using API_SISTEMA.models;
using API_SISTEMA.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace API_SISTEMA.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {

        private readonly VentaService _service;

        public VentaController(VentaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ListarVentas()
        {
            var listar = await _service.ListarVentes();
            return Ok(listar);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearVenta([FromBody] VentasDTOs dto)
        {
            if (dto.detalles == null || dto.detalles.Count == 0)
            {
                return BadRequest("La venta debe tener al menos un producto.");
            }

            if (dto.id_cliente == null && dto.clienteNuevo == null)
            {
                return BadRequest("Debe seleccionar un cliente existente o ingresar un cliente nuevo.");
            }

            var venta = await _service.CrearVentas(dto);

            return Ok(venta);
        }
    }


}
