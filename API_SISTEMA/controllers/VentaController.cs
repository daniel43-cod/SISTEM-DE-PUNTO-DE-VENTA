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


        
        
    }


}
