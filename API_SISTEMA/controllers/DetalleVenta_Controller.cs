using API_SISTEMA.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_SISTEMA.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleVenta_Controller : ControllerBase
    {
        private readonly DetalleVenta_Service _Service;

        public DetalleVenta_Controller(DetalleVenta_Service service)
        {
            _Service = service;
        }

        [HttpGet]
        public async Task<IActionResult> listar()
        {
            var listar = await _Service.ListarVentes();
            return Ok(listar);
        }
    }
}
