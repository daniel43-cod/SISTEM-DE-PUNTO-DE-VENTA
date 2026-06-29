using API_SISTEMA.DTOs;
using API_SISTEMA.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_SISTEMA.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
            private readonly PagoService _service;

            public PagosController(PagoService service)
            {
                _service = service;
            }

            [HttpPost("Registrar")]
            public async Task<IActionResult> RegistrarPago([FromBody] PagosDTO dto)
            {
                try
                {
                    var pago = await _service.RegistrarPago(dto);

                    return Ok(pago);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }


    }
}