using API_SISTEMA.data;
using API_SISTEMA.DTOs;
using API_SISTEMA.models;
using API_SISTEMA.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace API_SISTEMA.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {

        private readonly VentaService _context;

        public VentaController(VentaService service)
        {
            _context = service;
        }

        [HttpGet]
        public async Task<IActionResult> ListarVentas()
        {
            var listar = await _context.ListarVentes();
            return Ok(listar);
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> CrearVenta([FromBody] VentasDTOs dto)
        {
            try
            {
                var venta = await _context.CrearVenta(dto);
                return Ok(new
                {
                    venta.id_ventas,
                    venta.total,
                    venta.saldo_pendiente,
                    venta.estado_venta,
                    venta.fecha_venta
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }



    }


}
